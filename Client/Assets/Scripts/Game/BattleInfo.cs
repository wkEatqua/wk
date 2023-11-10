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

    public class BattleInfo : MonoBehaviour
    {
        //public Image uiIcon;
        //public TextMeshProUGUI uiName; //캐릭터이름
        //public Slider uiHPbar; //체력바
        //public Slider uiStamina; //피로도




    }
}