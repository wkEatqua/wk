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



    public class PlayerBattleInfo : MonoBehaviour
    {
        //UI
        public Image uiIcon;
        public TextMeshProUGUI uiName; //캐릭터이름
        public Slider uiHPbar; //체력바
        public Slider uiStamina; //피로도

        public TextMeshProUGUI uiDEM;   //DEM 게이지
        public TextMeshProUGUI uiCost; //개연성

        public TextMeshProUGUI uiATK; //공격력 
        public TextMeshProUGUI uiDEF; //방어력
        public TextMeshProUGUI uiCRT; //치명타율
        public TextMeshProUGUI uiLUK; //행운


        public DiceSlots[] uiDiceSlots; //주사위슬롯
       




        //DATA
        public string HeroName; //이름
        public int MaxHPvalue; // 피통
        public int CurHPvalue; // 현재체력
        public int Stamina; //피로도
        public int DEMGuage; //DEM(궁)
        public int COSTGuage; //개연성

        public int HavingDice; //소지 주사위
        public int MaxDiceSlot; //최대주사위
                            
        public string ATK; //공격력
        public string DEF; //방어력
        public string CRT; //치명타율
        public string LUK; //행운

        public Hero hero;
        public Skill[] Skills;
        public int[] diceValues;

        public void init()
        {
            //uiPlayerDiceSlots 초기화
        }

        public void initTurn()
        {
            //턴 돌아올때 초기화 해줄 것
        }
    }
}