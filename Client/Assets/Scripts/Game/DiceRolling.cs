using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace WK.Battle
{

    public class DiceRolling : MonoBehaviour
    {
        // parents
        public DicePopup dicePopup;

        // UI
        public Image diceBG;
        public TextMeshProUGUI diceNum;

        // INFO
        int DiceMin = 1;
        int DiceMax = 12;

        public bool stopDice = false;
        public bool once = false;

        //���̽� ��������
        public void SetDiceRange(int min, int max)
        {
            DiceMin = min;
            DiceMax = max;
        }

        private void OnEnable()
        {
            diceBG = GetComponent<Image>();
            diceNum = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            dicePopup = BattleManager.instance.DiceRollingPAN;

            stopDice = false;
            once = false;
        }

        float timer;
        void Update()
        {
            if (stopDice && !once)  //���̽� ���� ����
            {
                int tmpdiceNum = Random.Range(DiceMin, DiceMax);
                diceNum.text = tmpdiceNum.ToString();
                dicePopup.diceResult.Add(tmpdiceNum);

                once = true;
            }
            else if (!stopDice) //���̽� ���� �ٲ�
            {
                timer += Time.deltaTime;
                if (timer >= 0.1f)
                {
                    diceNum.text = Random.Range(DiceMin, DiceMax).ToString();
                    timer = 0;
                }
            }
        }

     


    }
}