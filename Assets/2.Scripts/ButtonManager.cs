using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [Header("�丮 â")]
    [SerializeField]
    private GameObject cooking_Panel;

    // �丮 â���� üũ
    private bool is_On_Cooking_Panel;

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

    //�丮 â ����, �丮 �Ϸ� ��ư
    public void Push_Button_Open_Cooking_Panel()
    {
        // �丮 â �������� �� (�丮â ���� ��ư)
        if (is_On_Cooking_Panel == false)
        {
            cooking_Panel.SetActive(true);
            is_On_Cooking_Panel = true;
        }
        // �丮 â �������� �� (�丮 �Ϸ� ��ư)
        else if (is_On_Cooking_Panel == true)
        {
            // �ܹ��� ���κ� ���� ��� �߰� ����
            
            cooking_Panel.SetActive(false);
            is_On_Cooking_Panel = false;
        }    
    }
}
