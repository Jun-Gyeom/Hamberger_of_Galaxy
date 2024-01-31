using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [Header("요리 창")]
    [SerializeField]
    private GameObject cooking_Panel;

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

    //요리창을 띄우는 버튼
    public void Push_Button_Open_Cooking_Panel()
    {
        cooking_Panel.SetActive(true);
    }

    //요리 완료 버튼
    public void Push_Button_Cooking_Complete()
    {
        cooking_Panel.SetActive(false);
    }
}
