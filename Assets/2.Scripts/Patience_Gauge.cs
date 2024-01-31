using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patience_Gauge : MonoBehaviour
{
    [Header("������ ��������")]
    [SerializeField]
    private GameObject[] patience_Guages;

    private int patience_Level=10;

    //�մ��� ���� 5�� ���� ��ٸ��� �귯����(�Ƹ� �մ� �����鿡 �� �ڵ带 ���� �� �����ϴ�)�Ѵ�
    void Start()
    {
        StartCoroutine(DeleteArrays());
    }

    
    private void Update()
    {
        
    }

    IEnumerator DeleteArrays()
    {
        for (int i = 0; i < 10; i++)
        {
            // �迭�� �����ϱ� ���� 2�� ��ٸ���
            yield return new WaitForSeconds(2f);
            patience_Guages[i].SetActive(false);
            patience_Level--;
        }
    }
    
    
}
