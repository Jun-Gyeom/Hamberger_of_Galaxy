using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioClip complete_Clip;

    public AudioClip failed_Clip;

    public AudioClip middle_Clip;

    public AudioMixer main_Audiomixer;

    public AudioMixer effect_Audiomixer;

    public AudioSource effect_AudioSource;


    public void SetMainVolume(float mainSliderValue)
    {

        main_Audiomixer.SetFloat("MainExposedParam", Mathf.Log10(mainSliderValue) * 20);
    }
    public void SetEffectVolume(float effectSliderValue)
    {
        effect_Audiomixer.SetFloat("EffectExposedParam", Mathf.Log10(effectSliderValue) * 20);
    }

   

}
