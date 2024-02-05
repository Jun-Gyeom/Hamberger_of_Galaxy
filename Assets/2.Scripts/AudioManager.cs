using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioClip complete_Clip;

    public AudioClip failed_Clip;

    public AudioClip middle_Clip;

    public AudioClip bgm_main; // ���� BGM ����� Ŭ��
    public AudioClip bgm_ending; // ���� BGM ����� Ŭ��

    public AudioMixer bgm_Audiomixer; // ������� �ͼ�

    public AudioMixer effect_Audiomixer; // ȿ���� �ͼ�

    public AudioSource effect_AudioSource; // ȿ���� ������ҽ�
    public AudioSource bgm_AudioSource; // ������� ������ҽ�


    public void SetMainVolume(float mainSliderValue)
    {

        bgm_Audiomixer.SetFloat("MainExposedParam", Mathf.Log10(mainSliderValue) * 20);
    }
    public void SetEffectVolume(float effectSliderValue)
    {
        effect_Audiomixer.SetFloat("EffectExposedParam", Mathf.Log10(effectSliderValue) * 20);
    }

   

}
