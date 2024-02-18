using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public enum SoundType { MUSIC, SFX };

    [Serializable]
    public class SoundMap { 
        public string name;
        public AudioSource source;

        public SoundType type;
    }

    public static AudioManager Instance = null;
    [SerializeField] private List<SoundMap> soundMap;
    [ShowNonSerializedField] private string currentMusic = "";

    private bool isMusicActive = true, isSFXActive = true;

    void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void Play(string name) {
        var sMap = soundMap.Find((map) => map.name == name);

        if(sMap != null){
            if(sMap.type == SoundType.MUSIC && isMusicActive){
                if(sMap.name != currentMusic) {
                    if(currentMusic != "") {
                        var stopSMap = soundMap.Find((map) => map.name == currentMusic);
                        stopSMap?.source.Stop();
                    }

                    currentMusic = sMap.name;
                    sMap.source.Play();
                }
            } else {
                if(isSFXActive)
                    sMap.source.Play();
            }
        }
    }

    public void Stop(){
        var sMap = soundMap.Find((map) => map.name == currentMusic);

        if(sMap != null){
            sMap.source.Stop();
        }
    }

    public void MuteMusic(bool mute){
        isMusicActive = mute;
    }

    public void MuteSFX(bool mute){
        isSFXActive = mute;
    }
}
