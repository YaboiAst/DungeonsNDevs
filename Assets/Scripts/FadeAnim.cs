using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeAnim : MonoBehaviour
{
    private GameManager gm = null;
    private Image fadeImage = null;
    [SerializeField] private Vector2 fadeInAndOutDuration = new(0.5f, 0.5f);
    [SerializeField] private float intervalDuration = 0.5f;

    void Start(){
        if(fadeImage == null){
            fadeImage = GetComponentInChildren<Image>(true);
        }

        if(gm == null){
            gm = GameManager.instance;
        }

        fadeImage.gameObject.SetActive(false);
        gm.onStartCombat.AddListener(GoToCombat);
    }

    public void GoToCombat(){
        Sequence seq = DOTween.Sequence();
        fadeImage.gameObject.SetActive(true);
        fadeImage.fillAmount = 0f;
        fadeImage.color = Color.black;
        seq.Append(fadeImage.DOFillAmount(1f, fadeInAndOutDuration.x)
            .SetEase(Ease.OutSine)
            .OnComplete(() => gm.SwitchVisions()));

        seq.AppendInterval(intervalDuration);
        
        seq.Append(fadeImage.DOFillAmount(0f, fadeInAndOutDuration.x)
            .SetEase(Ease.InSine)
            .OnComplete(() => fadeImage.gameObject.SetActive(false)));
    }
}
