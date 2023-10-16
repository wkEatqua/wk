using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace WK.Battle
{

    public class BattleManager : MonoBehaviour
    {
        public enum GameState
        {
            READY,
            START,
            GAME,
            END,
            RESULT
        }

        [Header("====스테이지기본정보====")]
        public int[] diceRange = { 5, 11, 12 }; // 효과1단계 5 효과2단계 11 효과3단계 12
        public Sprite diceImg;


        public PlayerBattleInfo Player;
        public EnemyBattleInfo Enemy;

        [Space(20)]
        [Header("====팝업====")]
        public DicePopup DiceRollingPAN; //주사위굴리는판
         

        [SerializeField] bool isMyTurn = true;  // true : 플레이어 턴  false: 적 턴
        public GameObject interactionEnable; //상호작용 가능상태 true 불가능 false

      

        GameState gameState = GameState.READY;

        


        public void init()
        {
            interactionEnable.SetActive(false);

            Player.init();
            Enemy.init();
        }



        private void Start()
        {
            init();
            StartCoroutine(GameBegin());
        }

      

        public void initPlayerDataLoad() //플레이어, 적 플레이어 데이터 로드
        {
            Player.HavingDice = 4; //임시 주사위갯수 --지울것
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
            if (DiceRollingPAN.gameObject.activeSelf) return; //이미활성화되어있다면 return

            interactionEnable.SetActive(false);
            StartCoroutine(BattleDiceRollingStart()); //주사위굴리기
        }


        public IEnumerator BattleDiceRollingStart()
        {

            //yield return new WaitForSeconds(1.5f);
            DiceRollingPAN.gameObject.SetActive(true);
            if (isMyTurn)
            { 
                DiceRollingPAN.SetDice(Player.HavingDice); 
            }
            else 
            { 
                DiceRollingPAN.SetDice(Enemy.BaseDiceCount); 
            }
            

            yield return null;
        }


        public IEnumerator BattleDiceRollFinish()
        {                           
            Player.diceValues = new int[4];

         
            for (int i=0;i<DiceRollingPAN.diceResult.Count;i++)
            {
                if (isMyTurn) { Player.diceValues[i] = DiceRollingPAN.diceResult[i]; }
                else Enemy.diceValues[i] = DiceRollingPAN.diceResult[i];
            }
            yield return new WaitForSeconds(2.0f);

            DiceRollingPAN.gameObject.SetActive(false);
            interactionEnable.SetActive(true);
            for (int i = 0; i < Player.diceValues.Length; i++)
            {
                if (isMyTurn)
                {
                    Player.uiDiceSlots[i].Dice.SaveToDiceSlot(Player.diceValues[i]);
                    Player.uiDiceSlots[i].Dice.gameObject.SetActive(true);
                }
                else {
                    Enemy.uiDiceSlots[i].Dice.SaveToDiceSlot(Enemy.diceValues[i]);
                    Enemy.uiDiceSlots[i].Dice.gameObject.SetActive(true);
                }

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