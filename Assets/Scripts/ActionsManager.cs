using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    GameManager gm;

    private void Start() {
        gm = GameManager.instance;
    }

    public void BasicAttackButton(){
        gm.active.BasicAttack();
    }

    public void SpecialAttackButton(){
        gm.active.SpecialAttack();
    }

    public void CallNextTurn(){
        gm.Next();
    }
}
