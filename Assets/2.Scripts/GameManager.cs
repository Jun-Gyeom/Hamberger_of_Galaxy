using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    //현재 시간 텍스트
    [SerializeField]
    private Text current_Time_Text;
    private string current_Time_Hour_Text;
    private string current_Time_Minute_Text;
    //현재 날짜 텍스트
    [SerializeField]
    private Text current_Date_Number_Text;

    //결과 화면
    public GameObject result_Panel;

   

    //시간이 흐르는 속도
    [SerializeField]
    private int time_Speed;
    //현재 시간(시)
    public float current_Time_Hour;
    //현재 시간(분)
    private float current_Time_Minute;
    //영업 시작 시간
    public float opening_Time;
    //영업 종료 시간
    [SerializeField]
    private float closing_Time;
    //현재 일자
    public float current_Date=0;

    //식당 문 닫았는지의 여부
    public bool is_Closed=false;

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
        //게임에서 현재 시간을 표시하게하는 함수
        Display_Current_Time();
        //가게의 문을 닫았는지의 여부를 확인하는 함수
        Check_Is_Closed();
    

    }

    //가게의 문을 닫았는지의 여부를 확인하는 함수
    public void Check_Is_Closed()
    {
        //현재 시간이 종료 시간과 같으면 bool 타입의 변수를 true로 바꿈
        if (current_Time_Hour == closing_Time)
        {
            is_Closed = true;
            //결과창을 띄운다
            result_Panel.SetActive(true);
        }
        //문을 닫았을 때 시간을 멈추기
        if (is_Closed==true)
        {
            current_Time_Hour = closing_Time;
            current_Time_Minute = 0;
        }
    }

   

    //게임에서 현재 시간을 표시하게하는 함수
    public void Display_Current_Time()
    {
        //현재 시간의 분을 정한다
        current_Time_Minute += Time.deltaTime * time_Speed;

        //60분이 되면 1시간으로 바뀌게 만들기
        if (((int)current_Time_Minute) >= 60)
        {
            current_Time_Minute -= 60;
            current_Time_Hour += 1;
        }

        //현재 시간(분)의 표기법 정의
        if (current_Time_Hour < 10 && is_Closed==false)
        {
            current_Time_Hour_Text = $"0{current_Time_Hour}";
        }
        else current_Time_Hour_Text = $"{current_Time_Hour}";

        //현재 시간(시)의 표기법 정의
        if (current_Time_Minute < 10 && is_Closed == false)
        {
            current_Time_Minute_Text = $"0{(int)current_Time_Minute}";
        }
        else current_Time_Minute_Text = $"{(int)current_Time_Minute}";

        //현재 시간과 날짜를 텍스트로 표시한다
        current_Time_Text.text = $"{current_Time_Hour_Text}:{current_Time_Minute_Text}";
        current_Date_Number_Text.text = current_Date.ToString();
    }
}
