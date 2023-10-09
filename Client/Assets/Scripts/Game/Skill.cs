using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace WK.Battle
{
    public class Skill : MonoBehaviour
    {
        bool isWaitSelcet;

        [Header("[ UI ]")]
        public TextMeshProUGUI skillName; //��ų�̸�
        public TextMeshProUGUI skillDesc; //��ų����
        public TextMeshProUGUI ActiveProb; //���� ��ų�Ͻ�, �ߵ�Ȯ�� ǥ�� (player�Ͻ� ��ǥ��)
        public Image SelectImg;

        public TextMeshProUGUI[] diceSection; // 1~5 / 6~11 / 12~12 
        public TextMeshProUGUI[] SkillDiceOption; //��ų 1~5 / 6~11 / 12~12 �� ������ ȿ������



        [Header("[ DATA ]")]
        private long ID;//���̵�
        private long ChararcterID; //��� ��ü ID
        private SkillName Name; //��ų �̸�
        private SkillOption SkillOption; //��ų �⺻ ȿ�� ����

        private string SkillOptionText; //��ų �⺻ ȿ�� �ؽ�Ʈ
        private int StaggerNum; //��ų ���� �� ���� ����ȭ ��ġ
        private string TransFormNum; //��ų ���� �� �����Ǵ� ���� ��ġ

        private DiceOption FirstDiceOption; //�ֻ��� ��ȭ ȿ��
        private string FirstDiceOptionText; //��ȭ ȿ�� 1�� �ؽ�Ʈ
        private string FirstDiceOptionMinRate; //1�� ��ȭ ���� �ֻ��� �� �ּ� ����
        private string FirstDiceOptionMaxRate; //1�� ��ȭ ���� �ֻ��� �� �ִ� ����

        private DiceOption SecondDiceOption; //�ֻ��� ��ȭ ȿ��
        private string SecondDiceOptionText; //��ȭ ȿ�� 2�� �ؽ�Ʈ
        private string SecondDiceOptionMinRate; //2�� ��ȭ ���� �ֻ��� �� �ּ� ����
        private string SecondDiceOptionMaxRate; //2�� ��ȭ ���� �ֻ��� �� �ִ� ����

        private DiceOption ThirdDiceOption; //�ֻ��� ��ȭ ȿ��
        private string ThirdDiceOptionText; //��ȭ ȿ�� 3�� �ؽ�Ʈ
        private string ThirdDiceOptionMinRate; //3�� ��ȭ ���� �ֻ��� �� �ּ� ����
        private string ThirdDiceOptionMaxRate; //3�� ��ȭ ���� �ֻ��� �� �ִ� ����




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
            FirstDiceOption = firstDiceOption;
            FirstDiceOptionText = firstDiceOptionText;
            FirstDiceOptionMinRate = firstDiceOptionMinRate;
            FirstDiceOptionMaxRate = firstDiceOptionMaxRate;
            SecondDiceOption = secondDiceOption;
            SecondDiceOptionText = secondDiceOptionText;
            SecondDiceOptionMinRate = secondDiceOptionMinRate;
            SecondDiceOptionMaxRate = secondDiceOptionMaxRate;
            ThirdDiceOption = thirdDiceOption;
            ThirdDiceOptionText = thirdDiceOptionText;
            ThirdDiceOptionMinRate = thirdDiceOptionMinRate;
            ThirdDiceOptionMaxRate = thirdDiceOptionMaxRate;
        }


        // real Data
        string DataSkillName;
        string DataskillDesc;
        string DataActiveProb;
        string[] DatadiceSection;
        string[] DataSkillDiceOption;

        public void SelectOn()
        {
            //isWaitSelcet.
        }


        public void SkillDiceSetting(int DiceNum)
        {
            //if(DiceNum)
            // ��ų �ߵ� ���
            Debug.Log("<<<<" + Name + "��ų��ȭ!!>>>>> ");
        }

        public void SkillUse()
        {
            // ��ų��� => �����ֻ��� ����, �� ü�¿� ����
            Debug.Log("<<<<" + Name + "��ų���!!>>>>> ");
        }

        public void OnBtnClick()
        {
            //SkillDiceSetting()
        }

    }
}