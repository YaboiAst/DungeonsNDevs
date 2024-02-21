using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionsManager : MonoBehaviour
{
    private TurnManager tm;

    [Header("Action Buttons References")]
    [SerializeField] private Button basicAttackButton;
    [SerializeField] private Button specialAttackButton;
    private Image basicAttackButtonArt, specialAttackButtonArt;
    private Players activePlayer;

    [Header("Turn Indicator References")]
    [SerializeField] private TextMeshProUGUI turnText;

    [Header("HUD Animation")]
    [SerializeField] private Transform actionAnchor;
    [SerializeField] private float actionOffset;
    [SerializeField] private Transform turnAnchor;
    [SerializeField] private float turnOffset;
    [Space(5)]
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float idleDuration = 0.5f;
    private Sequence seqAction;

    private void Start() {
        tm = TurnManager.instance;

        basicAttackButtonArt = basicAttackButton.GetComponent<Image>();
        specialAttackButtonArt = specialAttackButton.GetComponent<Image>();
    }

    public void SetActivePlayer(Players player){ activePlayer = player; }

    public void UpdatePlayerActionHUD(){
        if(activePlayer == null)
            return;

        seqAction = DOTween.Sequence();
        seqAction.AppendCallback(() => SetActiveName());
        seqAction.JoinCallback(() => SetButtonImage());
        seqAction.AppendInterval(idleDuration);
        seqAction.Append(turnAnchor.DOMoveY(turnAnchor.position.y + turnOffset, animationDuration)
            .SetEase(Ease.InQuad));
        seqAction.Join(actionAnchor.DOMoveY(actionAnchor.position.y + actionOffset, animationDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() => ActivateButtons(true)));
    }

    public void UpdateMonsterActionHUD(){
        if(activePlayer != null)
            return;

        seqAction = DOTween.Sequence();
        seqAction.AppendCallback(() => SetActiveName());
        seqAction.AppendInterval(idleDuration);
        seqAction.Append(turnAnchor.DOMoveY(turnAnchor.position.y + turnOffset, animationDuration)
            .SetEase(Ease.InQuad));
    }

    public void BasicAttackButton(){
        if(activePlayer == null)
            return;

        activePlayer.BasicAttack();
        NextTurnPlayerUpdate();
    }

    public void SpecialAttackButton(){
        if(activePlayer == null)
            return;

        activePlayer.SpecialAttack();
        NextTurnPlayerUpdate();
    }

    private void NextTurnPlayerUpdate(){
        seqAction = DOTween.Sequence();
        seqAction.AppendCallback(() => ActivateButtons(false));
        seqAction.AppendInterval(idleDuration/2f);
        seqAction.Append(turnAnchor.DOMoveY(turnAnchor.position.y - turnOffset, animationDuration)
            .SetEase(Ease.OutQuad));
        seqAction.Join(actionAnchor.DOMoveY(actionAnchor.position.y - actionOffset, animationDuration)
            .SetEase(Ease.OutQuad))
            .OnComplete(() => tm.Next());
    }

    public void NextTurnMonsterUpdate(){
        seqAction = DOTween.Sequence();
        seqAction.AppendInterval(idleDuration/2f);
        seqAction.Append(turnAnchor.DOMoveY(turnAnchor.position.y - turnOffset, animationDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => tm.Next()));
    }

    private void SetButtonImage(){
        basicAttackButtonArt.sprite   = activePlayer.GetPlayer().basicAttackButton;
        specialAttackButtonArt.sprite = activePlayer.GetPlayer().specialAttackButton;
    }

    private void SetActiveName(){
        turnText.text = tm.active.GetEntityName();
        turnText.text += "'s Turn";
    }

    private void ActivateButtons(bool mode){
        if(activePlayer == null){
            basicAttackButton.interactable = false;
            specialAttackButton.interactable = false;
            return;
        }

        basicAttackButton.interactable = mode;
        specialAttackButton.interactable = mode;

        if(activePlayer.specialCooldownCounter > 0){
            specialAttackButton.interactable = false;
        }
    }
}
