using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cooking_Panel;
    [SerializeField]
    private GameObject ingredients_Panel;
    [SerializeField]
    private GameObject title_Panel;
    
    //[SerializeField]
    //private GameObject tutorial_Panel;

    [Space(20f)]
    // 요리 창인지 체크
    private bool is_On_Cooking_Panel;


    //게임 시작 버튼
    public void Push_Button_Game_Start()
    {
        title_Panel.SetActive(false);
        GameManager.Instance.pause_Panel.SetActive(false);
        GameManager.Instance.is_Paused=false;
        Time.timeScale = 1;
        //tutorial_Panel.SetActive(true);

    }

    //게임 종료 버튼
    public void Push_Button_Game_Exit()
    {
        Application.Quit();
    }

    //일시 정지에서 타이틀로 가는 버튼
    public void Push_Button_To_Title()
    {
        title_Panel.SetActive(true);
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
        //날짜 +1
        GameManager.Instance.current_Date += 1;
        //결과창을 가린다
        GameManager.Instance.result_Panel.SetActive(false);
        // 업그레이드 창 닫기
        GameManager.Instance.upgread_Panel.SetActive(false);
        //가게 문을 연다
        GameManager.Instance.is_Closed = false;
        GameManager.Instance.current_Time_Hour = GameManager.Instance.opening_Time;
    }

    //요리 창 열기, 요리 완료 버튼
    public void Push_Button_Open_Cooking_Panel()
    {
        // 요리 창 닫혀있을 때 (요리창 열기 버튼)
        if (is_On_Cooking_Panel == false)
        {
            ingredients_Panel.SetActive (true);
            cooking_Panel.SetActive(true);
            is_On_Cooking_Panel = true;
        }
        // 요리 창 열려있을 때 (요리 완료 버튼)
        else if (is_On_Cooking_Panel == true)
        {
            // 햄버거 윗부분 덮기 기능 추가 예정

            ingredients_Panel.SetActive(false);
            cooking_Panel.SetActive(false);
            is_On_Cooking_Panel = false;
        }    
    }

    // 가게 업그레이드 함수
    public void Shop_Upgrade()
    {
        // 업그레이드 실패 (돈 부족)
        if (GameManager.Instance.shop_Upgrade_Cost[GameManager.Instance.shop_Level] > GameManager.Instance.money)
        {
            // 돈이 부족합니다 알림 또는 효과음 재생.
            Debug.Log("돈이 부족합니다.");
            return;
        }

        // 업그레이드 성공 (업그레이드 비용보다 돈이 많을 때)
        GameManager.Instance.money -= GameManager.Instance.shop_Upgrade_Cost[GameManager.Instance.shop_Level]; // 업그레이드 비용 지불
        GameManager.Instance.shop_Level++; // 가게 레벨 업그레이드
    }
}
