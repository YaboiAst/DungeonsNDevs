using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TurnAnim : MonoBehaviour
{
    GameManager gm;
    private TextMeshProUGUI turnText;
    private Sequence seq;

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
        seq = DOTween.Sequence();
        seq.AppendInterval(1f);

        seq.Append(transform.DOMoveY(transform.position.y - offset, animationDuration)
        .SetEase(Ease.OutQuad)
        .OnComplete(() => SetActiveName()));

        seq.AppendInterval(idleDuration);

        seq.Append(transform.DOMoveY(transform.position.y, animationDuration)
        .SetEase(Ease.InQuad));
    }

    private void SetActiveName(){
        GenericEntity active = gm.GetActiveTurn();
        turnText.text = active.GetComponent<GenericEntity>().GetEntityName();
        turnText.text += "'s Turn";
    }
}
