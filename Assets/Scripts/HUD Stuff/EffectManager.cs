//using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private GenericEntity parent;
    private TurnManager gm;

    [Header("Hit")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Vector2 offset;

    [Header("Heal")]
    [SerializeField] private GameObject healEffect;

    [Header("Shield")]
    [SerializeField] private GameObject shieldEffect;

    [Header("Stun")]
    [SerializeField] private GameObject stunEffect;

    private void Start() {
        parent = transform.parent.GetComponentInParent<GenericEntity>();
        gm = TurnManager.instance;

        parent.onHealthChange.AddListener(ChangeHealthEffect);
        parent.onShield.AddListener(ShieldEffect);
        parent.onStun.AddListener(StunEffect);
    }

    private void StunEffect()
    {
        //Instantiate(stunEffect, this.transform);
    }

    private void ShieldEffect()
    {
        //Instantiate(shieldEffect, this.transform);
    }

    private void ChangeHealthEffect(float amount){
        GameObject go;
        if(amount == 0)
            return;

        if(amount > 0)
            go = Instantiate(healEffect, this.transform);
        else
            go = Instantiate(hitEffect, this.transform);

        AnimateEffect(go, amount);
    }

    private void AnimateEffect(GameObject go, float amount){
        go.GetComponent<TextMeshProUGUI>().text = Mathf.Abs(amount).ToString();

        go.transform.DOScale(1f, 0.4f);

        go.transform.DOMoveX(go.transform.position.x + Random.Range(-offset.x, offset.x), 0.4f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.OutQuad);

        go.transform.DOMoveY(go.transform.position.y + offset.y, 0.4f)
        .OnComplete(() => Destroy(go));
    }
    
}
