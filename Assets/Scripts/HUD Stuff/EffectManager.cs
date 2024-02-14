//using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private GenericEntity parent;
    private GameManager gm;

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
        parent = GetComponentInParent<GenericEntity>();
        gm = GameManager.instance;

        parent.onTakeDamage.AddListener(TakeDamageEffect);
        parent.onHealDamage.AddListener(HealDamageEffect);
        parent.onShield.AddListener(ShieldEffect);
        parent.onStun.AddListener(StunEffect);
    }

    private void StunEffect()
    {
        Instantiate(stunEffect, this.transform);
    }

    private void ShieldEffect()
    {
        Instantiate(shieldEffect, this.transform);
    }

    private void HealDamageEffect()
    {
        Instantiate(healEffect, this.transform);
    }

    /// <summary>
    /// TODO
    ///     Find some way to get the amount of damage per event
    ///         UnityEvent parameters?
    /// </summary>

    private void TakeDamageEffect()
    {
        GameObject go = Instantiate(hitEffect, this.transform);
        go.GetComponent<TextMeshProUGUI>().text = gm.active.cInfo.basicAttack.ToString();

        go.transform.DOScale(1f, 0.4f);

        go.transform.DOMoveX(go.transform.position.x + Random.Range(-offset.x, offset.x), 0.4f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.OutQuad);

        go.transform.DOMoveY(go.transform.position.y + offset.y, 0.4f)
        .OnComplete(() => Destroy(go));
    }
    
}
