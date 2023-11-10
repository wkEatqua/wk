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
        public TextMeshProUGUI uiName; //캐릭터이름
        public Slider uiHPbar; //체력바
        public Slider uiStamina; //피로도

        public TextMeshProUGUI uiHPvalue;

        public DiceInventory[] uiDiceSlots; //주사위슬롯
        public TextMeshProUGUI uiATK;   //공격력
        public TextMeshProUGUI uiCRI;   //치명타율


        public DiceInventory[] uiEnemyDiceSlots; //주사위슬롯
        public Skill[] uiSkills;


        

        public Skill[] skills; // - 스킬 (스킬슬롯주소/스킬아이디/기본선택확률/확률감소수치) > 스킬테이블따로
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
            // uiEnemyDiceSlots; //주사위슬롯 초기화
        }

        public void initTurn()
        {
            //턴 돌아올때 초기화 해줄 것
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
            //주사위굴리기

            //주사위 인벤에 넣기

            //주사위로 스킬강화(버프랑 나눔)

            //스킬사용

            


            yield return null;
        }
    }
}