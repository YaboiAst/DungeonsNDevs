using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ActionsManager : MonoBehaviour
{
    TurnManager tm;

    [SerializeField] private Transform actionsTransform;
    [SerializeField] private Button basicAttackButton, specialAttackButton;
    private Image basicAttackButtonArt, specialAttackButtonArt;

    Sequence seqAction;
    public float offset;
    public float animationDuration = 1f;
    public float idleDuration = 0.5f;

    private void Start() {
        tm = TurnManager.instance;
        tm.onPassTurn.AddListener(ActionAnim);

        basicAttackButtonArt = basicAttackButton.GetComponent<Image>();
        specialAttackButtonArt = specialAttackButton.GetComponent<Image>();
    }

    private void OnEnable() {
        if(tm == null){
            tm = TurnManager.instance;
        }
        tm.onPassTurn.AddListener(ActionAnim);
        //ActionAnim();
    }

    public void BasicAttackButton(){
        tm.active.BasicAttack();
    }

    public void SpecialAttackButton(){
        Players active = (Players) tm.active;
        active.SpecialAttack();
    }

    public void CallNextTurn(){
        seqAction = DOTween.Sequence();

        seqAction.AppendCallback(() => SetButtons(false));
        seqAction.AppendInterval(.2f);
        
        seqAction.Append(actionsTransform.DOMoveY(actionsTransform.position.y - offset, animationDuration)
        .SetEase(Ease.OutQuad))
        .OnComplete(() => tm.Next());
    }

    public void ActionAnim(){
        seqAction = DOTween.Sequence();

        if(tm.active.isPlayer()){
            seqAction.AppendCallback(() => SetButtonImage());
            seqAction.AppendInterval(idleDuration);
            seqAction.Append(actionsTransform.DOMoveY(actionsTransform.position.y + offset, animationDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() => SetButtons(true)));
        }
    }

    private void SetButtonImage(){
        if(!tm.active.isPlayer())
            return;

        basicAttackButtonArt.sprite = tm.active.GetPlayer().basicAttackButton;
        specialAttackButtonArt.sprite = tm.active.GetPlayer().specialAttackButton;
    }

    private void SetButtons(bool mode){
        if(!tm.active.isPlayer())
            return;
        
        basicAttackButton.interactable = mode;
        if(tm.active.GetComponent<Players>().specialCooldownCounter > 0){
            specialAttackButton.interactable = false;
        }
        else{
            specialAttackButton.interactable = mode;
        }
    }
}
