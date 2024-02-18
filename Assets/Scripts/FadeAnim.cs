using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeAnim : MonoBehaviour
{
    [HideInInspector] public static FadeAnim instance;

    private GameManager gm = null;
    private Image fadeImage = null;
    [SerializeField] private Vector2 fadeInAndOutDuration = new(0.5f, 0.5f);

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    void Start(){
        if(fadeImage == null){
            fadeImage = GetComponentInChildren<Image>(true);
        }

        FadeIn(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        SceneManager.sceneLoaded += FadeIn;
    }

    private void OnEnable() {
        if(gm == null){
            gm = GameManager.instance;
        }
        gm.onChangeRoom.AddListener(NextScene);
    }

    public void FadeIn(Scene scene, LoadSceneMode mode){
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;
        fadeImage.DOFade(0f, fadeInAndOutDuration.x).SetEase(Ease.InSine).OnComplete(() => fadeImage.gameObject.SetActive(false));
    }

    public void NextScene(string nextScene){
        Debug.Log("Chegou no NextScene");
        if(nextScene == "GameOver"){
            fadeInAndOutDuration.y *= 2;
        }

        fadeImage.color = Color.black - Color.black;
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1f, fadeInAndOutDuration.y).SetEase(Ease.OutSine).OnComplete(() => gm.NextRoom(nextScene));
    }
}

