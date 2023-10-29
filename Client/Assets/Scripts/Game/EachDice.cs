using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace WK.Battle
{
    public class EachDice : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        //ui
        public Image slotDiceNumImage;
        public TextMeshProUGUI slotDiceText;

        //data
        public int slotDiceNum;

        BoxCollider2D box2d;



        // # 드래그앤 드롭 > 스킬활성화
        bool isDragging = false;
        Vector2 DefaultPos;

        public void OnBeginDrag(PointerEventData eventData)
        {
            DefaultPos = this.transform.position;
            isDragging = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 currentPos = eventData.position;
            this.transform.position = currentPos;
            isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(currentPickSkill)
            {
                currentPickSkill.GetComponent<Skill>().SkillDiceSetting(slotDiceNum,this.gameObject);
            }

            this.transform.position = DefaultPos;
            isDragging = false;
        }


        public void SaveToDiceSlot(int dicenum)
        {
            slotDiceNumImage.gameObject.SetActive(true);

            slotDiceText.text = dicenum.ToString();
            slotDiceNum = dicenum;
        }



        public GameObject currentPickSkill;
        //public void OnCollisionEnter2D(Collision2D col)
        //{
        //    if (col.collider.CompareTag("SkillSlot"))
        //    { 
        //        Debug.Log("스킬발동:" + col);
        //        currentPickSkill = col.gameObject;
        //    }
        //}

        public void OnCollisionStay2D(Collision2D col)
        {
            if (col.collider.CompareTag("SkillSlot"))
            {
                Debug.Log("스킬발동:" + col);
                currentPickSkill = col.gameObject;
            }
        }


        public void OnCollisionExit2D(Collision2D col)
        {
            if (col.collider.CompareTag("SkillSlot"))
            {
                currentPickSkill = null;
            }
        }

    }
}