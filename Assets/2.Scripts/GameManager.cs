using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    [SerializeField]
    private Text current_Time_Text;
    private string current_Time_Hour_Text;
    private string current_Time_Minute_Text;

    [SerializeField]
    private int time_Speed;
    //���� �ð�(��)
    [SerializeField]
    private float current_Time_Hour;
    //���� �ð�(��)
    private float current_Time_Minute;

    //�̱��� ����
    private static Gamemanager S_instance = null;
    public static Gamemanager Instance
    {
        get
        {
            if (S_instance == null)
            {
                S_instance = FindObjectOfType(typeof(Gamemanager)) as Gamemanager;
            }
            return S_instance;
        }
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        Display_Current_Time();
    }

    //���ӿ��� ���� �ð��� ǥ���ϰ��ϴ� �Լ�
    public void Display_Current_Time()
    {
        current_Time_Minute += Time.deltaTime * time_Speed;
        //60���� �Ǹ� 1�ð����� �ٲ�� �����
        if (((int)current_Time_Minute) >= 60)
        {
            current_Time_Minute -= 60;
            current_Time_Hour += 1;
        }
        //���� �ð�(��)�� ǥ��� ����
        if (current_Time_Hour < 10)
        {
            current_Time_Hour_Text = $"0{current_Time_Hour}";
        }
        else current_Time_Hour_Text = $"{current_Time_Hour}";
        //���� �ð�(��)�� ǥ��� ����
        if (current_Time_Minute < 10)
        {
            current_Time_Minute_Text = $"0{(int)current_Time_Minute}";
        }
        else current_Time_Minute_Text = $"{(int)current_Time_Minute}";

        current_Time_Text.text = $"{current_Time_Hour_Text}:{current_Time_Minute_Text}";
    }
}
