using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    easy,   // �ʱ�
    Medium, // �߱�
    Hard    // ���
}

[Serializable]
public class Difficulty_Limit
{
    [Header("�߱� ���̵� �ֹ� ���� ����")]
    public int medium_Limit;
    [Header("��� ���̵� �ֹ� ���� ����")]
    public int hard_Limit;
}


[Serializable]
public class Difficulty_Percentage
{
    [Header("�ʱ� ���̵� �ֹ� Ȯ��")]
    public float easy_Percentage;
    [Header("�߱� ���̵� �ֹ� Ȯ��")]
    public float medium_Percentage;
    [Header("��� ���̵� �ֹ� Ȯ��")]
    public float hard_Percentage;
}

[Serializable]
public class Order
{
    public int id; // �ֹ� ID (Key ��)
    public Difficulty difficulty;
}

public class OrderManager : MonoBehaviour
{
    [Header("��¥�� �߱�, ��� ���̵� �ֹ� ���� ����")]
    [SerializeField]
    public Difficulty_Limit[] difficulty_Limit_Of_Day; // <Day, Limit>
    [Header("��¥�� �ʱ�, �߱�, ��� ���̵� �ֹ� ���� Ȯ��")]
    [SerializeField]
    public Difficulty_Percentage[] difficulty_Percentage_Of_Day;
    [Header("�ֹ� ���")]
    [SerializeField]
    public List <Order> ordet_List;

    [SerializeField] // �ӽ�
    private List<Order> easy_Order_List;
    [SerializeField] // �ӽ�
    private List<Order> medeum_Order_List;
    [SerializeField] // �ӽ�
    private List<Order> hard_Order_List;

    [SerializeField] // �ӽ�
    private List<Order> target_List;

    private int current_Medeum_Order_Number_Of_Day;  // ���� �������� �߱� ���̵� �ֹ� ���� Ƚ��
    private int current_Hard_Order_Number_Of_Day;    // ���� �������� ��� ���̵� �ֹ� ���� Ƚ��

    private void Awake()
    {
        Order_Difficulty_Sorting();
    }

    // �ֹ� ���̵� �� �з� �Լ�
    void Order_Difficulty_Sorting()
    {
        foreach (Order order in ordet_List)
        {
            if (order.difficulty == Difficulty.easy) // �ʱ�
            {
                easy_Order_List.Add(order);
            }
            else if (order.difficulty == Difficulty.Medium) // �߱�
            {
                medeum_Order_List.Add(order);
            }
            else if (order.difficulty == Difficulty.Hard) // ���
            {
                hard_Order_List.Add(order);
            }
        }
    }

    // ���� ���̵� �̱� �� �ֹ� �̱�
    public Order Get_Order()
    {
        while (true)
        {
            float rand_Difficulty = UnityEngine.Random.Range(0f, 1f);
            Debug.Log(rand_Difficulty);

            Debug.Log(difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].easy_Percentage / 100f);

            Debug.Log(difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].easy_Percentage / 100f
                + difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].medium_Percentage / 100f);

            Debug.Log(difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].easy_Percentage / 100f
                + difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].medium_Percentage / 100f
                + difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].hard_Percentage / 100f);

            // �޸�
            // ex) �ʱ� 60, �߱� 30, ��� 10
            // �ʱ� ( ���ʱ�
            // �߱� ( �ʱ� + �߱�
            // ��� ( �ʱ� + �߱� + ���

            if (rand_Difficulty < (difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].easy_Percentage / 100f))  // �ʱ�
            {
                target_List = easy_Order_List;
                break;
            }
            else if (rand_Difficulty < (difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].easy_Percentage / 100f
                + difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].medium_Percentage / 100f)) // �߱�
            {
                // ���� ���� �� �ִ� ���̵� �ִ� ���� ������
                if (current_Medeum_Order_Number_Of_Day >= difficulty_Limit_Of_Day[GameManager.Instance.current_Date].medium_Limit)
                {
                    continue;
                }
                target_List = medeum_Order_List;
                current_Medeum_Order_Number_Of_Day++;
                break;
            }
            else if (rand_Difficulty < (difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].easy_Percentage / 100f
                + difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].medium_Percentage / 100f
                + difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].hard_Percentage / 100f)) // ���
            {
                // ���� ���� �� �ִ� ���̵� �ִ� ���� ������
                if (current_Hard_Order_Number_Of_Day >= difficulty_Limit_Of_Day[GameManager.Instance.current_Date].hard_Limit)
                {
                    continue;
                }
                target_List = hard_Order_List;
                current_Hard_Order_Number_Of_Day++;
                break;
            }
            else
            {
                Debug.Log("��, ��, ��� ��� ���� �ȵ�.");
                break;
            }
        }

        int rand_Order = UnityEngine.Random.Range(0, target_List.Count); // Ÿ�� ����Ʈ���� ������ �ֹ� ���� ���ϱ�)

        return target_List[rand_Order]; // �ֹ� ��ȯ
    }
}
