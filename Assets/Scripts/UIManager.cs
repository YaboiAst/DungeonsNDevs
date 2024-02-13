using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GenericEntity entity;

    [Header("Visual")]
    [SerializeField] private Image iconUI;
    [SerializeField] private TextMeshProUGUI nameUI;
    
    [SerializeField] private Image healthUI;
    [SerializeField] private TextMeshProUGUI healthIndicatorUI;

    private void Start() {
        entity = GetComponentInParent<GenericEntity>();
        entity.onHealthChange.AddListener(UpdateUI);

        iconUI.sprite = entity.cInfo.classIcon;
        nameUI.text = entity.cInfo.characterName;
        Invoke("UpdateUI", 0.1f);
    }

    private void UpdateUI(){
        healthIndicatorUI.text = entity.currentHealth.ToString() + "/" + entity.cInfo.maxHealth.ToString();
        healthUI.fillAmount = entity.currentHealth / entity.cInfo.maxHealth;
    }
}
