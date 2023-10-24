using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WK.Battle
{

    public class EnemyBattleInfo : MonoBehaviour
    {
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
        public  int BaseDiceCount; //소지 주사위 갯수
        public int MaxDiceCountFix; //최대 주사위 소지
        private int[] DiceRate; //주사위 눈 나올 확률 (1~12)

        public Skill[] skills; // - 스킬 (스킬슬롯주소/스킬아이디/기본선택확률/확률감소수치) > 스킬테이블따로
        public int[] diceValues;

        public float HPvalue;
        public float MAXHPvalue;


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
    }
}