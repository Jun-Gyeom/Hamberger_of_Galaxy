using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TalkData
{
    [Header("��ȭ ID (�ֹ� ID�� ��ġ)")]
    [SerializeField]
    public int id;
    [Header("��ȭ �޽���")]
    [SerializeField]
    public string messeges;
}
public class TalkManager : MonoBehaviour
{
    public Dictionary<int, string> talkData_Dictionary;
    [Header("��ȭ ������ ���")]
    [SerializeField]
    public TalkData[] talk_Data;
    

    void Awake()
    {
        talkData_Dictionary = new Dictionary<int, string>();
        GenerateData();
    }

    // ��ȭ ������ �߰�
    void GenerateData()
    {
        for (int i = 0;  i < talk_Data.Length; i++)
        {
            // ������ �߰�
            talkData_Dictionary.Add(talk_Data[i].id, talk_Data[i].messeges);
        }
    }

    // ��ȭ �ҷ�����
    public string Get_Talk(int id)
    {
        // ��ȭ �ҷ�����
        return talkData_Dictionary[id];
    }
}
