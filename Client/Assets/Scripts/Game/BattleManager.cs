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
        public TextMeshProUGUI myCharacterName; //ĳ�����̸�
        public int diceCount;
        //public List<Skill> Skills = new List<Skill>();
        public Skill[] Skills;

        //public List<int> DiceSlot = new List<int>();
        public int[] DiceSlot;
    }


    

    public class BattleManager : MonoBehaviour
    {
        //[Header("[ ========== STAGE INFO ============ ]")]
        //public string StageCode; //�������� �ڵ�
        //[Space(30)]

        [Header("[ PLAYER INFO ]")]
        public Image myCharacterIMG;
        public TextMeshProUGUI myCharacterName; //ĳ�����̸�
        public Slider myHPSlider; //ü�¹�
        public Slider myFatigueSlider; //�Ƿε�

        public DiceSlots[] myDiceSlot; //�ֻ�������
        public TextMeshProUGUI myDEM;   //DEM ������
        public TextMeshProUGUI myCOST; //������
        public Skill[] mySkills;
        
        [Space(30)]
        [Header("[ ENEMY INFO  ]")]
        public Image enemyCharacterIMG;
        public TextMeshProUGUI enemyCharacterName; //ĳ�����̸�
        public Slider enemyHPSlider; //ü�¹�
        public Slider enemyFatigueSlider; //�Ƿε�

        public TextMeshProUGUI[] enemyDiceSlot; //�ֻ�������
        //public TextMeshProUGUI myDEM;   //DEM ������
        //public TextMeshProUGUI myCOST; //������
        public Skill[] enemySkills;

        [SerializeField]
        public Player[] players;


        public Player player = new Player();
        public Player enemy = new Player();

        bool isMyTurn = true;  // true : �÷��̾� ��  false: �� ��
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
            //�� ���ƿö� �ʱ�ȭ ���� ��
        }

        public void initPlayerDataLoad() //�÷��̾�, �� �÷��̾� ������ �ε�
        {
            player.diceCount = 4; //�ӽ� �ֻ������� --�����
            gameState = GameState.START;
        }


        public void SwitchingCharacter()
        {
            //ĳ���� ����Ī
        }

        





        public IEnumerator GameBegin()
        {
            if (gameState != GameState.READY) StopCoroutine(GameBegin());

            interactionEnable = false;
            initPlayerDataLoad();
            yield return new WaitForSeconds(1.0f); //1�ʰ� ��ȣ�ۿ� �Ұ�
            StartCoroutine(BattleDiceRollingStart()); //�ֻ���������
            

            yield return null;  
        }


        




        #region DICE LOLLING �ֻ��� ������

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
                myDiceSlot[i].slotDiceText.text = currentSlot[i].ToString(); //�ֻ��� ���Կ��ֱ�
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