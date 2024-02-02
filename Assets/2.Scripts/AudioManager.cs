using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SetValure : MonoBehaviour
{
    public AudioMixer audiomixer;

    public void SetVolume(float sliderValue)
    {
        audiomixer.SetFloat("MainExposedParam", Mathf.Log10(sliderValue) * 20);
    }
}
