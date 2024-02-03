using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    [SerializeField]
    public Ingredients ingredient;
    public Image ingredient_Sprite;
    public Button ingredient_Button;

    private void Start()
    {
        ingredient_Sprite.sprite = ingredient.ingredients_UI_Sprite;
    }

    private void Update()
    {
        Unlock_Check();
    }

    // 재료 클릭 (사용)
    public void Click_Ingredient()
    {
        // 임시
        //Debug.Log($"{ingredient.like_Type}(이)가 좋아하는 {ingredient.ingredients_Name} 재료 사용!");
        //Debug.Log((int)ingredient.type);

        // 재료 햄버거에 사용
        GameManager.Instance.Cook_Hamburger(ingredient);
    }

    // 해금 여부 체크 및 반영
    public void Unlock_Check()
    {
        if (GameManager.Instance.shop_Level < ingredient.available_Shop_Level)
        {
            // 잠금
            ingredient_Button.interactable = false; // 버튼 비활성화
            ColorBlock colorBlock1 = ingredient_Button.colors;
            colorBlock1.normalColor = new Color(0f, 0f, 0f, 0.8f);
            ingredient_Button.colors = colorBlock1;

            return;
        }

        // 현재 가게 레벨에서 사용 가능한 재료라면 해금
        ingredient_Button.interactable = true; // 버튼 활성화
        ColorBlock colorBlock2 = ingredient_Button.colors;
        colorBlock2.normalColor = new Color(1f, 1f, 1f, 1f);
        ingredient_Button.colors = colorBlock2;
    }
}
