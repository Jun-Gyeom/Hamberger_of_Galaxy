using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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


    [SerializeField]
    private Sprite[] npcs_Face;
    [SerializeField]
    private Image npc_Face_Image;

    public Animator fade;
    public bool is_Stay_Npc = false;


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
    public bool is_Paused=true;
    public bool is_Npc_check=true;


    //일자별로 체크하는 돈
    public float FinishMoney;


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
        money_Text.text = $"{money}$";
        //npc여부 확인
        CheckNpc();
        if (Input.GetKeyDown(KeyCode.Escape) && is_Paused == false)
        {
            is_Paused = true;
        }
        CheckPause();
        
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

            //돈을 저장한다
            FinishMoney = money;

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
        if (is_Paused==true)
        {
            pause_Panel.SetActive(true);
            Time.timeScale = 0;
            is_Paused = false;
        }
        
    }
        
        
    
    //npc가 있는지 체크
    public void CheckNpc()
    {
        if (is_Stay_Npc==false&&is_Npc_check==true)
        {
            is_Npc_check = false;
            Invoke("ComeInNpc",3f);
        } 
    }
    //npc가 들어오는 함수
    public void ComeInNpc()
    {
        is_Stay_Npc = true;
        npc_Face_Image.sprite = npcs_Face[Random.Range(0, npcs_Face.Length)];
        fade.SetTrigger("FadeIn");
        is_Npc_check=true;
    }
}
