using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditorInternal.ReorderableList;

namespace WK.Battle
{

    public class DiceSlots : MonoBehaviour
    {
        public EachDice Dice;
        //data
        public int slotDiceNum;
        
        public void OnEnable()
        {
            Dice = transform.GetChild(0).GetComponent<EachDice>();
        }

        public void DataLoad(Image BGImage, Image diceImage, int diceNum)
        {
        //    slotBGimage = BGImage;
        //    slotDiceText.text = diceNum.ToString();
        //    slotDiceNumImage = diceImage;

        //    slotDiceNum = diceNum; 
        }
        



        


    }
}