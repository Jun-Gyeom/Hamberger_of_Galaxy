using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 재료 타입
[SerializeField]
public enum Like_Type
{
    none, // 없음
    earth, // 지구인
    space, // 우주인
    magic, // 마법사
    rich // 부자
}

// 재료 종류
[SerializeField]
public enum Type
{
    delete, // 삭제
    bun, // 햄버거 빵
    lettuce, // 양상추
    cheese, // 치즈
    meat_Patty, // 고기 패티
    pickle, // 피클
    tomato, // 토마토
    fried_Egg, // 계란 후라이
    mint_Choco, // 민트초코
    dragon_Wings, // 말린 용의 날개
    steamed_Mandragora, // 만드라고라 찜
    eagle_Constellation, // 독수리 자리의 편린
    space_Squid_Patty, // 우주 오징어 패티
    gold_Bullion // 금괴
}
[CreateAssetMenu]
public class Ingredients : ScriptableObject
{
    // 사용 가능한 가게 레벨
    [SerializeField]
    public int available_Shop_Level;
    // 재료 이미지
    [SerializeField]
    public Sprite ingredients_Sprite;
    // 재료 이름
    [SerializeField]
    public string ingredients_Name;
    // 해금 여부
    public bool is_Unlock;
    // 재료 타입
    [SerializeField]
    public Like_Type like_Type;
    // 재료 종류
    [SerializeField]
    public Type type;
}
