using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WK.Battle
{

    public class DiceSlots : MonoBehaviour
    {
        public Image slotBGimage;
        public TextMeshProUGUI slotDiceText;
        public Image slotDiceNumImage;



        //data
        public int slotDiceNum;
        
        public void OnEnable()
        {
            slotBGimage = GetComponent<Image>();
            slotDiceText = transform.GetChild(0).GetComponent<TextMeshProUGUI>(); //���� �ֻ��� �̹����� ġȯ�ɰ�
            //imageDiceNum = transform.GetChild(0).GetComponent<Image>();
        }

        public void DataLoad(Image BGImage, Image diceImage, int diceNum)
        {
            slotBGimage = BGImage;
            slotDiceText.text = diceNum.ToString();
            slotDiceNumImage = diceImage;

            slotDiceNum = diceNum; 
        }

    }
}