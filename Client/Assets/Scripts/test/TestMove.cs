using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestMove : MonoBehaviour
{
    // Start is called before the first frame update
    float timing = 2f;
    float sum = 0;

    // 상 하 좌 우
    Vector3[] dir = new Vector3[4]{ Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sum += Time.deltaTime;
        if (sum >= timing)
        {
            sum = 0;
            int dirIndex = Random.Range(0, 3);
            Vector3 dest = dir[dirIndex];
            transform.DOMove(dest + transform.position, 1f);
            //transform.Translate(dest * 0.5f);
            //transform.position = Vector3.MoveTowards(transform.position, dest, 1);
        }
    }
}
