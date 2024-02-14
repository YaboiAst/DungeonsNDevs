using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnAnim : MonoBehaviour
{
    GameManager gm;
    private TextMeshProUGUI turnText;
    private Sequence seqTurn;

    public float offset;
    public float animationDuration = 1f;
    public float idleDuration = 0.5f;

    void Start()
    {
        gm = GameManager.instance;
        gm.onPassTurn.AddListener(ChangeTurnAnim);

        turnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void ChangeTurnAnim(){
        seqTurn = DOTween.Sequence();

        seqTurn.AppendInterval(.5f);

        seqTurn.Append(transform.DOMoveY(transform.position.y - offset, animationDuration)
        .SetEase(Ease.OutQuad)
        .OnComplete(() => SetActiveName()));

        seqTurn.AppendInterval(idleDuration);

        seqTurn.Append(transform.DOMoveY(transform.position.y, animationDuration)
        .SetEase(Ease.InQuad));
    }

    private void SetActiveName(){
        GenericEntity active = gm.GetActiveTurn();
        turnText.text = active.GetComponent<GenericEntity>().GetEntityName();
        turnText.text += "'s Turn";
    }
}
