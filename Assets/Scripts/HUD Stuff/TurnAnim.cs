using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnAnim : MonoBehaviour
{
    TurnManager tm;
    private TextMeshProUGUI turnText;
    private Sequence seqTurn;

    public float offset;
    public float animationDuration = 1f;
    public float idleDuration = 0.5f;

    void Start()
    {
        tm = TurnManager.instance;
        tm.disableCombat.AddListener(() => this.enabled = false);
        
        turnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable() {
        if(tm == null)
            return;

        seqTurn = DOTween.Sequence();
        seqTurn.AppendCallback(() => SetActiveName());
        seqTurn.Append(transform.DOMoveY(transform.position.y + offset, animationDuration)
        .SetEase(Ease.OutQuad))
        .OnComplete(() => tm.onPassTurn.AddListener(ChangeTurnAnim));
    }

    private void ChangeTurnAnim(){
        seqTurn = DOTween.Sequence();

        seqTurn.Append(transform.DOMoveY(transform.position.y - offset, animationDuration)
        .SetEase(Ease.OutQuad)
        .OnComplete(() => SetActiveName()));

        seqTurn.AppendInterval(idleDuration);

        seqTurn.Append(transform.DOMoveY(transform.position.y, animationDuration)
        .SetEase(Ease.InQuad));
    }

    private void SetActiveName(){
        GenericEntity active = tm.GetActiveTurn();
        turnText.text = active.GetComponent<GenericEntity>().GetEntityName();
        turnText.text += "'s Turn";
    }
}
