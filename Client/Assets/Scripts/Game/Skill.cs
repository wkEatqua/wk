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
        public TextMeshProUGUI skillName; //스킬이름
        public TextMeshProUGUI skillDesc; //스킬설명
        public TextMeshProUGUI ActiveProb; //적군 스킬일시, 발동확률 표기 (player일시 미표기)
        public Image SelectImg;

        public TextMeshProUGUI[] diceSection; // 1~5 / 6~11 / 12~12 
        public TextMeshProUGUI[] SkillDiceOption; //스킬 1~5 / 6~11 / 12~12 각 구간당 효과설명



        [Header("[ DATA ]")]
        private long ID;//아이디값
        private long ChararcterID; //사용 주체 ID
        private SkillName Name; //스킬 이름
        private SkillOption SkillOption; //스킬 기본 효과 정보

        private string SkillOptionText; //스킬 기본 효과 텍스트
        private int StaggerNum; //스킬 시전 시 가할 무력화 수치
        private string TransFormNum; //스킬 시전 시 충전되는 변신 수치

        private DiceOption FirstDiceOption; //주사위 강화 효과
        private string FirstDiceOptionText; //강화 효과 1번 텍스트
        private string FirstDiceOptionMinRate; //1번 강화 적용 주사위 눈 최소 구간
        private string FirstDiceOptionMaxRate; //1번 강화 적용 주사위 눈 최대 구간

        private DiceOption SecondDiceOption; //주사위 강화 효과
        private string SecondDiceOptionText; //강화 효과 2번 텍스트
        private string SecondDiceOptionMinRate; //2번 강화 적용 주사위 눈 최소 구간
        private string SecondDiceOptionMaxRate; //2번 강화 적용 주사위 눈 최대 구간

        private DiceOption ThirdDiceOption; //주사위 강화 효과
        private string ThirdDiceOptionText; //강화 효과 3번 텍스트
        private string ThirdDiceOptionMinRate; //3번 강화 적용 주사위 눈 최소 구간
        private string ThirdDiceOptionMaxRate; //3번 강화 적용 주사위 눈 최대 구간




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
            // 스킬 발동 계산
            Debug.Log("<<<<" + Name + "스킬강화!!>>>>> ");
        }

        public void SkillUse()
        {
            // 스킬사용 => 본인주사위 영향, 적 체력에 영향
            Debug.Log("<<<<" + Name + "스킬사용!!>>>>> ");
        }

        public void OnBtnClick()
        {
            //SkillDiceSetting()
        }

    }
}