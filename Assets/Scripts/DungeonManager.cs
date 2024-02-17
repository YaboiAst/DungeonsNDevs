using System.Collections;
using System.Collections.Generic;
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

    [HideInInspector] public UnityEvent disableDungeon;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else return;
    }

    private void Start() {
        gm = GameManager.instance;
    }

    public void SetupDungeon(){
        if(gm == null){
            gm = GameManager.instance;
        }

        mainPlayer = gm.party[0];
        playerArt.sprite = mainPlayer.art;
        playerAnimator.runtimeAnimatorController = mainPlayer.combatAnimator;

        boss = gm.currentBoss;
        bossArt.sprite = boss.art;
        bossAnimator.runtimeAnimatorController = boss.combatAnimator;

        doorManager.SetupDoors();
    }

    private void OnDisable() {
        disableDungeon?.Invoke();
    }
}
