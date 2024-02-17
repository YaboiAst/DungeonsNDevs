using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private GenericEntity entity;
    private Character cInfo;

    [Header("Visual")]
    [SerializeField] private Image iconUI;
    [SerializeField] private TextMeshProUGUI nameUI;
    
    [SerializeField] private Image healthUI;
    [SerializeField] private TextMeshProUGUI healthIndicatorUI;

    private void Start() {
        entity = GetComponentInParent<GenericEntity>();
        cInfo = entity.GetCharacter();
        entity.onHealthChange.AddListener(UpdateUI);

        if(entity.isPlayer()){
            iconUI.sprite = entity.GetPlayer().classIcon;
        }
        
        nameUI.text = cInfo.characterName;
    }

    private void UpdateUI(float amount){
        healthIndicatorUI.text = entity.currentHealth.ToString() + "/" + cInfo.maxHealth.ToString();
        healthUI.DOFillAmount(entity.currentHealth / cInfo.maxHealth, 0.3f);
    }
}
