using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WK.Battle
{
    public class Hero // #수정중
    {
        public int id; //아이디
        public int Name; //이름
        public int HeroIcon; //아이콘
        public WeaponType weaponType; //무기타입 (ENUM)
        public int MainIllust; //일러스트
        public int HeroStamina; //
        public int NeedTransForm; //변신수치
        public int model3D;

        //스킬[6]
        public int[] SkillID;
        public int[] SkillProbabilityCost;
        public int[] SkillStamina; //6개

        public int StaminaDownSection; //피로도구간[4]

        public int[] StaminaDown; // 피로도총량[4] 
        public int[] StaminaDownRate; //4개 피로도감소수치

    }


    [Serializable]
    public class Enemy
    {
        //DATA  
        private string GroupID; //그룹 아이디
        private string ID; //아이디
        private string WorldID; //월드 아이디
        private string ChapterID; //챕터 아이디
        private string Name; //이름 텍스트
        private string HeroIcon; //경로 아이콘
        private DefenceType defenceType; //방어구타입
        private string MainIllust; //경로 일러스트
        private string TransFormValue; //변신수치 변신요구치
        private string DiceSkin; //주사위스킨
        public int BaseDiceCount; //소지 주사위 갯수
        public int MaxDiceCountFix; //최대 주사위 소지
        private int[] DiceRate; //주사위 눈 나올 확률 (1~12)
    }

    [Serializable]
    public class Player
    {
        //DATA
        public string HeroName; //이름
        public int MaxHPvalue; // 피통
        public int CurHPvalue; // 현재체력
        public int Stamina; //피로도
    }

    public class BattleInfo : MonoBehaviour
    {
        //public Image uiIcon;
        //public TextMeshProUGUI uiName; //캐릭터이름
        //public Slider uiHPbar; //체력바
        //public Slider uiStamina; //피로도




    }
}