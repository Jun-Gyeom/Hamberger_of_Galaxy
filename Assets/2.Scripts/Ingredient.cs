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

    // ��� Ŭ�� (���)
    public void Click_Ingredient()
    {
        // �ӽ�
        //Debug.Log($"{ingredient.like_Type}(��)�� �����ϴ� {ingredient.ingredients_Name} ��� ���!");
        //Debug.Log((int)ingredient.type);

        // ��� �ܹ��ſ� ���
        GameManager.Instance.Cook_Hamburger(ingredient);
    }

    // �ر� ���� üũ �� �ݿ�
    public void Unlock_Check()
    {
        if (GameManager.Instance.shop_Level < ingredient.available_Shop_Level)
        {
            // ���
            ingredient_Button.interactable = false; // ��ư ��Ȱ��ȭ
            ColorBlock colorBlock1 = ingredient_Button.colors;
            colorBlock1.normalColor = new Color(0f, 0f, 0f, 0.8f);
            ingredient_Button.colors = colorBlock1;

            return;
        }

        // ���� ���� �������� ��� ������ ����� �ر�
        ingredient_Button.interactable = true; // ��ư Ȱ��ȭ
        ColorBlock colorBlock2 = ingredient_Button.colors;
        colorBlock2.normalColor = new Color(1f, 1f, 1f, 1f);
        ingredient_Button.colors = colorBlock2;
    }
}
