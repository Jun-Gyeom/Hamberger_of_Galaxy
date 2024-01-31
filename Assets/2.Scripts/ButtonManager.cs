using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [Header("�丮 â")]
    [SerializeField]
    private GameObject cooking_Panel;

    //���� ���� �Ѿ�� ��ư�� ���� �Լ�
    public void Push_Button_To_Next_Day()
    {
        //��¥ +1
        GameManager.Instance.current_Date += 1;
        //���â�� ������
        GameManager.Instance.result_Panel.SetActive(false);
        //���� ���� ����
        GameManager.Instance.is_Closed = false;
        GameManager.Instance.current_Time_Hour = GameManager.Instance.opening_Time;
    }

    //�丮â�� ���� ��ư
    public void Push_Button_Open_Cooking_Panel()
    {
        cooking_Panel.SetActive(true);
    }

    //�丮 �Ϸ� ��ư
    public void Push_Button_Cooking_Complete()
    {
        cooking_Panel.SetActive(false);
    }
}
