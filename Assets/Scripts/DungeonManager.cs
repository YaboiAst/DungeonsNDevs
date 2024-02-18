using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DungeonManager : MonoBehaviour
{
    [HideInInspector] public static DungeonManager instance;
    private GameManager gm;
    [SerializeField] DoorManager doorManager;

    [SerializeField] SpriteRenderer playerArt, bossArt;
    [SerializeField] Animator playerAnimator, bossAnimator;
    //Portas

    Player mainPlayer;
    Boss boss;

    PlayerMovement playerMoving;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else return;

        playerMoving = GetComponentInChildren<PlayerMovement>();
    }

    public void SetupDungeon(){
        if(gm == null){
            gm = GameManager.instance;
        }

        mainPlayer = gm.party[0];
        playerArt.sprite = mainPlayer.art;
        playerAnimator.runtimeAnimatorController = mainPlayer.walkingController;

        boss = gm.currentBoss;
        bossArt.sprite = boss.art;
        bossAnimator.runtimeAnimatorController = boss.combatAnimator;

        doorManager.SetupDoors();
    }

    private void OnEnable() {
        playerMoving.enabled = true;
        doorManager.enabled = true;

        if(gm != null){
            if(gm.isBossDefeated){
                Destroy(bossArt.gameObject);
            }
        }
    }

    private void OnDisable() {
        playerMoving.enabled = false;
        doorManager.enabled = false;
    }
}
