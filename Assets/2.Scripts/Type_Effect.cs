using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Type_Effect : MonoBehaviour
{
    [Header("타이핑 속도")]
    [SerializeField]
    public float Char_Per_Seconds; // 타이핑 속도
    string targetMsg;
    TMP_Text msg_Text;
    int index;
    bool current_Is_Order;

    [SerializeField]
    public bool is_Type_Effecting; // 타이핑 진행 중인지 여부

    private void Awake()
    {
        msg_Text = GetComponent<TMP_Text>();
    }

    public void SetMsg(string msg, bool is_Order)
    {
        if (is_Order)
        {
            current_Is_Order = true;
        }
        else
        {
            current_Is_Order = false;
        }

        // 스킵 기능
        if (is_Type_Effecting)
        {
            msg_Text.text = targetMsg;
            CancelInvoke();
            Effect_End();
        }
        else
        {
            targetMsg = msg;
            Effect_Start();
        }
    }

    void Effect_Start()
    {
        msg_Text.text = "";
        index = 0;

        is_Type_Effecting = true;

        float gap_Timp = 1 / Char_Per_Seconds;
        Invoke("Effecting", gap_Timp);
    }

    void Effecting()
    {
        if (msg_Text.text == targetMsg)
        {
            Effect_End();
            return;
        }

        msg_Text.text += targetMsg[index];
        index++;

        float gap_Timp = 1 / Char_Per_Seconds;
        Invoke("Effecting", gap_Timp);
    }

    void Effect_End()
    {
        is_Type_Effecting = false;

        if (current_Is_Order)
        {
            GameManager.Instance.is_End_Current_Order = true;
        }
    }
}
