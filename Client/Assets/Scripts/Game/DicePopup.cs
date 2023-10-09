using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WK.Battle
{

    public class DicePopup : MonoBehaviour
    {
        public BattleManager battleManager;


        public DiceRolling[] dices;
        public int diceCount;

        public List<int> diceResult = new List<int>();
        bool once = false;
        public void Initialize()
        {
            battleManager.init();
            once = false;
            diceResult = new List<int>();
            foreach (var dice in dices)
                dice.gameObject.SetActive(false);
        }

        public void OnEnable()
        {
            Initialize();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < diceCount; i++)
                {
                    dices[i].stopDice = true;
                }
            }

            if (diceResult.Count == diceCount && !once)
            {
                battleManager.StartCoroutine(battleManager.BattleDiceRollFinish());
                once = true;
            }
        }


        public void SetDice(int mydiceCount)
        {
            diceCount = mydiceCount;
            for (int i = 0; i < diceCount; i++)
                dices[i].gameObject.SetActive(true);
        }


    }
}