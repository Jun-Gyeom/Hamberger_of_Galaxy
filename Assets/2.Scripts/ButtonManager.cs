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
    // �ܹ��� ���� ��
    public Ingredients ingredients_Top_Bun;

    //[SerializeField]
    //private GameObject tutorial_Panel;

    //���� ���� ��ư
    public void Push_Button_Game_Start()
    {
        GameManager.Instance.current_Time_Minute = 0;
        GameManager.Instance.current_Time_Hour = GameManager.Instance.opening_Time;

        // ����� �� �ʱ�ȭ
        GameManager.Instance.last_Money = 0f;

        //���� ù ���̸� Ʃ�丮�� ����
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

    //Ʃ�丮�� ���� ��ư
    public void Push_Button_End_Tutorial()
    {
        // �մ� ���� �Լ�
        GameManager.Instance.Guest_Come();
        // ���� ���� ���� üũ
        GameManager.Instance.is_Game_Start = true;

        tutorial_Panel.SetActive(false);

        GameManager.Instance.is_Paused = false;
        Time.timeScale = 1;

    }

    //���� ���� ��ư
    public void Push_Button_Game_Exit()
    {
        Application.Quit();
    }

    //Ÿ��Ʋ�� ���� ��ư
    public void Push_Button_To_Title()
    {
        // ���� ���� ���� üũ ����
        GameManager.Instance.is_Game_Start = false;

        Time.timeScale = 1f; // �Ͻ����� ����

        GameManager.Instance.pause_Panel.SetActive(false);

        //���� �ð��� ��¥�� �ؽ�Ʈ�� ǥ���Ѵ�
        GameManager.Instance.current_Time_Text.text = $"{GameManager.Instance.opening_Time}:00";
        GameManager.Instance.current_Date_Number_Text.text = (GameManager.Instance.current_Date + 1).ToString();

        GameManager.Instance.talk.SetMsg($"", false); // ��ȭâ �����

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
        //���� ���� ����
        GameManager.Instance.is_Closed = false;
        GameManager.Instance.current_Time_Hour = GameManager.Instance.opening_Time;

        //���� �����Ѵ�
        GameManager.Instance.last_Money = GameManager.Instance.money;
        //��¥ +1
        GameManager.Instance.current_Date += 1;
        //���â�� ������
        GameManager.Instance.result_Panel.SetActive(false);
        // ���׷��̵� â �ݱ�
        GameManager.Instance.upgrade_Panel.SetActive(false);

        //���� ���� Ȯ��
        if (GameManager.Instance.current_Date>=5 && GameManager.Instance.is_Gameover==false)
        {
            is_Ending = true;
            GameManager.Instance.ending_Panel.SetActive(true);
            StartCoroutine("WaitForToTitle");
        }

        // �մ� �θ���
        GameManager.Instance.Guest_Come();

    }

    

    //�丮 â ����, �丮 �Ϸ� ��ư
    public void Push_Button_Open_Cooking_Panel()
    {
        if (GameManager.Instance.is_Closed)
        {
            return;
        }

        // �ֹ��� �� �޾��� �� (�丮â ���� ����)
        if ((GameManager.Instance.is_On_Cooking_Panel == false) && (GameManager.Instance.is_End_Current_Order))
        {
            // ���� ������� ���� �̹��� �ʱ�ȭ
            for (int i = 0; i < GameManager.Instance.max_Burgur_Height + 1; i++)
            {
                GameManager.Instance.burgur_Ingredient_Object[i].SetActive(false); ; // ��� ��Ȱ��ȭ
                GameManager.Instance.current_Make_Burger_Info[i] = 0; // ������ �ʱ�ȭ
            }

            GameManager.Instance.ingredients_Panel.SetActive (true);
            GameManager.Instance.cooking_Panel.SetActive(true);
            GameManager.Instance.is_On_Cooking_Panel = true;
            GameManager.Instance.is_Guest_Waiting = true; // �մ� ��ٸ��� ����
        }
        // �丮 â �������� �� (�丮 �Ϸ� ��ư)
        else if ((GameManager.Instance.is_On_Cooking_Panel == true) && !GameManager.Instance.is_Cooking_Panel_Closing_Coroutine)
        {
            // �ܹ��� ���κ� ����
            GameManager.Instance.Cook_Hamburger(ingredients_Top_Bun);

            // ��� ���� ����(���� ����)
            GameManager.Instance.Sorting_Ingredient_Objects();

            // �մ� ��ٸ��� ����
            GameManager.Instance.is_Guest_Waiting = false;

            // �ܹ��� ���� ����� �����ֱ� ���� �� �� ��ٸ���
            StartCoroutine("Cooking_Panel_Close");
        }    
    }

    // ���� ���׷��̵� �Լ�
    public void Shop_Upgrade()
    {
        // ���׷��̵� ���� (�� ����)
        if (GameManager.Instance.shop_Upgrade_Cost[GameManager.Instance.shop_Level] > GameManager.Instance.money)
        {
            // ���� �����մϴ� �ؽ�Ʈ ����.
            GameManager.Instance.talk.SetMsg($"���� �����մϴ�.", false); 
            return;
        }

        // ���׷��̵� ���� (���׷��̵� ��뺸�� ���� ���� ��)
        GameManager.Instance.money -= GameManager.Instance.shop_Upgrade_Cost[GameManager.Instance.shop_Level]; // ���׷��̵� ��� ����
        GameManager.Instance.shop_Level++; // ���� ���� ���׷��̵�

        // ��ᰡ ���� ���� � ��밡�������� ���� ���׷��̵� �� ���� �� �ִ� ��� ǥ��
        int unlock_Num = 0; // �ر� �Ǵ� ��� ����
        GameManager.Instance.unlock_Ingredient_Object[0].SetActive(false);
        GameManager.Instance.unlock_Ingredient_Object[1].SetActive(false);
        GameManager.Instance.unlock_Ingredient_Object[2].SetActive(false);

        for (int i = 0; i < GameManager.Instance.ingredients.Length; i++)
        {
            // �ر� �� ��ᰡ �ִٸ�
            if (GameManager.Instance.ingredients[i].ingredient.available_Shop_Level == GameManager.Instance.shop_Level + 1)
            {
                GameManager.Instance.unlock_Ingredient_Image[unlock_Num].sprite = GameManager.Instance.ingredients[i].ingredient.ingredients_Sprite; // ������ ����
                GameManager.Instance.unlock_Ingredient_Name[unlock_Num].text = GameManager.Instance.ingredients[i].ingredient.ingredients_Name; // �̸� ����
                GameManager.Instance.unlock_Ingredient_Object[unlock_Num].SetActive(true);
                unlock_Num++;
            }
        }

        // ���׷��̵� ��� ǥ��
        if (GameManager.Instance.shop_Level == GameManager.Instance.shop_Upgrade_Cost.Length)
        {
            GameManager.Instance.shop_Upgrade_Cost_Text.text = "�ر� �Ϸ�";
            GameManager.Instance.shop_Upgrade_Button.interactable = false; // ���� ���׷��̵� ��ư ��Ȱ��ȭ
            return;
        }

        GameManager.Instance.shop_Upgrade_Cost_Text.text = $"����:{GameManager.Instance.shop_Upgrade_Cost[GameManager.Instance.shop_Level]}��";
    }

    // �丮 â �ݱ� (�丮�Ϸ�) �ڷ�ƾ
    IEnumerator Cooking_Panel_Close()
    {
        GameManager.Instance.is_Cooking_Panel_Closing_Coroutine = true;

        // ��� ���
        yield return new WaitForSeconds(GameManager.Instance.burgur_Compete_Time);

        // �丮 â �ݱ�
        GameManager.Instance.ingredients_Panel.SetActive(false);
        GameManager.Instance.cooking_Panel.SetActive(false);
        GameManager.Instance.is_On_Cooking_Panel = false;
        GameManager.Instance.is_End_Current_Order = false;

        // ���� ���õ� �ܹ��� ���� �ʱ�ȭ
        GameManager.Instance.current_Burgur_Height = 0;

        GameManager.Instance.is_Cooking_Panel_Closing_Coroutine = false;

        GameManager.Instance.Guest_Leave(); // �մ� ������.
    }

    public IEnumerator Game_Over_To_Title()
    {
        // 3�� ���
        yield return new WaitForSeconds(3f);

        GameManager.Instance.talk.SetMsg($"3�� �� Ÿ��Ʋ��...", false); // ��ȭ â�� ���
        // 4�� ���                                                          
        yield return new WaitForSeconds(4f);

        Push_Button_To_Title(); // Ÿ��Ʋ��
    }

    IEnumerator WaitForToTitle()
    {
        yield return new WaitForSeconds(15f);
        GameManager.Instance.current_Date = 0;
        Push_Button_To_Title();
    }
}
