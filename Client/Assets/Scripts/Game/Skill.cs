using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace WK.Battle
{
    public class Skill : MonoBehaviour
    {
        public bool AlreadyHaveDiceSkill = false; //���̽��� ����� ���� ? true yes false no
        

        //UI
        public TextMeshProUGUI skillName; //��ų�̸�
        public TextMeshProUGUI skillDesc; //��ų����
        public TextMeshProUGUI ActiveProb; //���� ��ų�Ͻ�, �ߵ�Ȯ�� ǥ�� (player�Ͻ� ��ǥ��)
        public Image SelectImg;

        public TextMeshProUGUI[] diceSection; // 1~5 / 6~11 / 12~12 
        public TextMeshProUGUI[] SkillDiceOption; //��ų 1~5 / 6~11 / 12~12 �� ������ ȿ������

        public TextMeshProUGUI tskillCost; //�Һ� ������
        public int skillCost = 2; //�Һ� ������

        // [ DATA ] 
        private long ID;//���̵�
        private long ChararcterID; //��� ��ü ID
        private SkillName Name; //��ų �̸�
        private SkillOption SkillOption; //��ų �⺻ ȿ�� ����

        private string SkillOptionText; //��ų �⺻ ȿ�� �ؽ�Ʈ
        private int StaggerNum; //��ų ���� �� ���� ����ȭ ��ġ
        private string TransFormNum; //��ų ���� �� �����Ǵ� ���� ��ġ

        private DiceOption[] DiceOption; //�ֻ��� ��ȭ ȿ��
        private string[] DiceOptionText; //��ȭ ȿ�� 1~3�� �ؽ�Ʈ
        private string[] DiceOptionMinRate; //1~3�� ��ȭ ���� �ֻ��� �� �ּ� ����
        private string[] DiceOptionMaxRate; //1~3�� ��ȭ ���� �ֻ��� �� �ִ� ����


        public Skill(long iD, long chararcterID, SkillName name, SkillOption skillOption, string skillOptionText, int staggerNum, string transFormNum,
            DiceOption firstDiceOption, string firstDiceOptionText, string firstDiceOptionMinRate, string firstDiceOptionMaxRate,
            DiceOption secondDiceOption, string secondDiceOptionText, string secondDiceOptionMinRate, string secondDiceOptionMaxRate,
            DiceOption thirdDiceOption, string thirdDiceOptionText, string thirdDiceOptionMinRate, string thirdDiceOptionMaxRate)
        {
            ID = iD;
            ChararcterID = chararcterID;
            Name = name;
            SkillOption = skillOption;
            SkillOptionText = skillOptionText;
            StaggerNum = staggerNum;
            TransFormNum = transFormNum;


            DiceOption[0]           = firstDiceOption;
            DiceOptionText[0]       = firstDiceOptionText;
            DiceOptionMinRate[0]    = firstDiceOptionMinRate;
            DiceOptionMaxRate[0]    = firstDiceOptionMaxRate;

            DiceOption[1]           = secondDiceOption;
            DiceOptionText[1]       = secondDiceOptionText;
            DiceOptionMinRate[1]    = secondDiceOptionMinRate;
            DiceOptionMaxRate[1]    = secondDiceOptionMaxRate;

            DiceOption[2]           = thirdDiceOption;
            DiceOptionText[2]       = thirdDiceOptionText;
            DiceOptionMinRate[2]    = thirdDiceOptionMinRate;
            DiceOptionMaxRate[2]    = thirdDiceOptionMaxRate;
        }


        // real Data
        string DataSkillName;
        string DataskillDesc;
        string DataActiveProb;
        string[] DatadiceSection;
        string[] DataSkillDiceOption;


        //��ų��ȭ(�ֻ����ֱ�)
        public void SkillDiceSetting(int DiceNum,GameObject eachDice)
        {
            if (AlreadyHaveDiceSkill) 
            { 
                Debug.Log("�̹̰�ȭ�� ��ų�Դϴ�");
                return;
            }



            int[] dicerange = BattleManager.instance.diceRange;
            

            if(DiceNum <= dicerange[0])
            {
                FontsRed(diceSection[0], SkillDiceOption[0]);
            }
            else if (DiceNum <= dicerange[1])
            {
                FontsRed(diceSection[1], SkillDiceOption[1]);
            }
            else
            {
                FontsRed(diceSection[2], SkillDiceOption[2]);
            }

            AlreadyHaveDiceSkill = true;
            eachDice.gameObject.SetActive(false);

            // ��ų �ߵ� ���
            Debug.Log("<<<<" + gameObject.name+ "��ų��ȭ!!>>>>> ");
        }

        
        
        public void AttackSkillUse()
        {
            // ��ų��� => �����ֻ��� ����, �� ü�¿� ����
            Debug.Log("<<<<" + gameObject.name + " ���� ��ų���!!>>>>> ");
        }


        public void BuffSkillUse()
        {
            // ��ų��� => �����ֻ��� ����, �� ü�¿� ����
            Debug.Log("<<<<" + gameObject.name + " ���� ��ų���!!>>>>> ");
        }

        //��ų���
        public void OnBtnClick()
        {
            //1. ������ ��ġ �ִ���?
            if(BattleManager.instance.Player.COSTGuage< skillCost) { Debug.Log("�������� �����մϴ�"); return; }

            //2. ����? ����?
            if(SkillOption == SkillOption.Buff) //2-1. ������� � ��ų ��ȭ
            {
                BuffSkillUse();
            }
            else if(SkillOption == SkillOption.Attack)//2-2. �����̶�� ����!
            {
                AttackSkillUse();
            }


            // �� ���ȿ� ����
            //END.  DEM������
        }










        public void FontsRed(TextMeshProUGUI diceSection, TextMeshProUGUI SkillDiceOption)
        {
            string tempText = string.Empty;

            tempText = "<color=red>" + diceSection.text+ "</color>";
            diceSection.text = tempText;
            tempText = "<color=red>" + SkillDiceOption.text+ "</color>";
            SkillDiceOption.text = tempText;
        }

    }
}