using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace WK.Battle
{
    public class Skill : MonoBehaviour
    {
        public bool AlreadyHaveDiceSkill = false; //다이스가 적용된 상태 ? true yes false no
        

        //UI
        public TextMeshProUGUI skillName; //스킬이름
        public TextMeshProUGUI skillDesc; //스킬설명
        public TextMeshProUGUI ActiveProb; //적군 스킬일시, 발동확률 표기 (player일시 미표기)
        public Image SelectImg;

        public TextMeshProUGUI[] diceSection; // 1~5 / 6~11 / 12~12 
        public TextMeshProUGUI[] SkillDiceOption; //스킬 1~5 / 6~11 / 12~12 각 구간당 효과설명

        public TextMeshProUGUI tskillCost; //소비 개연성
        public int skillCost = 2; //소비 개연성

        // [ DATA ] 
        private long ID;//아이디값
        private long ChararcterID; //사용 주체 ID
        private SkillName Name; //스킬 이름
        private SkillOption SkillOption; //스킬 기본 효과 정보

        private string SkillOptionText; //스킬 기본 효과 텍스트
        private int StaggerNum; //스킬 시전 시 가할 무력화 수치
        private string TransFormNum; //스킬 시전 시 충전되는 변신 수치

        private DiceOption[] DiceOption; //주사위 강화 효과
        private string[] DiceOptionText; //강화 효과 1~3번 텍스트
        private string[] DiceOptionMinRate; //1~3번 강화 적용 주사위 눈 최소 구간
        private string[] DiceOptionMaxRate; //1~3번 강화 적용 주사위 눈 최대 구간


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


        public void OnEnable()
        {
            SelectImg.enabled = false;


        }

        //스킬강화(주사위넣기)
        public void SkillDiceSetting(int DiceNum,GameObject eachDice)
        {
            if (AlreadyHaveDiceSkill) 
            { 
                Debug.Log("이미강화된 스킬입니다");
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

            // 스킬 발동 계산
            Debug.Log("<<<<" + gameObject.name+ "스킬강화!!>>>>> ");

            SelectImg.enabled = true;
        }

        
        
        public void AttackSkillUse()
        {
            // 스킬사용 => 본인주사위 영향, 적 체력에 영향
            Debug.Log("<<<<" + gameObject.name + " 공격 스킬사용!!>>>>> ");

            BattleManager.instance.Enemy.setHP(10); //임시 데미지

            BattleManager.instance.PlayEffectATTACK();



            BattleManager.instance.SetDEMGauge(5);
            BattleManager.instance.SetRelationGauge(2);
        }


        public void BuffSkillUse()
        {
            // 스킬사용 => 본인주사위 영향, 적 체력에 영향
            Debug.Log("<<<<" + gameObject.name + " 버프 스킬사용!!>>>>> ");
        }

        //스킬사용
        public void OnBtnClick()
        {

            //1. 개연성 수치 있는지?
            if (BattleManager.instance.Player.COSTGuage< skillCost) { Debug.Log("개연성이 부족합니다"); return; }

            //2. 버프? 공격?
            if(SkillOption == SkillOption.Buff) //2-1. 버프라면 어떤 스킬 강화
            {
                BuffSkillUse();
            }
            else if(SkillOption == SkillOption.Attack)//2-2. 공격이라면 어택!
            {
                AttackSkillUse();
            }


            // 적 스탯에 영향
            

            //END.  DEM게이지
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