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

        seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(transform.position.y - offset, animationDuration)
        .SetEase(Ease.OutQuad)
        .OnComplete(() => SetActiveName()));
        seq.AppendInterval(idleDuration);
        seq.Append(transform.DOMoveY(transform.position.y, animationDuration)
        .SetEase(Ease.InQuad));

        turnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void ChangeTurnAnim(){
        seq.Play();
    }

    private void SetActiveName(){
        Transform active = gm.GetActiveTurn();
        turnText.text = active.GetComponent<Players>().GetPlayerName();
        turnText.text += "'s Turn";
    }
}
