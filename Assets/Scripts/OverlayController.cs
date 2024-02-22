using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{
    [HideInInspector] public static OverlayController instance;
    private GameManager gm = null;

    [Header("Fade Image Ref")]
    [SerializeField] private Image fadeImage = null;

    [Header("Transition Image Ref")]
    [SerializeField] private Image transitionImage = null;

    [Header("Overlay Settings")]
    [SerializeField] private Vector2 fadeInAndOutDuration = new(0.5f, 0.5f);
    [SerializeField] private float intervalDuration = 0.5f;
    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    void Start(){
        gm = GameManager.instance;

        FadeIn(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        SceneManager.sceneLoaded += FadeIn;
        transitionImage.gameObject.SetActive(false);

        gm.onChangeRoom.AddListener(NextScene);
        gm.onStartCombat.AddListener(Transition);
        gm.onReturnDungeon.AddListener(Transition);
    }

    public void Transition(){
        Sequence seq = DOTween.Sequence();
        transitionImage.gameObject.SetActive(true);
        transitionImage.fillAmount = 0f;
        transitionImage.color = Color.black;
        seq.Append(transitionImage.DOFillAmount(1f, fadeInAndOutDuration.x)
            .SetEase(Ease.OutSine)
            .OnComplete(() => gm.SwitchVisions()));

        seq.AppendInterval(intervalDuration);
        
        seq.Append(transitionImage.DOFillAmount(0f, fadeInAndOutDuration.x)
            .SetEase(Ease.InSine)
            .OnComplete(() => transitionImage.gameObject.SetActive(false)));
    }

    public void FadeIn(Scene scene, LoadSceneMode mode){
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;
        fadeImage.DOFade(0f, fadeInAndOutDuration.x).SetEase(Ease.InSine).OnComplete(() => fadeImage.gameObject.SetActive(false));
    }

    public void NextScene(string nextScene){
        if(nextScene == "GameOver"){
            fadeInAndOutDuration.y *= 2;
        }

        fadeImage.color = Color.black - Color.black;
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1f, fadeInAndOutDuration.y).SetEase(Ease.OutSine).OnComplete(() => gm.NextRoom(nextScene));
    }
}

