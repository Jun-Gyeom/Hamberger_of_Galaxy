using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //���� ���� �Ѿ�� ��ư
    [SerializeField]
    private GameObject button_To_Next_Day;

    //���� ���� �Ѿ�� ��ư�� ���� �Լ�
    public void Push_Button_To_Next_Day()
    {
        //��¥ +1
        Gamemanager.Instance.current_Date += 1;
        //���â�� ������
        Gamemanager.Instance.result_Panel.SetActive(false);
        //���� ���� ����
        Gamemanager.Instance.is_Closed = false;
        Gamemanager.Instance.current_Time_Hour = Gamemanager.Instance.opening_Time;
    }
}
