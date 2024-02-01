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
    // �丮 â���� üũ
    private bool is_On_Cooking_Panel;


    //���� ���� ��ư
    public void Push_Button_Game_Start()
    {
        title_Panel.SetActive(false);
        GameManager.Instance.pause_Panel.SetActive(false);
        GameManager.Instance.is_Paused=false;
        Time.timeScale = 1;
        //tutorial_Panel.SetActive(true);

    }

    //���� ���� ��ư
    public void Push_Button_Game_Exit()
    {
        Application.Quit();
    }

    //�Ͻ� �������� Ÿ��Ʋ�� ���� ��ư
    public void Push_Button_To_Title()
    {
        title_Panel.SetActive(true);
    }

    //�Ͻ� �������� ������ ����ϴ� ��ư
    public void Push_Button_To_Game_Continue()
    {
        GameManager.Instance.pause_Panel.SetActive(false);
        GameManager.Instance.is_Paused = false;
        Time.timeScale = 1;
    }

    //���� ���� �Ѿ�� ��ư�� ���� �Լ�
    public void Push_Button_To_Next_Day()
    {
        //��¥ +1
        GameManager.Instance.current_Date += 1;
        //���â�� ������
        GameManager.Instance.result_Panel.SetActive(false);
        // ���׷��̵� â �ݱ�
        GameManager.Instance.upgread_Panel.SetActive(false);
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
            ingredients_Panel.SetActive (true);
            cooking_Panel.SetActive(true);
            is_On_Cooking_Panel = true;
        }
        // �丮 â �������� �� (�丮 �Ϸ� ��ư)
        else if (is_On_Cooking_Panel == true)
        {
            // �ܹ��� ���κ� ���� ��� �߰� ����

            ingredients_Panel.SetActive(false);
            cooking_Panel.SetActive(false);
            is_On_Cooking_Panel = false;
        }    
    }

    // ���� ���׷��̵� �Լ�
    public void Shop_Upgrade()
    {
        // ���׷��̵� ���� (�� ����)
        if (GameManager.Instance.shop_Upgrade_Cost[GameManager.Instance.shop_Level] > GameManager.Instance.money)
        {
            // ���� �����մϴ� �˸� �Ǵ� ȿ���� ���.
            Debug.Log("���� �����մϴ�.");
            return;
        }

        // ���׷��̵� ���� (���׷��̵� ��뺸�� ���� ���� ��)
        GameManager.Instance.money -= GameManager.Instance.shop_Upgrade_Cost[GameManager.Instance.shop_Level]; // ���׷��̵� ��� ����
        GameManager.Instance.shop_Level++; // ���� ���� ���׷��̵�
    }
}
