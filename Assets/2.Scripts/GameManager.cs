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
    [Header("�ð��� �帣�� �ӵ�")]
    [SerializeField]
    private int time_Speed;

    [Space (20f)]
    //���� �ð�(��)
    public float current_Time_Hour;
    //���� �ð�(��)
    private float current_Time_Minute;
    //���� ���� �ð�
    [Header("���� ���� �ð�")]
    public float opening_Time;
    //���� ���� �ð�
    [Header("���� ���� �ð�")]
    [SerializeField]
    private float closing_Time;

    [Space(20f)]
    //���� ����
    public float current_Date=0;

    //�Ĵ� �� �ݾҴ����� ����
    public bool is_Closed=false;



    // �丮 â�� �����ִ����� ����
    private bool is_On_Cooking_Panel;
    // �丮 â ������Ʈ
    [SerializeField]
    private GameObject Cooking_Panel_Object;
    // �丮 â ��Ʈ Ʈ������
    private RectTransform Cooking_Panel_RectTransform;
    // �丮 â ���� �ݴ� �ӵ�
    [Header("�丮 â ���� �ݴ� �ӵ�")]
    public float Cooking_Panel_Toggle_Speed;

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

    private void Awake()
    {
        // �ػ� FHD�� ���� �� ��ü ȭ������ ����
        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        // �丮 â ��ȯ�� ���� ��Ʈ Ʈ������ �Ҵ�
        Cooking_Panel_RectTransform = Cooking_Panel_Object.GetComponent<RectTransform>();
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

    // �丮 â ����, �ݱ� ��� ����ġ �ڷ�ƾ
    IEnumerator Cooking_Panel_Toggle_Coroutine()
    {
        float Cooking_Panel_RectTransform_PosX = Cooking_Panel_RectTransform.anchoredPosition.x;
        // ���� (�丮 â�� �����ִ� ���)
        if (is_On_Cooking_Panel == false)
        {
            Debug.Log("�丮 â ����");
            while (Cooking_Panel_RectTransform.anchoredPosition.x > 0)
            {
                Cooking_Panel_RectTransform_PosX -= Time.deltaTime * Cooking_Panel_Toggle_Speed;

                Cooking_Panel_RectTransform.anchoredPosition = new Vector2(Cooking_Panel_RectTransform_PosX, 0);
                yield return null;
            }

            Cooking_Panel_RectTransform.anchoredPosition = new Vector2(0, 0);
            is_On_Cooking_Panel = true;
        }
        // �ݱ� (�丮 â�� �����ִ� ���)
        else if (is_On_Cooking_Panel == true)
        {
            Debug.Log("�丮 â �ݱ�");
            while (Cooking_Panel_RectTransform.anchoredPosition.x < 1920)
            {
                Cooking_Panel_RectTransform_PosX += Time.deltaTime * Cooking_Panel_Toggle_Speed;

                Cooking_Panel_RectTransform.anchoredPosition = new Vector2(Cooking_Panel_RectTransform_PosX, 0);
                yield return null;
            }

            Cooking_Panel_RectTransform.anchoredPosition = new Vector2(1920, 0);
            is_On_Cooking_Panel = false;
        }
    }

    // ��ư�� �����ų �丮 â ���� �ݱ� ��� �Լ�
    public void Cooking_Panel_Toggle()
    {
        StartCoroutine("Cooking_Panel_Toggle_Coroutine");
    }
}
