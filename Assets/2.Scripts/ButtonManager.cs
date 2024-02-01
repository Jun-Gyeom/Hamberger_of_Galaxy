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
    [SerializeField]
    private NPCmanager npcmanager;


    [Header("������ ��������")]
    [SerializeField]
    private GameObject[] patience_Guages;

    private Coroutine deleteCoroutine;

    //���ں��� üũ�ϴ� ��
    private float FinishMoney;

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
        GameManager.Instance.money = FinishMoney;
    }

    //���� ���� ��ư
    public void Push_Button_Game_Exit()
    {
        Application.Quit();
    }

    //�Ͻ� �������� Ÿ��Ʋ�� ���� ��ư
    public void Push_Button_To_Title()
    {
        GameManager.Instance.current_Time_Hour = GameManager.Instance.opening_Time;
        GameManager.Instance.current_Time_Minute = 0;
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
        //���� �����Ѵ�
        FinishMoney = GameManager.Instance.money;
    }

    //�丮 â ����, �丮 �Ϸ� ��ư
    public void Push_Button_Open_Cooking_Panel()
    {
        // �丮 â �������� �� (�丮â ���� ��ư)
        if (is_On_Cooking_Panel == false && GameManager.Instance.is_Stay_Npc==true)
        {
            ingredients_Panel.SetActive (true);
            cooking_Panel.SetActive(true);
            is_On_Cooking_Panel = true;
            deleteCoroutine = StartCoroutine(DeleteArrays());

        }
        // �丮 â �������� �� (�丮 �Ϸ� ��ư)
        else if (is_On_Cooking_Panel == true)
        {
            // �ܹ��� ���κ� ���� ��� �߰� ����
            GameManager.Instance.is_Stay_Npc = false;
            ingredients_Panel.SetActive(false);
            cooking_Panel.SetActive(false);
            is_On_Cooking_Panel = false;
            GameManager.Instance.fade.SetTrigger("Fadeout");
            GameManager.Instance.is_Stay_Npc = false;
            if (deleteCoroutine != null)
            {
                StopCoroutine(deleteCoroutine);
            }
            foreach (var item in patience_Guages)
            {
                item.SetActive(true);
            }
        }    
    }

    IEnumerator DeleteArrays()
    {
        for (int i = 0; i < 10; i++)
        {
            // �迭�� �����ϱ� ���� 2�� ��ٸ���
            yield return new WaitForSeconds(2f);
            patience_Guages[i].SetActive(false);
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
