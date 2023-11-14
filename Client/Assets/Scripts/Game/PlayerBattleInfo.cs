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
        public TextMeshProUGUI uiName; //ĳ�����̸�
        public Slider uiHPbar; //ü�¹�
        public Slider uiStamina; //�Ƿε�

        public TextMeshProUGUI uiDEM;   //DEM ������
        public TextMeshProUGUI uiCost; //������

        public Image uiDEMGauge; // dem ������ �̹���

        public TextMeshProUGUI uiATK; //���ݷ� 
        public TextMeshProUGUI uiDEF; //����
        public TextMeshProUGUI uiCRT; //ġ��Ÿ��
        public TextMeshProUGUI uiLUK; //���


        public DiceInventory[] uiDiceSlots; //�ֻ�������



        


        public int DEMGuage; //DEM(��)
        public int COSTGuage; //������

        int MAXDEMGuage = 100; //dem ������ full
        int MAXRLSGuage = 10; //relationship ������ full
        int MAXRLSorigin = 5; //relationship 


        public int HavingDice; //���� �ֻ���
        public int MaxDiceSlot; //�ִ��ֻ���
                            
        public string ATK; //���ݷ�
        public string DEF; //����
        public string CRT; //ġ��Ÿ��
        public string LUK; //���

        
        public Skill[] Skills;
        public int[] diceValues;


        public void DataLoad()
        {

        }


        public void init()
        {
            //uiPlayerDiceSlots �ʱ�ȭ
        }

        public void initTurn()
        {
            //�� ���ƿö� �ʱ�ȭ ���� ��
        }



        //// ������ ������ ����
        //public void SetRelationGauge(int point)
        //{
        //    if (MAXRLSGuage < MAXRLSorigin) return;
        //    RelationShipValue -= point;
        //    uiRelationShowText.text = "������\n" + RelationShipValue + "/" + MAXRLSorigin;
        //}

        //// DEM ������ ����
        //public void SetDEMGauge(int point)
        //{
        //    DEMValue += point;

        //    uiDEMGauge.fillAmount = DEMValue / MAXDEMGuage;
        //    //uiDEMShowText.text = "DEM\n"+Mathf.Floor(MAXDEMGuage / DEMValue) * 100 +"%"; // ä���� �ۼ�Ʈ
        //    uiDEMGauge.text = "DEM\n" + (DEMValue / MAXDEMGuage) * 100 + "%"; // ä���� �ۼ�Ʈ

        //    Debug.Log("DEMValue = " + DEMValue);
        //}


    }
}