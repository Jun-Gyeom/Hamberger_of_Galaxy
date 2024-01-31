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
    [Header("시간이 흐르는 속도")]
    [SerializeField]
    private int time_Speed;

    [Space (20f)]
    //현재 시간(시)
    public float current_Time_Hour;
    //현재 시간(분)
    private float current_Time_Minute;
    //영업 시작 시간
    [Header("영업 시작 시간")]
    public float opening_Time;
    //영업 종료 시간
    [Header("영업 종료 시간")]
    [SerializeField]
    private float closing_Time;

    [Space(20f)]
    //현재 일자
    public float current_Date=0;

    //식당 문 닫았는지의 여부
    public bool is_Closed=false;



    // 요리 창이 열려있는지의 여부
    private bool is_On_Cooking_Panel;
    // 요리 창 오브젝트
    [SerializeField]
    private GameObject Cooking_Panel_Object;
    // 요리 창 렉트 트랜스폼
    private RectTransform Cooking_Panel_RectTransform;
    // 요리 창 열고 닫는 속도
    [Header("요리 창 열고 닫는 속도")]
    public float Cooking_Panel_Toggle_Speed;

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

    private void Awake()
    {
        // 해상도 FHD로 고정 및 전체 화면으로 설정
        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        // 요리 창 전환을 위한 렉트 트랜스폼 할당
        Cooking_Panel_RectTransform = Cooking_Panel_Object.GetComponent<RectTransform>();
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

    // 요리 창 열기, 닫기 토글 스위치 코루틴
    IEnumerator Cooking_Panel_Toggle_Coroutine()
    {
        float Cooking_Panel_RectTransform_PosX = Cooking_Panel_RectTransform.anchoredPosition.x;
        // 열기 (요리 창이 닫혀있는 경우)
        if (is_On_Cooking_Panel == false)
        {
            Debug.Log("요리 창 열기");
            while (Cooking_Panel_RectTransform.anchoredPosition.x > 0)
            {
                Cooking_Panel_RectTransform_PosX -= Time.deltaTime * Cooking_Panel_Toggle_Speed;

                Cooking_Panel_RectTransform.anchoredPosition = new Vector2(Cooking_Panel_RectTransform_PosX, 0);
                yield return null;
            }

            Cooking_Panel_RectTransform.anchoredPosition = new Vector2(0, 0);
            is_On_Cooking_Panel = true;
        }
        // 닫기 (요리 창이 열려있는 경우)
        else if (is_On_Cooking_Panel == true)
        {
            Debug.Log("요리 창 닫기");
            while (Cooking_Panel_RectTransform.anchoredPosition.x < 1920)
            {
                Cooking_Panel_RectTransform_PosX += Time.deltaTime * Cooking_Panel_Toggle_Speed;

                Cooking_Panel_RectTransform.anchoredPosition = new Vector2(Cooking_Panel_RectTransform_PosX, 0);
                yield return null;
            }

            Cooking_Panel_RectTransform.anchoredPosition = new Vector2(1920, 0);
            is_On_Cooking_Panel = false;
        }
    }

    // 버튼에 적용시킬 요리 창 열고 닫기 토글 함수
    public void Cooking_Panel_Toggle()
    {
        StartCoroutine("Cooking_Panel_Toggle_Coroutine");
    }
}
