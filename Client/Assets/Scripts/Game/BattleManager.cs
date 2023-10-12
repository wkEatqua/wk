using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace WK.Battle
{
    public enum GameState
    {
        READY,
        START,
        GAME,
        END,
        RESULT
    }

    [SerializeField]
    public struct Player
    {
        public bool isPlayer;

        public Image myCharacterIMG;
        public TextMeshProUGUI myCharacterName; //캐릭터이름
        public int diceCount;
        //public List<Skill> Skills = new List<Skill>();
        public Skill[] Skills;

        //public List<int> DiceSlot = new List<int>();
        public int[] DiceSlot;
    }


    

    public class BattleManager : MonoBehaviour
    {
        [Header("====스테이지기본정보====")]
        //public string StageCode; //스테이지 코드
        public int[] diceRange = { 5, 11, 12 }; // 효과1단계 5 효과2단계 11 효과3단계 12
        public Sprite diceImg;

        [Space(10)]
        [Header("====플레이어UI====")]
        public Image myCharacterIMG;
        public TextMeshProUGUI myCharacterName; //캐릭터이름
        public Slider myHPSlider; //체력바
        public Slider myFatigueSlider; //피로도

        public DiceSlots[] myDiceSlot; //주사위슬롯
        public TextMeshProUGUI myDEM;   //DEM 게이지
        public TextMeshProUGUI myCOST; //개연성
        public Skill[] mySkills;
        
        [Space(10)]
        [Header("====상대적UI====")]
        public Image enemyCharacterIMG;
        public TextMeshProUGUI enemyCharacterName; //캐릭터이름
        public Slider enemyHPSlider; //체력바
        public Slider enemyFatigueSlider; //피로도

        public DiceSlots[] enemyDiceSlot; //주사위슬롯
        //public TextMeshProUGUI myDEM;   //DEM 게이지
        //public TextMeshProUGUI myCOST; //개연성
        public Skill[] enemySkills;

        [Space(20)]
        [Header("====팝업====")]
        public DicePopup dicePopup;


        public Player player = new Player();
        public Player enemy = new Player();

        [SerializeField] bool isMyTurn = true;  // true : 플레이어 턴  false: 적 턴
        int[] currentSlot;
        public GameObject interactionEnable; //상호작용 가능상태 true 불가능 false (default: false)

      

        GameState gameState = GameState.READY;

        


        public void init()
        {
            interactionEnable.SetActive(false);
            foreach (var ds in myDiceSlot)
                ds.slotDiceText.text = ""; //기본빈칸
        }
        public void initTurn()
        {
            //턴 돌아올때 초기화 해줄 것
        }

        private void Start()
        {
            init();
            StartCoroutine(GameBegin());
        }

      

        public void initPlayerDataLoad() //플레이어, 적 플레이어 데이터 로드
        {
            player.diceCount = 4; //임시 주사위갯수 --지울것
            //스테이지정보
            //적정보
            //소유캐릭터,스킬 정보...등등 로드

            gameState = GameState.START;
        }


        public void SwitchingCharacter()
        {
            //캐릭터 스위칭
        }

        

        // #0 게임시작 
        public IEnumerator GameBegin()
        {
            if (gameState != GameState.READY) StopCoroutine(GameBegin());

            interactionEnable.SetActive(true);
            initPlayerDataLoad();
            yield return new WaitForSeconds(1.0f);
            interactionEnable.SetActive(false);
            yield return null;  
        }







        #region # 주사위 굴리기 
        public void OnBtnClickDiceLoll()
        {
            if (dicePopup.gameObject.activeSelf) return; //이미활성화되어있다면 return

            interactionEnable.SetActive(false);
            StartCoroutine(BattleDiceRollingStart()); //주사위굴리기
        }


        public IEnumerator BattleDiceRollingStart()
        {
            Player currentPlayer = new Player();

            if (isMyTurn)   currentPlayer = player;
            else            currentPlayer = enemy;

           //yield return new WaitForSeconds(1.5f);
           dicePopup.gameObject.SetActive(true);
           dicePopup.SetDice(currentPlayer.diceCount);

            yield return null;
        }


        public IEnumerator BattleDiceRollFinish()
        {
            player.DiceSlot = new int[4];

            for (int i=0;i<dicePopup.diceResult.Count;i++)
            {
                if (isMyTurn) { player.DiceSlot[i] = dicePopup.diceResult[i]; }
                else enemy.DiceSlot[i] = dicePopup.diceResult[i];
            }
            yield return new WaitForSeconds(2.0f);

            dicePopup.gameObject.SetActive(false);

            for (int i = 0; i < player.DiceSlot.Length; i++)
            {
                if (isMyTurn) { myDiceSlot[i].SaveToDiceSlot(player.DiceSlot[i]); }
                else enemyDiceSlot[i].SaveToDiceSlot(enemy.DiceSlot[i]);

                yield return new WaitForSeconds(1.0f);
            }
            interactionEnable.SetActive(false);

            yield return null;
        }


        #endregion


        #region # 스킬발동

        #endregion



        #region Singleton
        public static BattleManager instance = null;
        public void Awake()
        {
            if (instance == null) instance = this;
            else instance = null;
        }
        #endregion

    }
}