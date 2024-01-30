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
    //현재 시간(시)
    [SerializeField]
    private float current_Time_Hour;
    //현재 시간(분)
    private float current_Time_Minute;

    //싱글톤 패턴
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

    //게임에서 현재 시간을 표시하게하는 함수
    public void Display_Current_Time()
    {
        current_Time_Minute += Time.deltaTime * time_Speed;
        //60분이 되면 1시간으로 바뀌게 만들기
        if (((int)current_Time_Minute) >= 60)
        {
            current_Time_Minute -= 60;
            current_Time_Hour += 1;
        }
        //현재 시간(분)의 표기법 정의
        if (current_Time_Hour < 10)
        {
            current_Time_Hour_Text = $"0{current_Time_Hour}";
        }
        else current_Time_Hour_Text = $"{current_Time_Hour}";
        //현재 시간(시)의 표기법 정의
        if (current_Time_Minute < 10)
        {
            current_Time_Minute_Text = $"0{(int)current_Time_Minute}";
        }
        else current_Time_Minute_Text = $"{(int)current_Time_Minute}";

        current_Time_Text.text = $"{current_Time_Hour_Text}:{current_Time_Minute_Text}";
    }
}
