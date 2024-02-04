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

    public void SetCompleteAudio()
    {
        effect_AudioSource.clip = complete_Clip;
        effect_AudioSource.Play();
    }

    public void SetFailedAudio()
    {
        effect_AudioSource.clip = failed_Clip;
        effect_AudioSource.Play();
    }

    public void SetMiddleAudio()
    {
        effect_AudioSource.clip = middle_Clip;
        effect_AudioSource.Play();
    }
    /* public AudioManager audioManager;
    public void Guest_Leave()
    {
        // 손님 없음 체크
        is_Come_Guest = false;

        // 손님 실루엣 페이드 아웃
        StartCoroutine(Fade_Out());

        // 재료 맞는지 판정 후 불만족 했다면 돈 차감 ------- (판정 시스템)
        bool ingredients_Check = Ingredients_Check(current_Order, current_Make_Burger_Info);
        if (ingredients_Check)
        {
            // 손님이 만족한 코드
            Debug.Log("만족스러운 햄버거에요!");
            
            if (current_Patience_Value > 5)
            {
                //만족한 오디오 재생
                audioManager.effect_AudioSource.clip = audioManager.complete_Clip;
                audioManager.effect_AudioSource.Play();
            }
            // 인내심이 5칸 아래라면
            if (current_Patience_Value < 5)
            {
                // 돈 차감 식 계산
                float return_Percentage = (5 - current_Patience_Value) * return_Money_Per_Patience_Value;
                float return_Money = burgur_Price * (return_Percentage / 100f);

                money -= return_Money; // 돈 차감
                
                //애매하다는 오디오 재생
                audioManager.effect_AudioSource.clip = audioManager.middle_Clip;
                audioManager.effect_AudioSource.Play();

                // 대화창에 "맛있긴 하지만 너무 오래걸렸어요" 같은 메시지 출력하면 좋을듯)
            }
        }
        else
        {
            // 손님이 만족하지 못한 코드
            Debug.Log("주문한 햄버거가 아니에요..");

            // 만족도 패널티 받고 인내심에 따른 처리.
            float burgur_Panalty = burgur_Price * (burgur_Penalty_Percentage / 100f);

            //불만족한 오디오 재생
            audioManager.effect_AudioSource.clip = audioManager.failed_Clip;
            audioManager.effect_AudioSource.Play();

            money -= burgur_Panalty; // 돈 차감

            // 인내심이 5칸 아래라면
            if (current_Patience_Value < 5)
            {
                // 돈 차감 식 계산
                float return_Percentage = (5 - current_Patience_Value) * return_Money_Per_Patience_Value;
                float return_Money = (burgur_Price - burgur_Panalty) * (return_Percentage / 100f);

                money -= return_Money; // 돈 차감

                // 대화창에 "늦었는데 내가 주문한 음식도 아니야 최악이군" 같은 메시지 출력하면 좋을듯)
            }
        }

        // 딜레이 시간 만큼 후 손님 다시 오게하기.
        StartCoroutine(Come_Guest_Delay());
    }*/

}
