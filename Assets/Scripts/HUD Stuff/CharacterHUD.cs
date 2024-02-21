using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CharacterHUD : MonoBehaviour
{
    private GenericEntity entity;
    private Character cInfo;

    [Header("Visual")]
    [SerializeField] private TextMeshProUGUI nameUI;
    
    [SerializeField] private Image healthUI;
    [SerializeField] private TextMeshProUGUI healthIndicatorUI;

    private void Start() {
        entity = GetComponentInParent<GenericEntity>();
        cInfo = entity.GetCharacter();
        entity.onHealthChange.AddListener(UpdateUI);
        
        nameUI.text = cInfo.characterName;
        UpdateUI();
    }

    private void UpdateUI(float amount = 0){
        healthIndicatorUI.text = entity.currentHealth.ToString() + "/" + cInfo.maxHealth.ToString();
        healthUI.DOFillAmount(entity.currentHealth / cInfo.maxHealth, 0.3f);
    }
}
