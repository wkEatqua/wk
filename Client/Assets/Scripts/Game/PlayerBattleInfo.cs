using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

namespace WK.Battle
{

    public class PlayerBattleInfo : BattleInfo
    {
        //Data
        public Player player;
        public Hero hero;

        //UI
        public Image uiIcon;
        public TextMeshProUGUI uiName; //캐릭터이름
        public Slider uiHPbar; //체력바
        public Slider uiStamina; //피로도

        public TextMeshProUGUI uiDEM;   //DEM 게이지
        public TextMeshProUGUI uiCost; //개연성

        public Image uiDEMGauge; // dem 게이지 이미지

        public TextMeshProUGUI uiATK; //공격력 
        public TextMeshProUGUI uiDEF; //방어력
        public TextMeshProUGUI uiCRT; //치명타율
        public TextMeshProUGUI uiLUK; //행운


        public DiceInventory[] uiDiceSlots; //주사위슬롯



        


        public int DEMGuage; //DEM(궁)
        public int COSTGuage; //개연성

        int MAXDEMGuage = 100; //dem 게이지 full
        int MAXRLSGuage = 10; //relationship 게이지 full
        int MAXRLSorigin = 5; //relationship 


        public int HavingDice; //소지 주사위
        public int MaxDiceSlot; //최대주사위
                            
        public string ATK; //공격력
        public string DEF; //방어력
        public string CRT; //치명타율
        public string LUK; //행운


        public Skill[] Skills;
        public int[] diceValues;


        public void DataLoad()
        {

        }


        public void init()
        {
            //uiPlayerDiceSlots 초기화
        }

        public void initTurn()
        {
            //턴 돌아올때 초기화 해줄 것
        }



        //// 개연성 게이지 변경
        //public void SetRelationGauge(int point)
        //{
        //    if (MAXRLSGuage < MAXRLSorigin) return;
        //    RelationShipValue -= point;
        //    uiRelationShowText.text = "개연성\n" + RelationShipValue + "/" + MAXRLSorigin;
        //}

        //// DEM 게이지 변경
        //public void SetDEMGauge(int point)
        //{
        //    DEMValue += point;

        //    uiDEMGauge.fillAmount = DEMValue / MAXDEMGuage;
        //    //uiDEMShowText.text = "DEM\n"+Mathf.Floor(MAXDEMGuage / DEMValue) * 100 +"%"; // 채워진 퍼센트
        //    uiDEMGauge.text = "DEM\n" + (DEMValue / MAXDEMGuage) * 100 + "%"; // 채워진 퍼센트

        //    Debug.Log("DEMValue = " + DEMValue);
        //}


    }
}