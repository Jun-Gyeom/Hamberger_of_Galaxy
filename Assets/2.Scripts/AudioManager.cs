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
        // �մ� ���� üũ
        is_Come_Guest = false;

        // �մ� �Ƿ翧 ���̵� �ƿ�
        StartCoroutine(Fade_Out());

        // ��� �´��� ���� �� �Ҹ��� �ߴٸ� �� ���� ------- (���� �ý���)
        bool ingredients_Check = Ingredients_Check(current_Order, current_Make_Burger_Info);
        if (ingredients_Check)
        {
            // �մ��� ������ �ڵ�
            Debug.Log("���������� �ܹ��ſ���!");
            
            if (current_Patience_Value > 5)
            {
                //������ ����� ���
                audioManager.effect_AudioSource.clip = audioManager.complete_Clip;
                audioManager.effect_AudioSource.Play();
            }
            // �γ����� 5ĭ �Ʒ����
            if (current_Patience_Value < 5)
            {
                // �� ���� �� ���
                float return_Percentage = (5 - current_Patience_Value) * return_Money_Per_Patience_Value;
                float return_Money = burgur_Price * (return_Percentage / 100f);

                money -= return_Money; // �� ����
                
                //�ָ��ϴٴ� ����� ���
                audioManager.effect_AudioSource.clip = audioManager.middle_Clip;
                audioManager.effect_AudioSource.Play();

                // ��ȭâ�� "���ֱ� ������ �ʹ� �����ɷȾ��" ���� �޽��� ����ϸ� ������)
            }
        }
        else
        {
            // �մ��� �������� ���� �ڵ�
            Debug.Log("�ֹ��� �ܹ��Ű� �ƴϿ���..");

            // ������ �г�Ƽ �ް� �γ��ɿ� ���� ó��.
            float burgur_Panalty = burgur_Price * (burgur_Penalty_Percentage / 100f);

            //�Ҹ����� ����� ���
            audioManager.effect_AudioSource.clip = audioManager.failed_Clip;
            audioManager.effect_AudioSource.Play();

            money -= burgur_Panalty; // �� ����

            // �γ����� 5ĭ �Ʒ����
            if (current_Patience_Value < 5)
            {
                // �� ���� �� ���
                float return_Percentage = (5 - current_Patience_Value) * return_Money_Per_Patience_Value;
                float return_Money = (burgur_Price - burgur_Panalty) * (return_Percentage / 100f);

                money -= return_Money; // �� ����

                // ��ȭâ�� "�ʾ��µ� ���� �ֹ��� ���ĵ� �ƴϾ� �־��̱�" ���� �޽��� ����ϸ� ������)
            }
        }

        // ������ �ð� ��ŭ �� �մ� �ٽ� �����ϱ�.
        StartCoroutine(Come_Guest_Delay());
    }*/

}
