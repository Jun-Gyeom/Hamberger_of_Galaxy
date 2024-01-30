using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    //���� �ð� �ؽ�Ʈ
    [SerializeField]
    private Text current_Time_Text;
    private string current_Time_Hour_Text;
    private string current_Time_Minute_Text;
    //���� ��¥ �ؽ�Ʈ
    [SerializeField]
    private Text current_Date_Number_Text;

    //��� ȭ��
    public GameObject result_Panel;

   

    //�ð��� �帣�� �ӵ�
    [SerializeField]
    private int time_Speed;
    //���� �ð�(��)
    public float current_Time_Hour;
    //���� �ð�(��)
    private float current_Time_Minute;
    //���� ���� �ð�
    public float opening_Time;
    //���� ���� �ð�
    [SerializeField]
    private float closing_Time;
    //���� ����
    public float current_Date=0;

    //�Ĵ� �� �ݾҴ����� ����
    public bool is_Closed=false;

    //�̱��� ����
    private static Gamemanager S_instance = null;
    public static Gamemanager Instance
    {
        get
        {
            if (S_instance == null)
            {
                S_instance = FindObjectOfType(typeof(Gamemanager)) as Gamemanager;
            }
            return S_instance;
        }
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        //���ӿ��� ���� �ð��� ǥ���ϰ��ϴ� �Լ�
        Display_Current_Time();
        //������ ���� �ݾҴ����� ���θ� Ȯ���ϴ� �Լ�
        Check_Is_Closed();
    

    }

    //������ ���� �ݾҴ����� ���θ� Ȯ���ϴ� �Լ�
    public void Check_Is_Closed()
    {
        //���� �ð��� ���� �ð��� ������ bool Ÿ���� ������ true�� �ٲ�
        if (current_Time_Hour == closing_Time)
        {
            is_Closed = true;
            //���â�� ����
            result_Panel.SetActive(true);
        }
        //���� �ݾ��� �� �ð��� ���߱�
        if (is_Closed==true)
        {
            current_Time_Hour = closing_Time;
            current_Time_Minute = 0;
        }
    }

   

    //���ӿ��� ���� �ð��� ǥ���ϰ��ϴ� �Լ�
    public void Display_Current_Time()
    {
        //���� �ð��� ���� ���Ѵ�
        current_Time_Minute += Time.deltaTime * time_Speed;

        //60���� �Ǹ� 1�ð����� �ٲ�� �����
        if (((int)current_Time_Minute) >= 60)
        {
            current_Time_Minute -= 60;
            current_Time_Hour += 1;
        }

        //���� �ð�(��)�� ǥ��� ����
        if (current_Time_Hour < 10 && is_Closed==false)
        {
            current_Time_Hour_Text = $"0{current_Time_Hour}";
        }
        else current_Time_Hour_Text = $"{current_Time_Hour}";

        //���� �ð�(��)�� ǥ��� ����
        if (current_Time_Minute < 10 && is_Closed == false)
        {
            current_Time_Minute_Text = $"0{(int)current_Time_Minute}";
        }
        else current_Time_Minute_Text = $"{(int)current_Time_Minute}";

        //���� �ð��� ��¥�� �ؽ�Ʈ�� ǥ���Ѵ�
        current_Time_Text.text = $"{current_Time_Hour_Text}:{current_Time_Minute_Text}";
        current_Date_Number_Text.text = current_Date.ToString();
    }
}
