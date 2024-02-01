using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;

public class GameManager : MonoBehaviour
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
    //일시 정지 화면
    public GameObject pause_Panel;




    //시간이 흐르는 속도
    [Header("시간이 흐르는 속도")]
    [SerializeField]
    private int time_Speed;

    [Space (20f)]
    //현재 시간(시)
    public float current_Time_Hour;
    //현재 시간(분)
    public float current_Time_Minute;
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
    //일시정지 여부
    public bool is_Paused=false;



    // 가게 레벨 (재료 업그레이드 레벨)
    public int shop_Level;
    // 가게 업그레이드 비용 배열
    [Header("가게 업그레이드 비용")]
    public float[] shop_Upgrade_Cost;
    [Space(20f)]
    // 소지한 돈
    public float money;
    // 가게 업그레이드 비용 텍스트
    [SerializeField]
    private TMP_Text shop_Upgrade_Cost_Text;
    // 업그레이드 창
    public GameObject upgread_Panel;
    // 돈 텍스트
    public TMP_Text money_Text;
    // 가게 업그레이드 버튼
    public Button shop_Upgrade_Button;
    // 재료 스크립트 배열
    public Ingredient[] ingredients;
    // 해금되는 재료 오브젝트 배열
    public GameObject[] unlock_Ingredient_Object;
    // 해금되는 재료 이미지 배열
    public Image[] unlock_Ingredient_Image;
    // 해금되는 재료 이름 배열
    public TMP_Text[] unlock_Ingredient_Name;

    // 햄버거 높이
    [Header("햄버거 최대 높이 (윗면 빵 제외)")]
    [SerializeField]
    public int max_Burgur_Height;
    [Space(20f)]
    // 현재 올려야할 햄버거 재료 위치(높이)
    public int current_Burgur_Height;
    // 현재 버거 정보(들어간 재료) 배열
    public int[] current_Make_Burger_Info;
    // 버거 재료 들어갈 오브젝트 배열
    public GameObject[] burgur_Ingredient_Object;
    // 버거 재료 들어갈 이미지 배열
    public Image[] burgur_Ingredient_Image;

    // 현재 주문을 다 받았는지 체크
    public bool is_End_Current_Order;
    // 햄버거 재료를 선택 중인지 체크
    public bool is_Select_Ingredient;
    // 재료 선택 시 화면 강조 효과 패널
    public GameObject ingredient_Shadow_Panel;
    // 요리 창 닫기 코루틴 실행 여부
    public bool is_Cooking_Panel_Closing_Coroutine;
    // 현재 선택 중인 재료 위치 (햄버거에서의 높이)
    private int current_Select_Ingredient_Height;

    //싱글톤 패턴
    private static GameManager S_instance = null;
    public static GameManager Instance
    {
        get
        {
            if (S_instance == null)
            {
                S_instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return S_instance;
        }
    }
    private void Awake()
    {
        //해상도 초기 설정 FHD로 고정
        Screen.SetResolution(1920, 1080, true);
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
        //일시 정지를 확인하는 함수
        CheckPause();
        // 소지금 텍스트 반영
        money_Text.text = $"{money}원";




        // 테스트 중 ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        /*
        if (Input.GetKeyUp(KeyCode.F))
        {
            int index = Array.IndexOf(current_Make_Burger_Info, (int)Type.cheese);
            if (index > -1)
            {
                Debug.Log($"{Type.cheese}의 재료는 {index + 1}번째에 있다.");

                current_Make_Burger_Info[index] = 0;
            }
            else
            {
                Debug.Log("해당 재료는 햄버거에 없다.");
            }
        }


        if (Input.GetKeyUp(KeyCode.F))
        {
            for (int i = 0; i < current_Make_Burger_Info.Length; i++)
            {
                Debug.Log($"{current_Make_Burger_Info[i]}");
            }
        }
        */
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

            // 가게 레벨에 따른 업그레이드 창을 띄우는 함수 실행
            Upgrade_Panel_Open();
        }
        //문을 닫았을 때 시간을 멈추기
        if (is_Closed==true)
        {
            current_Time_Hour = closing_Time;
            current_Time_Minute = 0;
        }
    }

    // 가게 레벨에 따른 업그레이드 창 띄우는 함수
    void Upgrade_Panel_Open()
    {
        // 업그레이드 창 띄우기
        upgread_Panel.SetActive(true);


        // 재료가 가게 레벨 몇에 사용가능한지에 따라 업그레이드 시 얻을 수 있는 재료 표시
        int unlock_Num = 0; // 해금 되는 재료 갯수
        unlock_Ingredient_Object[0].SetActive(false);
        unlock_Ingredient_Object[1].SetActive(false);
        unlock_Ingredient_Object[2].SetActive(false);

        for (int i = 0; i < ingredients.Length; i++)
        {
            // 해금 될 재료가 있다면
            if (ingredients[i].ingredient.available_Shop_Level == shop_Level + 1)
            {
                unlock_Ingredient_Image[unlock_Num] = ingredients[i].ingredient_Sprite; // 아이콘 변경
                unlock_Ingredient_Name[unlock_Num].text = ingredients[i].ingredient.ingredients_Name; // 이름 변경
                unlock_Ingredient_Object[unlock_Num].SetActive(true);
                unlock_Num++;
            }
        }

        // 업그레이드 비용 표시
        if (shop_Level == shop_Upgrade_Cost.Length)
        {
            shop_Upgrade_Cost_Text.text = "업그레이드 완료";
            shop_Upgrade_Button.interactable = false; // 가게 업그레이드 버튼 비활성화
            return;
        }

        shop_Upgrade_Cost_Text.text = shop_Upgrade_Cost[shop_Level].ToString();
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

    //일시 정지 체크
    public void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&is_Paused==false)
        {
            pause_Panel.SetActive(true);
            is_Paused = true;
            Time.timeScale = 0;
        }
    }

    // 햄버거 제작 시스템
    public void Cook_Hamburger(Ingredients ingredient)
    {
        // 재료가 가득 찼을 때
        if (current_Burgur_Height >= max_Burgur_Height)
        {
            // 재료 넣지 않고
            Debug.Log("재료 가득 참.");

            // 근데 재료가 윗면 빵이라면
            if (ingredient.type == Type.Top_Bun)
            {
                Debug.Log("윗면빵 넣기!");
                burgur_Ingredient_Image[current_Burgur_Height].sprite = ingredient.ingredients_Sprite; // 재료 이미지 변경
                burgur_Ingredient_Object[current_Burgur_Height].SetActive(true); ; // 재료 활성화
                current_Make_Burger_Info[current_Burgur_Height] = (int)ingredient.type; // 실질적 데이터 적용

                current_Burgur_Height++;
            }

            // 예외처리 (재료 스왑은 가득 차도 가능)
            if (!is_Select_Ingredient)
            {
                return; // 리턴
            }
        }

        // 요리 완료 버튼 눌렀다면
        if (is_Cooking_Panel_Closing_Coroutine)
        {
            return;
        }

        // 재료 선택 중이었다면 (치환, 스왑 기능)
        if (is_Select_Ingredient)
        {
            burgur_Ingredient_Image[current_Select_Ingredient_Height].sprite = ingredient.ingredients_Sprite; // 재료 이미지 변경
            burgur_Ingredient_Object[current_Select_Ingredient_Height].SetActive(true); ; // 재료 활성화
            current_Make_Burger_Info[current_Select_Ingredient_Height] = (int)ingredient.type; // 실질적 데이터 적용

            // 재료 선택 해제(강조 해제)
            Sorting_Ingredient_Objects();

            return;
        }

        // 재료 넣을 공간이 있다면
        for (int i = 0; i < max_Burgur_Height; i++)
        {
            Debug.Log("재료 넣을 공간 확인 중...");
            if (!burgur_Ingredient_Object[i].activeSelf)
            {
                Debug.Log("재료 넣기!");
                burgur_Ingredient_Image[current_Burgur_Height].sprite = ingredient.ingredients_Sprite; // 재료 이미지 변경
                burgur_Ingredient_Object[current_Burgur_Height].SetActive(true); ; // 재료 활성화
                current_Make_Burger_Info[current_Burgur_Height] = (int)ingredient.type; // 실질적 데이터 적용

                current_Burgur_Height++;
                return;
            }
        }
    }

    // 햄버거 재료 하이어라키 순서 정렬 함수
    public void Sorting_Ingredient_Objects()
    {
        for (int i = 0; i < burgur_Ingredient_Object.Length; i++)
        {
            ingredient_Shadow_Panel.SetActive(false); // 강조 패널 비활성화
            burgur_Ingredient_Object[i].transform.SetSiblingIndex(i);
        }
        is_Select_Ingredient = false;
    }

    // 햄버거 재료 선택 함수
    public void Select_Ingredient(int num)
    {
        // 이미 다른 재료 선택 중이면 (또는 요리 완료 버튼 눌렀다면) 리턴
        if (is_Select_Ingredient || is_Cooking_Panel_Closing_Coroutine)
        {
            return;
        }

        // 재료 선택
        ingredient_Shadow_Panel.SetActive(true); // 강조 패널 활성화
        burgur_Ingredient_Object[num].gameObject.transform.SetAsLastSibling();
        is_Select_Ingredient = true;
        current_Select_Ingredient_Height = num;
    }
}
