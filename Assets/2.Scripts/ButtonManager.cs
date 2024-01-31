using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [Header("요리 창")]
    [SerializeField]
    private GameObject cooking_Panel;

    // 요리 창인지 체크
    private bool is_On_Cooking_Panel;

    //다음 날로 넘어가는 버튼을 누른 함수
    public void Push_Button_To_Next_Day()
    {
        //날짜 +1
        GameManager.Instance.current_Date += 1;
        //결과창을 가린다
        GameManager.Instance.result_Panel.SetActive(false);
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
            cooking_Panel.SetActive(true);
            is_On_Cooking_Panel = true;
        }
        // 요리 창 열려있을 때 (요리 완료 버튼)
        else if (is_On_Cooking_Panel == true)
        {
            // 햄버거 윗부분 덮기 기능 추가 예정
            
            cooking_Panel.SetActive(false);
            is_On_Cooking_Panel = false;
        }    
    }
}
