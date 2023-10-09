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

        //다이스 범위설정
        public void SetDiceRange(int min, int max)
        {
            DiceMin = min;
            DiceMax = max;
        }

        private void OnEnable()
        {
            diceBG = GetComponent<Image>();
            diceNum = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            stopDice = false;
            once = false;
        }

        float timer;
        void Update()
        {
            if (stopDice && !once)  //다이스 숫자 멈춤
            {
                int tmpdiceNum = Random.Range(DiceMin, DiceMax);
                diceNum.text = tmpdiceNum.ToString();
                dicePopup.diceResult.Add(tmpdiceNum);

                once = true;
            }
            else if (!stopDice) //다이스 숫자 바뀜
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