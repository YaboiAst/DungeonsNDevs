using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TooltipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    enum TooltipType {BASIC, SPECIAL};

    [SerializeField] private GameObject tooltipCanva;
    [SerializeField] private TooltipType type;

    private TextMeshProUGUI tooltipText;
    private GameManager gm;

    private Button thisButton;
    private string moveName, moveDescription;
    
    private void Start() {
        gm = GameManager.instance;
        gm.onPassTurn.AddListener(UpdateTooltips);

        thisButton = GetComponent<Button>();

        tooltipText = tooltipCanva.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void SetMoveType(){
        Character character = gm.active.GetCharacter();
        switch(type){
            case TooltipType.BASIC:
                moveName = character.basicAttackName;
                moveDescription = character.basicAttackDescription;
                break;
            
            case TooltipType.SPECIAL:
                moveName = character.specialAttackName;
                moveDescription = character.specialAttackDescription;
                break;
        }
    }

    private void UpdateTooltips(){
        SetMoveType();

        tooltipText.text = 
        "<b>" + moveName + "</b> \n" +
        moveDescription;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(!thisButton.interactable)
            return;


        tooltipCanva.transform.localScale = Vector3.one * 0.01f;
        tooltipCanva.SetActive(true);
        tooltipCanva.transform.DOScale(1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseTooltip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CloseTooltip();
    }

    public void CloseTooltip(){
        if(!tooltipCanva.activeSelf)
            return;

        tooltipCanva.transform.DOScale(0.01f, 0.2f);
        tooltipCanva.SetActive(false);
    }
}
