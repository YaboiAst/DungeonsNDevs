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

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isFacingRight;

    private bool canInteractMonster, canInteractDoor;
    private Door interactableDoor;

    private void Start() {
        gm = GameManager.instance;
        dm = DungeonManager.instance;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        if(gm == null){
            return;
        }
    }

    private void GetInput(){
        input.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        input.y = Input.GetAxisRaw("Vertical")   * moveSpeed;
    }

    private void Update() {
        GetInput();

        // ANIMATION --------------------
        if(input.x < 0 && !isFacingRight){
            isFacingRight = false;
            spriteRenderer.flipX = true;
        }
        if(input.x > 0 && !isFacingRight){
            isFacingRight = true;
            spriteRenderer.flipX = false;
        }
        else if(input.x < 0 && isFacingRight){
            isFacingRight = false;
            spriteRenderer.flipX = true;
        }

        if(input.x != 0){
            animator.SetBool("Horizontal", true);
        }
        else{
            if(input.y > 0)
                animator.SetInteger("Vertical", 1);
            if(input.y < 0)
                animator.SetInteger("Vertical", -1);
            else{   
                animator.SetInteger("Vertical", 0);
                animator.SetBool("Horizontal", false);
            }
                
        }

        // INTERACTIONS --------------------
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
            other.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Boss")){
            canInteractMonster = false;
            other.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Doors") && gm.isBossDefeated){
            canInteractDoor = true;
            interactableDoor = other.GetComponent<Door>();
            interactableDoor.OpenTip();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Doors") && gm.isBossDefeated){
            interactableDoor.CloseTip();
            canInteractDoor = false;
            interactableDoor = null;
        }
    }
}
