using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class twinkle : MonoBehaviour
{
    private string ending_Text;
    public TMP_Text targetText;
    [SerializeField]
    private float delay = 0.1f;

    void Start()
    {
        ending_Text = targetText.text.ToString();
        targetText.text = " ";

        StartCoroutine(textPrint(delay));
    }

    IEnumerator textPrint(float d)
    {
        int count = 0;

        while (count != ending_Text.Length)
        {
            if (count < ending_Text.Length)
            {
                targetText.text += ending_Text[count].ToString();
                count++;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}