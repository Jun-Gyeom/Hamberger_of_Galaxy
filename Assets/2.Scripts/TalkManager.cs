using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TalkData
{
    [Header("대화 ID (주문 ID와 일치)")]
    [SerializeField]
    public int id;
    [Header("대화 메시지")]
    [SerializeField]
    public string messeges;
}
public class TalkManager : MonoBehaviour
{
    public Dictionary<int, string> talkData_Dictionary;
    [Header("대화 데이터 목록")]
    [SerializeField]
    public TalkData[] talk_Data;
    

    void Awake()
    {
        talkData_Dictionary = new Dictionary<int, string>();
        GenerateData();
    }

    // 대화 데이터 추가
    void GenerateData()
    {
        for (int i = 0;  i < talk_Data.Length; i++)
        {
            // 데이터 추가
            talkData_Dictionary.Add(talk_Data[i].id, talk_Data[i].messeges);
        }
    }

    // 대화 불러오기
    public string Get_Talk(int id)
    {
        // 대화 불러오기
        return talkData_Dictionary[id];
    }
}
