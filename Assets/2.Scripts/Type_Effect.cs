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

    private void Awake()
    {
        msg_Text = GetComponent<TMP_Text>();
    }

    public void SetMsg(string msg)
    {
        targetMsg = msg;
        Effect_Start();
    }

    void Effect_Start()
    {
        msg_Text.text = "";
        index = 0;

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
        GameManager.Instance.is_End_Current_Order = true;
    }
}
