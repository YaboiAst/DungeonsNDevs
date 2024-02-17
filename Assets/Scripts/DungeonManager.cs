using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] SpriteRenderer playerArt, bossArt;
    [SerializeField] Animator playerAnimator, bossAnimator;
    //Portas

    Players mainPlayer;
    Bosses boss;

    private void Start() {
        gm = GameManager.instance;
        
        mainPlayer = gm.GetParty()[0];
        playerArt.sprite = mainPlayer.GetPlayer().art;
        playerAnimator.runtimeAnimatorController = mainPlayer.GetPlayer().animator;

        boss = gm.GetBoss();
        bossArt.sprite = boss.GetBoss().art;
        bossAnimator.runtimeAnimatorController = boss.GetBoss().animator;
    }
}
