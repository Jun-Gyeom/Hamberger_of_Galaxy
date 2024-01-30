using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //다음 날로 넘어가는 버튼
    [SerializeField]
    private GameObject button_To_Next_Day;

    //다음 날로 넘어가는 버튼을 누른 함수
    public void Push_Button_To_Next_Day()
    {
        //날짜 +1
        Gamemanager.Instance.current_Date += 1;
        //결과창을 가린다
        Gamemanager.Instance.result_Panel.SetActive(false);
        //가게 문을 연다
        Gamemanager.Instance.is_Closed = false;
        Gamemanager.Instance.current_Time_Hour = Gamemanager.Instance.opening_Time;
    }
}
