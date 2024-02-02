using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    easy,   // 초급
    Medium, // 중급
    Hard    // 고급
}

[Serializable]
public class Difficulty_Limit
{
    [Header("중급 난이도 주문 갯수 제한")]
    public int medium_Limit;
    [Header("고급 난이도 주문 갯수 제한")]
    public int hard_Limit;
}


[Serializable]
public class Difficulty_Percentage
{
    [Header("초급 난이도 주문 확률")]
    public float easy_Percentage;
    [Header("중급 난이도 주문 확률")]
    public float medium_Percentage;
    [Header("고급 난이도 주문 확률")]
    public float hard_Percentage;
}

[Serializable]
public class Order
{
    public int id; // 주문 ID (Key 값)
    public Difficulty difficulty;
}

public class OrderManager : MonoBehaviour
{
    [Header("날짜별 중급, 고급 난이도 주문 갯수 제한")]
    [SerializeField]
    public Difficulty_Limit[] difficulty_Limit_Of_Day; // <Day, Limit>
    [Header("날짜별 초급, 중급, 고급 난이도 주문 등장 확률")]
    [SerializeField]
    public Difficulty_Percentage[] difficulty_Percentage_Of_Day;
    [Header("주문 목록")]
    [SerializeField]
    public List <Order> ordet_List;

    [SerializeField] // 임시
    private List<Order> easy_Order_List;
    [SerializeField] // 임시
    private List<Order> medeum_Order_List;
    [SerializeField] // 임시
    private List<Order> hard_Order_List;

    [SerializeField] // 임시
    private List<Order> target_List;

    private int current_Medeum_Order_Number_Of_Day;  // 현재 일차에서 중급 난이도 주문 나온 횟수
    private int current_Hard_Order_Number_Of_Day;    // 현재 일차에서 고급 난이도 주문 나온 횟수

    private void Awake()
    {
        Order_Difficulty_Sorting();
    }

    // 주문 난이도 별 분류 함수
    void Order_Difficulty_Sorting()
    {
        foreach (Order order in ordet_List)
        {
            if (order.difficulty == Difficulty.easy) // 초급
            {
                easy_Order_List.Add(order);
            }
            else if (order.difficulty == Difficulty.Medium) // 중급
            {
                medeum_Order_List.Add(order);
            }
            else if (order.difficulty == Difficulty.Hard) // 고급
            {
                hard_Order_List.Add(order);
            }
        }
    }

    // 랜덤 난이도 뽑기 및 주문 뽑기
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

            // 메모
            // ex) 초급 60, 중급 30, 고급 10
            // 초급 ( 걍초급
            // 중급 ( 초급 + 중급
            // 고급 ( 초급 + 중급 + 고급

            if (rand_Difficulty < (difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].easy_Percentage / 100f))  // 초급
            {
                target_List = easy_Order_List;
                break;
            }
            else if (rand_Difficulty < (difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].easy_Percentage / 100f
                + difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].medium_Percentage / 100f)) // 중급
            {
                // 오늘 나올 수 있는 난이도 최대 갯수 넘으면
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
                + difficulty_Percentage_Of_Day[GameManager.Instance.current_Date].hard_Percentage / 100f)) // 고급
            {
                // 오늘 나올 수 있는 난이도 최대 갯수 넘으면
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
                Debug.Log("초, 중, 고급 모두 포함 안됨.");
                break;
            }
        }

        int rand_Order = UnityEngine.Random.Range(0, target_List.Count); // 타겟 리스트에서 랜덤한 주문 난수 구하기)

        return target_List[rand_Order]; // 주문 반환
    }
}
