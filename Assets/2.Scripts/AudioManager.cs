using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioClip complete_Clip;

    public AudioClip failed_Clip;

    public AudioClip middle_Clip;

    public AudioClip bgm_main; // 메인 BGM 오디오 클립
    public AudioClip bgm_ending; // 엔딩 BGM 오디오 클립

    public AudioMixer bgm_Audiomixer; // 배경음악 믹서

    public AudioMixer effect_Audiomixer; // 효과음 믹서

    public AudioSource effect_AudioSource; // 효과음 오디오소스
    public AudioSource bgm_AudioSource; // 배경음악 오디오소스


    public void SetMainVolume(float mainSliderValue)
    {

        bgm_Audiomixer.SetFloat("MainExposedParam", Mathf.Log10(mainSliderValue) * 20);
    }
    public void SetEffectVolume(float effectSliderValue)
    {
        effect_Audiomixer.SetFloat("EffectExposedParam", Mathf.Log10(effectSliderValue) * 20);
    }

   

}
