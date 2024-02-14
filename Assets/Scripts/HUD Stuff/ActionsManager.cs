using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ActionsManager : MonoBehaviour
{
    GameManager gm;

    [SerializeField] private Transform actionsTransform;
    [SerializeField] private Button basicAttackButton, specialAttackButton;

    Sequence seqAction;
    public float offset;
    public float animationDuration = 1f;
    public float idleDuration = 0.5f;

    private void Start() {
        gm = GameManager.instance;
        gm.onPassTurn.AddListener(ActionAnim);
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

    public void ActionAnim(){
        seqAction = DOTween.Sequence();

        seqAction.AppendCallback(() => SetButtons(false));
        seqAction.AppendInterval(.5f);

        seqAction.Append(actionsTransform.DOMoveY(actionsTransform.position.y - offset, animationDuration)
        .SetEase(Ease.OutQuad));

        seqAction.AppendInterval(idleDuration);
        
        if(gm.active.isPlayer()){
            seqAction.Append(actionsTransform.DOMoveY(actionsTransform.position.y, animationDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() => SetButtons(true)));
        }
    }

    private void SetButtons(bool mode){
        basicAttackButton.interactable = mode;
        if(gm.active.GetComponent<Players>().specialCooldownCounter > 0){
            specialAttackButton.interactable = false;
        }
        else{
            specialAttackButton.interactable = mode;
        }
    }
}
