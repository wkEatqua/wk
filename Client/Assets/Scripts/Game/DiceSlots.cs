using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WK.Battle
{

    public class DiceSlots : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Image slotBGimage;
        public Image slotDiceNumImage;
        public TextMeshProUGUI slotDiceText;

        public BoxCollider2D box2d;

        //data
        public int slotDiceNum;


        public void OnEnable()
        {
            //slotBGimage = GetComponent<Image>();
            //slotDiceNumImage = transform.GetChild(0).GetComponent<Image>();
            //slotDiceText = transform.GetChild(1).GetComponent<TextMeshProUGUI>(); //���� �ֻ��� �̹����� ġȯ�ɰ�
        }

        public void init()
        {
            slotDiceNumImage.gameObject.SetActive(false);

            slotDiceText.text = "0";
            slotDiceNum = 0;
        }

        public void DataLoad(Image BGImage, Image diceImage, int diceNum)
        {
            slotBGimage = BGImage;
            slotDiceText.text = diceNum.ToString();
            slotDiceNumImage = diceImage;

            slotDiceNum = diceNum; 
        }


        public void SaveToDiceSlot(int dicenum)
        {
            slotDiceNumImage.gameObject.SetActive(true);

            slotDiceText.text = dicenum.ToString();
            slotDiceNum = dicenum;
        }




        // # �巡�׾� ��� > ��ųȰ��ȭ
        bool isDragging = false;
        Vector2 DefaultPos;

        public void OnBeginDrag(PointerEventData eventData)
        {
            DefaultPos = slotDiceNumImage.transform.position;
            //DefaultPos = transform.position;
            isDragging = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 currentPos = eventData.position;
            slotDiceNumImage.transform.position = currentPos;
            //transform.position = currentPos;
            isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);



            slotDiceNumImage.transform.position = DefaultPos;
            //transform.position = DefaultPos;
            isDragging = false;

        }

        public void ShowNum()
        {
            Debug.Log(slotDiceNum);
        }


        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    Debug.Log("�⺻: "+collision.gameObject.name);
        //    if (collision.gameObject.tag.Equals("SkillSlot"))
        //    {
        //        Debug.Log("�浹: "+collision.gameObject.name);
        //        collision.gameObject.GetComponent<Skill>().ShowNum();
        //    }
        //}

    }
}