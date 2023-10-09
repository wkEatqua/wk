using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UIElements;

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
        //[Header("[ ========== STAGE INFO ============ ]")]
        //public string StageCode; //스테이지 코드
        //[Space(30)]

        [Header("[ PLAYER INFO ]")]
        public Image myCharacterIMG;
        public TextMeshProUGUI myCharacterName; //캐릭터이름
        public Slider myHPSlider; //체력바
        public Slider myFatigueSlider; //피로도

        public DiceSlots[] myDiceSlot; //주사위슬롯
        public TextMeshProUGUI myDEM;   //DEM 게이지
        public TextMeshProUGUI myCOST; //개연성
        public Skill[] mySkills;
        
        [Space(30)]
        [Header("[ ENEMY INFO  ]")]
        public Image enemyCharacterIMG;
        public TextMeshProUGUI enemyCharacterName; //캐릭터이름
        public Slider enemyHPSlider; //체력바
        public Slider enemyFatigueSlider; //피로도

        public TextMeshProUGUI[] enemyDiceSlot; //주사위슬롯
        //public TextMeshProUGUI myDEM;   //DEM 게이지
        //public TextMeshProUGUI myCOST; //개연성
        public Skill[] enemySkills;

        [SerializeField]
        public Player[] players;


        public Player player = new Player();
        public Player enemy = new Player();

        bool isMyTurn = true;  // true : 플레이어 턴  false: 적 턴
        bool interactionEnable = false;

        public DicePopup dicePopup;

        GameState gameState = GameState.READY;

        public int[] diceRange = { 5, 11, 12 };


        public void init()
        {
            foreach (var ds in myDiceSlot)
                ds.slotDiceText.text = "DiceSlot";
        }
        private void Start()
        {
            StartCoroutine(GameBegin());
        }

        public void initTurn()
        {
            //턴 돌아올때 초기화 해줄 것
        }

        public void initPlayerDataLoad() //플레이어, 적 플레이어 데이터 로드
        {
            player.diceCount = 4; //임시 주사위갯수 --지울것
            gameState = GameState.START;
        }


        public void SwitchingCharacter()
        {
            //캐릭터 스위칭
        }

        





        public IEnumerator GameBegin()
        {
            if (gameState != GameState.READY) StopCoroutine(GameBegin());

            interactionEnable = false;
            initPlayerDataLoad();
            yield return new WaitForSeconds(1.0f); //1초간 상호작용 불가
            StartCoroutine(BattleDiceRollingStart()); //주사위굴리기
            

            yield return null;  
        }


        




        #region DICE LOLLING 주사위 굴리기

        public IEnumerator BattleDiceRollingStart()
        {
            Player currentPlayer = new Player();

            if (isMyTurn) currentPlayer = player;
            else currentPlayer = enemy;

            yield return new WaitForSeconds(1.5f);
           dicePopup.gameObject.SetActive(true);
           dicePopup.SetDice(currentPlayer.diceCount);

            yield return null;
        }


        public IEnumerator BattleDiceRollFinish()
        {
            //List<int> currentSlot = new List<int>();
            int[] currentSlot;

            if (isMyTurn) currentSlot = player.DiceSlot;
            else currentSlot = enemy.DiceSlot;

            for(int i=0;i<dicePopup.diceResult.Count;i++)
            {
                currentSlot[i] = dicePopup.diceResult[i];
            }
            


            yield return new WaitForSeconds(2.0f);

            dicePopup.gameObject.SetActive(false);

            for (int i = 0; i < currentSlot.Length; i++)
            {
                myDiceSlot[i].slotDiceText.text = currentSlot[i].ToString(); //주사위 슬롯에넣기
                yield return new WaitForSeconds(1.0f);
            }
            interactionEnable = true;

            yield return null;
        }


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