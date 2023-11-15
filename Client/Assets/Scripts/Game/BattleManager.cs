using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;

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

        [Header("====���������⺻����====")]
        public int[] diceRange = { 5, 11, 12 }; // ȿ��1�ܰ� 5 ȿ��2�ܰ� 11 ȿ��3�ܰ� 12
        public Sprite diceImg;


        [SerializeField] public PlayerBattleInfo Player;
        [SerializeField] public EnemyBattleInfo Enemy;

        [Space(20)]
        [Header("====�˾�====")]
        public DicePopup DiceRollingPAN; //�ֻ�����������
         

        [SerializeField] bool isMyTurn = true;  // true : �÷��̾� ��  false: �� ��
        public GameObject interactionEnable; //��ȣ�ۿ� ���ɻ��� true �Ұ��� false

        [Space(20)]
        [Header("====��ų����Ʈ====")]
        [SerializeField] public GameObject Effect_HURT;
        [SerializeField] public GameObject Effect_ATTACK;

        GameState gameState = GameState.READY;



       
        public TextMeshProUGUI uiRelationShowText; //������ �ؽ�Ʈ
        
        public TextMeshProUGUI uiDEMShowText; // dem ������ �ؽ�Ʈ
        public Image uiDEMShowGauge; // dem ������ �̹���

        int RelationShipValue = 5;
        int DEMValue = 0;

        int MAXDEMGuage = 100; //dem ������ full
        int MAXRelatinshipGuage = 10; //relationship ������ full
        int MAXRelationshiporigin = 5; //relationship 


        
        public void GlobalDataLoad()
        {
            //�������� �����ͷε�

            

            Player.DataLoad();
            Enemy.DataLoad();
        }



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

        public void Update()
        {
            
        }

 

            public void initPlayerDataLoad() //�÷��̾�, �� �÷��̾� ������ �ε�
        {
            Player.HavingDice = 4; //�ӽ� �ֻ������� --�����
            //������������
            //������
            //����ĳ����,��ų ����...��� �ε�

            gameState = GameState.START;
        }


        public void SwitchingCharacter()
        {
            //ĳ���� ����Ī
        }

        

        // #0 ���ӽ��� 
        public IEnumerator GameBegin()
        {
            if (gameState != GameState.READY) StopCoroutine(GameBegin());

            interactionEnable.SetActive(true);
            initPlayerDataLoad();
            yield return new WaitForSeconds(1.0f);
            interactionEnable.SetActive(false);
            yield return null;  
        }







        #region # �ֻ��� ������ 
        public void OnBtnClickDiceLoll()
        {
            if (DiceRollingPAN.gameObject.activeSelf) return; //�̹�Ȱ��ȭ�Ǿ��ִٸ� return

            interactionEnable.SetActive(false);
            StartCoroutine(BattleDiceRollingStart()); //�ֻ���������
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
                //DiceRollingPAN.SetDice(Enemy.BaseDiceCount); 
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


        #region # ��ų�ߵ�

        // ������ ������ ����
        public void SetRelationGauge(int point)
        {
            if (MAXRelatinshipGuage < MAXRelationshiporigin) return;
            RelationShipValue -= point;
            uiRelationShowText.text = "������\n"+ RelationShipValue + "/" + MAXRelationshiporigin;
        }

        // DEM ������ ����
        public void SetDEMGauge(int point)
        {
            DEMValue += point;

            uiDEMShowGauge.fillAmount = DEMValue / MAXDEMGuage; 
            //uiDEMShowText.text = "DEM\n"+Mathf.Floor(MAXDEMGuage / DEMValue) * 100 +"%"; // ä���� �ۼ�Ʈ
            uiDEMShowText.text = "DEM\n"+ (DEMValue/ MAXDEMGuage)*100 +"%"; // ä���� �ۼ�Ʈ

            Debug.Log("DEMValue = "+DEMValue);
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




        // UI ��ȣ�ۿ� ================== (�ӽ�)

        public void PlayEffectHURT()
        {
            Effect_HURT.SetActive(false);
            Effect_HURT.SetActive(true);
        }

        public void PlayEffectATTACK()
        {
            Effect_ATTACK.SetActive(false);
            Effect_ATTACK.SetActive(true);
        }

        //=================================
    }
}