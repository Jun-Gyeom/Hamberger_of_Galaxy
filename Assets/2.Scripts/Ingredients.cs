using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� Ÿ��
[SerializeField]
public enum Like_Type
{
    none, // ����
    earth, // ������
    space, // ������
    magic, // ������
    rich // ����
}

// ��� ����
[SerializeField]
public enum Type
{
    delete, // ����
    bun, // �ܹ��� ��
    lettuce, // �����
    cheese, // ġ��
    meat_Patty, // ��� ��Ƽ
    pickle, // ��Ŭ
    tomato, // �丶��
    fried_Egg, // ��� �Ķ���
    mint_Choco, // ��Ʈ����
    dragon_Wings, // ���� ���� ����
    steamed_Mandragora, // ������� ��
    eagle_Constellation, // ������ �ڸ��� ��
    space_Squid_Patty, // ���� ��¡�� ��Ƽ
    gold_Bullion // �ݱ�
}
[CreateAssetMenu]
public class Ingredients : ScriptableObject
{
    // ��� ������ ���� ����
    [SerializeField]
    public int available_Shop_Level;
    // ��� �̹���
    [SerializeField]
    public Sprite ingredients_Sprite;
    // ��� �̸�
    [SerializeField]
    public string ingredients_Name;
    // �ر� ����
    public bool is_Unlock;
    // ��� Ÿ��
    [SerializeField]
    public Like_Type like_Type;
    // ��� ����
    [SerializeField]
    public Type type;
}
