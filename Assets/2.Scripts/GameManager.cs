using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System;

public class GameManager : MonoBehaviour
{
    // 오더매니저 스크립트
    public OrderManager order_Manager;
    // 토크매니저 스크립트
    public TalkManager talk_Manager;

    //현재 시간 텍스트
    [SerializeField]
    private TMP_Text current_Time_Text;
    private string current_Time_Hour_Text;
    private string current_Time_Minute_Text;
    //현재 날짜 텍스트
    [SerializeField]
    private TMP_Text current_Date_Number_Text;

    //결과 화면
    public GameObject result_Panel;
    //일시 정지 화면
    public GameObject pause_Panel;
    //게임 오버 화면
    public GameObject gameover_Panel;
    //게임 클리어 화면
    public GameObject ending_Panel;

    [SerializeField]
    private int ingredient_Money;


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
    public int current_Date = 0;

    //식당 문 닫았는지의 여부
    public bool is_Closed=false;
    //일시정지 여부
    public bool is_Paused=false;
    //게임 오버 여부
    public bool is_Gameover=false;


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
    public GameObject upgrade_Panel;
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

    [Header("손님 시스템 관련 변수")]
    // 손님 얼굴 이미지
    public Image guest_Face_Image;
    // 손님 얼굴 스프라이트 배열
    public Sprite[] guest_Faces;
    // 손님 실루엣 이미지
    public Image guest_Silhoutte_Image;
    // 손님 실루엣 페이드 속도
    public float fade_Speed;
    // 현재 손님 인내심 게이지 수치
    public float current_Patience_Value;
    // 손님 인내심 게이지 오브젝트 배열
    public GameObject[] patience_Gauge_Objects;
    // 타이머
    float timer;
    // 인내심 게이지 한 칸 닳는 시간 간격
    public float patience_Decrease_Time;
    // 현재 화면 요리 창인지 체크
    public bool is_On_Cooking_Panel;
    // 현재 손님이 요리를 기다리는 중인지 체크 (ture일 때, 인내심 게이지 닳아야 함)
    public bool is_Guest_Waiting;
    // 대화 창 타이핑 이펙트
    public Type_Effect talk;
    // 햄버거 가격 설정
    [Header("햄버거 가격")]
    [SerializeField]
    public float burgur_Price;
    [Space(20f)]
    // 손님 다시 오는 시간 간격 --- (이후에 날마다 난이도 조절로 다르게 하려면 배열로 함 될듯.
    [Header("손님 오는 시간 간격")] 
    [SerializeField]
    public float come_Guest_Delay_Time;
    [Space(20f)]
    // 현재 받고있는 주문
    private Order current_Order;

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
        // 손님 오는 함수 ---------------------------------------------------- (게임 스타트, 위치 바꿀 가능성 큼.)
        Guest_Come();
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
        // 손님 인내심 게이지
        Patience_Gauge_Decrease();

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Fade_Out());
        }
    }

    //가게의 문을 닫았는지의 여부를 확인하는 함수
    public void Check_Is_Closed()
    {
        
        //현재 시간이 종료 시간과 같으면 bool 타입의 변수를 true로 바꿈
        if (current_Time_Hour == closing_Time)
        {
            is_Closed = true;
        }
        if (is_Closed==true)
        {
            //결과창을 띄운다
            result_Panel.SetActive(true);

            // 가게 레벨에 따른 업그레이드 창을 띄우는 함수 실행
            Upgrade_Panel_Open();

            //재료비 차감
            money -= ingredient_Money;

            //게임 오버여부
            if (money < 0)
            {
                gameover_Panel.SetActive(true);
                is_Gameover = true;
            }
        }
        //문을 닫았을 때 시간을 멈추기
        if (is_Closed==true)
        {
            Time.timeScale=0;
        }
    }

    // 가게 레벨에 따른 업그레이드 창 띄우는 함수
    void Upgrade_Panel_Open()
    {
        // 업그레이드 창 띄우기
        upgrade_Panel.SetActive(true);


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
                burgur_Ingredient_Image[current_Burgur_Height].sprite = ingredient.ingredients_Sprite; // 재료 이미지 변경
                burgur_Ingredient_Object[current_Burgur_Height].SetActive(true); ; // 재료 활성화
                current_Make_Burger_Info[current_Burgur_Height] = (int)ingredient.type; // 실질적 데이터 적용
                Debug.Log(current_Make_Burger_Info[current_Burgur_Height]); // ------------------------------------------------- 테스트 중

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
            Debug.Log(current_Make_Burger_Info[current_Select_Ingredient_Height]); // ------------------------------------------------- 테스트 중

            // 재료 선택 해제(강조 해제)
            Sorting_Ingredient_Objects();

            return;
        }

        // 재료 넣을 공간이 있다면
        for (int i = 0; i < max_Burgur_Height; i++)
        {
            if (!burgur_Ingredient_Object[i].activeSelf)
            {
                burgur_Ingredient_Image[current_Burgur_Height].sprite = ingredient.ingredients_Sprite; // 재료 이미지 변경
                burgur_Ingredient_Object[current_Burgur_Height].SetActive(true); ; // 재료 활성화
                current_Make_Burger_Info[current_Burgur_Height] = (int)ingredient.type; // 실질적 데이터 적용
                Debug.Log(current_Make_Burger_Info[current_Burgur_Height]); // ------------------------------------------------- 테스트 중

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

    // 인내심 게이지
    public void Patience_Gauge_Decrease()
    {
        // 만약 손님이 기다리는 상태라면 (햄버거 조리 중)
        if (is_Guest_Waiting)
        {
            timer += Time.deltaTime;
            // 일정 시간마다
            if (timer > patience_Decrease_Time)
            {
                timer = 0f;

                // 인내심 1칸 깎기
                for (int i = 0; i < patience_Gauge_Objects.Length; i++)
                {
                    if (patience_Gauge_Objects[i].activeSelf)
                    {
                        patience_Gauge_Objects[i].SetActive(false);
                        break;
                    }
                }
                if (current_Patience_Value > 0)
                {
                    current_Patience_Value--;
                }
            }
        }
    }

    // 손님 오는 함수
    public void Guest_Come()
    {
        // 손님 실루엣 페이드인
        StartCoroutine(Fade_In());

        // 손님 얼굴 랜덤 뽑기
        guest_Face_Image.sprite = guest_Faces[UnityEngine.Random.Range(0, guest_Faces.Length)];

        // 손님 인내심 수치 초기화
        current_Patience_Value = 10f;
        for (int i = 0; i < patience_Gauge_Objects.Length; i++)
        {
            patience_Gauge_Objects[i].SetActive(true);
        }

        // 주문 랜덤 뽑기
        current_Order = order_Manager.Get_Order();

        // 돈 선입금
        money += burgur_Price;

        // 주문에 맞는 대화 출력하기
        Talk(current_Order.id);
    }

    // 손님 가는 함수
    public void Guest_Leave()
    {
        // 손님 실루엣 페이드 아웃
        StartCoroutine(Fade_Out());

        // 재료 맞는지 판정 후 불만족 했다면 돈 차감 ------- (판정 시스템)
        bool ingredients_Check = Ingredients_Check(current_Order, current_Make_Burger_Info);
        if (ingredients_Check)
        {
            // 손님이 만족한 코드
            Debug.Log("만족스러운 햄버거에요!");

            // if 인내심이 5칸 아래면 1칸 당 5퍼였나 돈 깎기. (대화창에 "맛있긴 하지만 너무 오래걸렸어요 같은 메시지 출력하면 좋을듯)
        }
        else
        {
            // 손님이 만족하지 못한 코드
            Debug.Log("주문한 햄버거가 아니에요..");

            // 만족도 패널티 받고 인내심에 따른 처리.
        }

        // 딜레이 시간 만큼 후 손님 다시 오게하기.
        StartCoroutine(Come_Guest_Delay());
    }

    // 실제로 대화 출력하는 함수
    public void Talk(int id)
    {
        string talk_Data = talk_Manager.Get_Talk(id);

        talk.SetMsg(talk_Data); // 실제 대화 창에 출력 + 타이핑 이펙트
    }

    // 재료 판정 함수
    public bool Ingredients_Check(Order order, int[] burger_Info)
    {
        // 주문 정보 받아옴.
        // 현재 만든 햄버거 정보 받아옴.
        
        // order의 판정 기준 갯수에 따라 for문으로 판정 (아마 2중 for문 사용해서 한번 더 재료가 몇 개 있는지 체크할 듯.)

        // 주문 판정 기준 갯수만큼 판정하기.
        for (int i = 0; i < order.condition_Number.Length; i++)
        {
            // 재료 필요한 갯수만큼 for문
            for (int j = 0; j < order.condition_Number[i].ingredients_Input_Number; j++)
            {
                // 해당 재료 있는지 확인
                int ingredient_Type = Array.IndexOf(burger_Info, order.condition_Number[i].ingredients_Type);
                if (ingredient_Type > -1) // 재료 있음.
                {
                    // 확인한 재료는 배열에서 제외.
                    burger_Info[ingredient_Type] = (int)Type.none;
                    continue;
                }
                else // 재료 없음.
                {
                    // 재료가 없으므로 옳지 않은 햄버거라고 판단. false 반환.
                    return false;
                }
            }
        }

        // 모든 판정에서 없는 재료가 없었다면 옳은 햄버거라고 판단. true 반환.
        return true;
    }

    // 손님 실루엣 페이드 인
    IEnumerator Fade_In()
    {
        Color color = new Color(0f, 0f, 0f, 0f);
        while (guest_Silhoutte_Image.color.a < 0.96)
        {
            color.a += Time.deltaTime * fade_Speed;
            guest_Silhoutte_Image.color = color;

            yield return null;
        }    
    }

    // 손님 실루엣 페이드 아웃
    IEnumerator Fade_Out()
    {
        Color color = new Color(0f, 0f, 0f, 0.96f);
        while (guest_Silhoutte_Image.color.a > 0)
        {
            color.a -= Time.deltaTime * fade_Speed;
            guest_Silhoutte_Image.color = color;

            yield return null;
        }
    }

    // 손님 일정 시간 후 다시오게 하는 코루틴
    IEnumerator Come_Guest_Delay()
    {
        yield return new WaitForSeconds(come_Guest_Delay_Time);

        Guest_Come();
    }
}
