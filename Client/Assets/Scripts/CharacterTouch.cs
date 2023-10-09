using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CharacterTouch : MonoBehaviour
{
    public string characterName;    //캐릭터정보
    public GameObject talkBalloon;  //말풍선

    public void Start()
    {
        InvokeRepeating("Move", 5.0f, 5.0f);
    }

    public void Move()
    {
        transform.DOMove(new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 4)), 6.0f);
        
    }

    private void OnMouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            talkBalloon.transform.position = Camera.main.WorldToScreenPoint(hit.transform.position);
            talkBalloon.SetActive(true);
        }
    }

    private void Update()
    {
        talkBalloon.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
    }

}
