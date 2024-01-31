using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patience_Gauge : MonoBehaviour
{
    [Header("참을성 게이지들")]
    [SerializeField]
    private GameObject[] patience_Guages;

    private int patience_Level=10;

    //손님이 오면 5초 정도 기다리고 흘러가게(아마 손님 프리펩에 이 코드를 넣을 것 같습니다)한다
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
            // 배열을 삭제하기 전에 2초 기다리기
            yield return new WaitForSeconds(2f);
            patience_Guages[i].SetActive(false);
            patience_Level--;
        }
    }
    
    
}
