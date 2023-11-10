using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI;

namespace WK.Battle
{

    public class EnemyBattleInfo : BattleInfo
    {
        //Data
        public Enemy enemy;

        //UI
        public Image uiIcon;
        public TextMeshProUGUI uiName; //ĳ�����̸�
        public Slider uiHPbar; //ü�¹�
        public Slider uiStamina; //�Ƿε�

        public TextMeshProUGUI uiHPvalue;

        public DiceInventory[] uiDiceSlots; //�ֻ�������
        public TextMeshProUGUI uiATK;   //���ݷ�
        public TextMeshProUGUI uiCRI;   //ġ��Ÿ��


        public DiceInventory[] uiEnemyDiceSlots; //�ֻ�������
        public Skill[] uiSkills;


        

        public Skill[] skills; // - ��ų (��ų�����ּ�/��ų���̵�/�⺻����Ȯ��/Ȯ�����Ҽ�ġ) > ��ų���̺����
        public int[] diceValues;

        public float HPvalue;
        public float MAXHPvalue;

        public void DataLoad()
        {

        }

        public void init()
        {
            HPvalue = 100;
            MAXHPvalue = 100;
            // uiEnemyDiceSlots; //�ֻ������� �ʱ�ȭ
        }

        public void initTurn()
        {
            //�� ���ƿö� �ʱ�ȭ ���� ��
        }

        public void setHP(float hit)
        {
            //value update
            HPvalue = HPvalue - hit;

            //ui update
            uiHPbar.value = HPvalue/ MAXHPvalue;
            uiHPvalue.text = HPvalue + "/100";
        }


        public IEnumerator AttackLoutine()
        {
            //�ֻ���������

            //�ֻ��� �κ��� �ֱ�

            //�ֻ����� ��ų��ȭ(������ ����)

            //��ų���

            


            yield return null;
        }
    }
}