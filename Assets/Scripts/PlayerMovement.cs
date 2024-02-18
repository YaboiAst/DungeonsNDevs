using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gm;
    private DungeonManager dm;

    [SerializeField] private float moveSpeed = 1f;

    private Vector3 input = Vector3.zero;
    private Rigidbody2D rb;

    private bool defeatedMonster = false;

    private bool canInteractMonster, canInteractDoor;
    private Door interactableDoor;

    private void Start() {
        gm = GameManager.instance;
        dm = DungeonManager.instance;
        rb = GetComponent<Rigidbody2D>();

        defeatedMonster = false;
    }

    private void OnEnable() {
        if(gm == null){
            return;
        }

        defeatedMonster = true;
    }

    private void GetInput(){
        input.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        input.y = Input.GetAxisRaw("Vertical")   * moveSpeed;
    }

    private void Update() {
        GetInput();

        if(Input.GetKeyDown(KeyCode.E)){
            if(canInteractMonster && !gm.isBossDefeated){
                gm.onStartCombat?.Invoke();
            }
            else if(canInteractDoor && interactableDoor != null && gm.isBossDefeated){
                interactableDoor.onSelectDoor?.Invoke();
            }
        }
    }

    private void FixedUpdate() {
        rb.velocity = input * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Boss")){
            canInteractMonster = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Boss")){
            canInteractMonster = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Doors")){
            canInteractDoor = true;
            interactableDoor = other.GetComponent<Door>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Doors")){
            canInteractDoor = false;
            interactableDoor = null;
        }
    }
}
