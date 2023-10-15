using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditorInternal.ReorderableList;

namespace WK.Battle
{

    public class DiceSlots : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Image slotBGimage;
        public Image slotDiceNumImage;
        public TextMeshProUGUI slotDiceText;
        



        //data
        public int slotDiceNum;
        
        public void OnEnable()
        {
            //slotBGimage = GetComponent<Image>();
            //slotDiceNumImage = transform.GetChild(0).GetComponent<Image>();
            //slotDiceText = transform.GetChild(1).GetComponent<TextMeshProUGUI>(); //밑의 주사위 이미지로 치환될것
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




        // # 드래그앤 드롭 > 스킬활성화
        bool isDragging = false;
        Vector2 DefaultPos;

        public void OnBeginDrag(PointerEventData eventData)
        {
            DefaultPos = slotDiceNumImage.transform.position;
            isDragging = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 currentPos = eventData.position;
            slotDiceNumImage.transform.position = currentPos;
            isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            slotDiceNumImage.transform.position = DefaultPos;
            isDragging = false;
        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            if (isDragging)
            {
                var col = collision.GetComponent<Skill>();
                if (slotDiceNum < BattleManager.instance.diceRange[0])
                {

                }else if(slotDiceNum < BattleManager.instance.diceRange[1])
                {

                }
                else
                {

                }
                //col.SkillDiceSetting();
            }
        }


    }
}