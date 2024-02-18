using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    private GameManager gm;
    
    private Biome doorBiome;
    private Boss doorBoss;
    private Canvas doorTip;

    [SerializeField] private int nEasy, nMedium, nHard;

    [HideInInspector] public UnityEvent onSelectDoor;

    public void SetupBiome(){
        if(gm == null){
            gm = GameManager.instance;
        }
        onSelectDoor.AddListener(UpdateRoom);

        nEasy = (int) Mathf.Floor(1 + 3/gm.roomCounter);
        nMedium = gm.roomCounter;
        nHard = (int) Mathf.Floor(0.25f * gm.roomCounter);

        gm.possibleBiomes.Remove(gm.currentBiome);
        doorBiome = gm.SelectFrom(gm.possibleBiomes);

        GetComponent<SpriteRenderer>().sprite = doorBiome.biomeDoor;
        doorTip = GetComponentInChildren<Canvas>();
    }

    public void SetupBoss(){
        List<Boss> easyBosses = new List<Boss>();
        easyBosses.AddRange(doorBiome.easyBosses);
        List<Boss> mediumBosses = new List<Boss>();
        mediumBosses.AddRange(doorBiome.mediumBosses);
        List<Boss> hardBosses = new List<Boss>();
        hardBosses.AddRange(doorBiome.hardBosses);

        List<Boss> possibleBosses = new List<Boss>();
        int i;

        if(nEasy > doorBiome.easyBosses.Count)
            nEasy = doorBiome.easyBosses.Count;

        if(nMedium > doorBiome.mediumBosses.Count)
            nMedium = doorBiome.mediumBosses.Count;

        if(nHard > doorBiome.hardBosses.Count)
            nHard = doorBiome.hardBosses.Count;

        for(i = 0; i < nEasy; i++){
            gm.AddToListFrom(possibleBosses, easyBosses);
        }

        for(i = 0; i < nMedium; i++){
            gm.AddToListFrom(possibleBosses, mediumBosses);
        }

        for(i = 0; i < nHard; i++){
            gm.AddToListFrom(possibleBosses, hardBosses);
        }

        doorBoss = gm.SelectFrom(possibleBosses);
        doorTip.GetComponentInChildren<TextMeshProUGUI>(true).text = doorBoss.bossDescription;
    }

    public void UpdateRoom(){
        gm.currentBiome = doorBiome;
        gm.currentBoss = doorBoss;
        Debug.Log(gm.currentBiome.biomeScene);
        gm.onChangeRoom?.Invoke(gm.currentBiome.biomeScene);
    }

    public void OpenTip(){
        Image tooltipImage = doorTip.GetComponentInChildren<Image>(true);
        TextMeshProUGUI[] tooltipText = doorTip.GetComponentsInChildren<TextMeshProUGUI>(true);

        Sequence seq = DOTween.Sequence();
        tooltipImage.transform.localScale = Vector3.zero;
        seq.AppendCallback(() => tooltipImage.gameObject.SetActive(true));
        seq.Append(tooltipImage.transform.DOScale(1f, 0.5f)
        .SetEase(Ease.InSine)
        .OnComplete(() => ActivateText(tooltipText, true)));
    }

    public void CloseTip(){
        Image tooltipImage = doorTip.GetComponentInChildren<Image>(true);
        TextMeshProUGUI[] tooltipText = doorTip.GetComponentsInChildren<TextMeshProUGUI>(true);

        Sequence seq = DOTween.Sequence();
        tooltipImage.transform.localScale = Vector3.one;
        seq.AppendCallback(() => ActivateText(tooltipText, false));
        seq.Append(tooltipImage.transform.DOScale(0f, 0.5f)
        .SetEase(Ease.OutSine)
        .OnComplete(() => tooltipImage.gameObject.SetActive(false)));
    }

    private void ActivateText(TextMeshProUGUI[] text, bool mode){
        foreach(TextMeshProUGUI t in text){
            t.gameObject.SetActive(mode);
        }
    }
}
