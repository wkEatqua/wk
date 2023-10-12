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
        public TextMeshProUGUI myCharacterName; //ĳ�����̸�
        public int diceCount;
        //public List<Skill> Skills = new List<Skill>();
        public Skill[] Skills;

        //public List<int> DiceSlot = new List<int>();
        public int[] DiceSlot;
    }


    

    public class BattleManager : MonoBehaviour
    {
        [Header("====���������⺻����====")]
        //public string StageCode; //�������� �ڵ�
        public int[] diceRange = { 5, 11, 12 }; // ȿ��1�ܰ� 5 ȿ��2�ܰ� 11 ȿ��3�ܰ� 12
        public Sprite diceImg;

        [Space(10)]
        [Header("====�÷��̾�UI====")]
        public Image myCharacterIMG;
        public TextMeshProUGUI myCharacterName; //ĳ�����̸�
        public Slider myHPSlider; //ü�¹�
        public Slider myFatigueSlider; //�Ƿε�

        public DiceSlots[] myDiceSlot; //�ֻ�������
        public TextMeshProUGUI myDEM;   //DEM ������
        public TextMeshProUGUI myCOST; //������
        public Skill[] mySkills;
        
        [Space(10)]
        [Header("====�����UI====")]
        public Image enemyCharacterIMG;
        public TextMeshProUGUI enemyCharacterName; //ĳ�����̸�
        public Slider enemyHPSlider; //ü�¹�
        public Slider enemyFatigueSlider; //�Ƿε�

        public DiceSlots[] enemyDiceSlot; //�ֻ�������
        //public TextMeshProUGUI myDEM;   //DEM ������
        //public TextMeshProUGUI myCOST; //������
        public Skill[] enemySkills;

        [Space(20)]
        [Header("====�˾�====")]
        public DicePopup dicePopup;


        public Player player = new Player();
        public Player enemy = new Player();

        [SerializeField] bool isMyTurn = true;  // true : �÷��̾� ��  false: �� ��
        int[] currentSlot;
        public GameObject interactionEnable; //��ȣ�ۿ� ���ɻ��� true �Ұ��� false (default: false)

      

        GameState gameState = GameState.READY;

        


        public void init()
        {
            interactionEnable.SetActive(false);
            foreach (var ds in myDiceSlot)
                ds.slotDiceText.text = ""; //�⺻��ĭ
        }
        public void initTurn()
        {
            //�� ���ƿö� �ʱ�ȭ ���� ��
        }

        private void Start()
        {
            init();
            StartCoroutine(GameBegin());
        }

      

        public void initPlayerDataLoad() //�÷��̾�, �� �÷��̾� ������ �ε�
        {
            player.diceCount = 4; //�ӽ� �ֻ������� --�����
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
            if (dicePopup.gameObject.activeSelf) return; //�̹�Ȱ��ȭ�Ǿ��ִٸ� return

            interactionEnable.SetActive(false);
            StartCoroutine(BattleDiceRollingStart()); //�ֻ���������
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


        #region # ��ų�ߵ�

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