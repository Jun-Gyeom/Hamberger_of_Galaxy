using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject title_Panel;
    [SerializeField]
    private GameObject tutorial_Panel;


    private bool is_Ending=false;
    // 햄버거 윗면 빵
    public Ingredients ingredients_Top_Bun;

    //[SerializeField]
    //private GameObject tutorial_Panel;

    //게임 시작 버튼
    public void Push_Button_Game_Start()
    {
        GameManager.Instance.current_Time_Minute = 0;
        GameManager.Instance.current_Time_Hour = GameManager.Instance.opening_Time;

        // 저장된 돈 초기화
        GameManager.Instance.last_Money = 0f;

        //게임 첫 날이면 튜토리얼 띄우기
        if (GameManager.Instance.current_Date==0)
        {
            tutorial_Panel.SetActive(true);
            GameManager.Instance.pause_Panel.SetActive(false);
            Time.timeScale = 0;
            GameManager.Instance.is_Paused = true;
            title_Panel.SetActive(false);
        }
        else
        {
            title_Panel.SetActive(false);
            GameManager.Instance.pause_Panel.SetActive(false);
            GameManager.Instance.is_Paused = false;
            Time.timeScale = 1;
            GameManager.Instance.money = GameManager.Instance.last_Money;
        }
    }

    //튜토리얼 종료 버튼
    public void Push_Button_End_Tutorial()
    {
        // 손님 오는 함수
        GameManager.Instance.Guest_Come();
        // 게임 시작 상태 체크
        GameManager.Instance.is_Game_Start = true;

        tutorial_Panel.SetActive(false);

        GameManager.Instance.is_Paused = false;
        Time.timeScale = 1;

    }

    //게임 종료 버튼
    public void Push_Button_Game_Exit()
    {
        Application.Quit();
    }

    //타이틀로 가는 버튼
    public void Push_Button_To_Title()
    {
        // 게임 시작 상태 체크 해제
        GameManager.Instance.is_Game_Start = false;

        Time.timeScale = 1f; // 일시정지 해제

        GameManager.Instance.pause_Panel.SetActive(false);

        //현재 시간과 날짜를 텍스트로 표시한다
        GameManager.Instance.current_Time_Text.text = $"{GameManager.Instance.opening_Time}:00";
        GameManager.Instance.current_Date_Number_Text.text = (GameManager.Instance.current_Date + 1).ToString();

        GameManager.Instance.talk.SetMsg($"", false); // 대화창 지우기

        GameManager.Instance.upgrade_Panel.SetActive(false);
        GameManager.Instance.is_Closed=false;
        GameManager.Instance.current_Time_Hour = GameManager.Instance.opening_Time;
        GameManager.Instance.current_Time_Minute = 0;
        title_Panel.SetActive(true);
        GameManager.Instance.result_Panel.SetActive(false);
        if (GameManager.Instance.is_Gameover==true || is_Ending == true)
        {
            GameManager.Instance.is_Gameover = false;
            is_Ending = false;
            GameManager.Instance.ending_Panel.SetActive(false);
            GameManager.Instance.money = 0;
            GameManager.Instance.current_Date = 0;
            GameManager.Instance.current_Time_Hour = GameManager.Instance.opening_Time;
            GameManager.Instance.gameover_Panel.SetActive(false);
        }
    }

    //일시 정지에서 게임을 계속하는 버튼
    public void Push_Button_To_Game_Continue()
    {
        GameManager.Instance.pause_Panel.SetActive(false);
        GameManager.Instance.is_Paused = false;
        Time.timeScale = 1;
    }

    //다음 날로 넘어가는 버튼을 누른 함수
    public void Push_Button_To_Next_Day()
    {
        //가게 문을 연다
        GameManager.Instance.is_Closed = false;
        GameManager.Instance.current_Time_Hour = GameManager.Instance.opening_Time;

        //돈을 저장한다
        GameManager.Instance.last_Money = GameManager.Instance.money;
        //날짜 +1
        GameManager.Instance.current_Date += 1;
        //결과창을 가린다
        GameManager.Instance.result_Panel.SetActive(false);
        // 업그레이드 창 닫기
        GameManager.Instance.upgrade_Panel.SetActive(false);

        //엔딩 여부 확인
        if (GameManager.Instance.current_Date>=5 && GameManager.Instance.is_Gameover==false)
        {
            is_Ending = true;
            GameManager.Instance.ending_Panel.SetActive(true);
            StartCoroutine("WaitForToTitle");
        }

        // 손님 부르기
        GameManager.Instance.Guest_Come();

    }

    

    //요리 창 열기, 요리 완료 버튼
    public void Push_Button_Open_Cooking_Panel()
    {
        if (GameManager.Instance.is_Closed)
        {
            return;
        }

        // 주문을 다 받았을 때 (요리창 열기 가능)
        if ((GameManager.Instance.is_On_Cooking_Panel == false) && (GameManager.Instance.is_End_Current_Order))
        {
            // 현재 만들어진 버거 이미지 초기화
            for (int i = 0; i < GameManager.Instance.max_Burgur_Height + 1; i++)
            {
                GameManager.Instance.burgur_Ingredient_Object[i].SetActive(false); ; // 재료 비활성화
                GameManager.Instance.current_Make_Burger_Info[i] = 0; // 데이터 초기화
            }

            GameManager.Instance.ingredients_Panel.SetActive (true);
            GameManager.Instance.cooking_Panel.SetActive(true);
            GameManager.Instance.is_On_Cooking_Panel = true;
            GameManager.Instance.is_Guest_Waiting = true; // 손님 기다리기 시작
        }
        // 요리 창 열려있을 때 (요리 완료 버튼)
        else if ((GameManager.Instance.is_On_Cooking_Panel == true) && !GameManager.Instance.is_Cooking_Panel_Closing_Coroutine)
        {
            // 햄버거 윗부분 덮기
            GameManager.Instance.Cook_Hamburger(ingredients_Top_Bun);

            // 재료 선택 해제(강조 해제)
            GameManager.Instance.Sorting_Ingredient_Objects();

            // 손님 기다리기 종료
            GameManager.Instance.is_Guest_Waiting = false;

            // 햄버거 덮힌 모습을 보여주기 위해 몇 초 기다리기
            StartCoroutine("Cooking_Panel_Close");
        }    
    }

    // 가게 업그레이드 함수
    public void Shop_Upgrade()
    {
        // 업그레이드 실패 (돈 부족)
        if (GameManager.Instance.shop_Upgrade_Cost[GameManager.Instance.shop_Level] > GameManager.Instance.money)
        {
            // 돈이 부족합니다 텍스트 실행.
            GameManager.Instance.talk.SetMsg($"돈이 부족합니다.", false); 
            return;
        }

        // 업그레이드 성공 (업그레이드 비용보다 돈이 많을 때)
        GameManager.Instance.money -= GameManager.Instance.shop_Upgrade_Cost[GameManager.Instance.shop_Level]; // 업그레이드 비용 지불
        GameManager.Instance.shop_Level++; // 가게 레벨 업그레이드

        // 재료가 가게 레벨 몇에 사용가능한지에 따라 업그레이드 시 얻을 수 있는 재료 표시
        int unlock_Num = 0; // 해금 되는 재료 갯수
        GameManager.Instance.unlock_Ingredient_Object[0].SetActive(false);
        GameManager.Instance.unlock_Ingredient_Object[1].SetActive(false);
        GameManager.Instance.unlock_Ingredient_Object[2].SetActive(false);

        for (int i = 0; i < GameManager.Instance.ingredients.Length; i++)
        {
            // 해금 될 재료가 있다면
            if (GameManager.Instance.ingredients[i].ingredient.available_Shop_Level == GameManager.Instance.shop_Level + 1)
            {
                GameManager.Instance.unlock_Ingredient_Image[unlock_Num].sprite = GameManager.Instance.ingredients[i].ingredient.ingredients_Sprite; // 아이콘 변경
                GameManager.Instance.unlock_Ingredient_Name[unlock_Num].text = GameManager.Instance.ingredients[i].ingredient.ingredients_Name; // 이름 변경
                GameManager.Instance.unlock_Ingredient_Object[unlock_Num].SetActive(true);
                unlock_Num++;
            }
        }

        // 업그레이드 비용 표시
        if (GameManager.Instance.shop_Level == GameManager.Instance.shop_Upgrade_Cost.Length)
        {
            GameManager.Instance.shop_Upgrade_Cost_Text.text = "해금 완료";
            GameManager.Instance.shop_Upgrade_Button.interactable = false; // 가게 업그레이드 버튼 비활성화
            return;
        }

        GameManager.Instance.shop_Upgrade_Cost_Text.text = $"구매:{GameManager.Instance.shop_Upgrade_Cost[GameManager.Instance.shop_Level]}원";
    }

    // 요리 창 닫기 (요리완료) 코루틴
    IEnumerator Cooking_Panel_Close()
    {
        GameManager.Instance.is_Cooking_Panel_Closing_Coroutine = true;

        // 잠시 대기
        yield return new WaitForSeconds(GameManager.Instance.burgur_Compete_Time);

        // 요리 창 닫기
        GameManager.Instance.ingredients_Panel.SetActive(false);
        GameManager.Instance.cooking_Panel.SetActive(false);
        GameManager.Instance.is_On_Cooking_Panel = false;
        GameManager.Instance.is_End_Current_Order = false;

        // 현재 선택된 햄버거 높이 초기화
        GameManager.Instance.current_Burgur_Height = 0;

        GameManager.Instance.is_Cooking_Panel_Closing_Coroutine = false;

        GameManager.Instance.Guest_Leave(); // 손님 떠나기.
    }

    public IEnumerator Game_Over_To_Title()
    {
        // 3초 대기
        yield return new WaitForSeconds(3f);

        GameManager.Instance.talk.SetMsg($"3초 후 타이틀로...", false); // 대화 창에 출력
        // 4초 대기                                                          
        yield return new WaitForSeconds(4f);

        Push_Button_To_Title(); // 타이틀로
    }

    IEnumerator WaitForToTitle()
    {
        yield return new WaitForSeconds(15f);
        GameManager.Instance.current_Date = 0;
        Push_Button_To_Title();
    }
}
