using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// �ֻ��� ������ Ȯ�����
/// </summary>
public class ProbeOfDice : MonoBehaviour
{
    public TextMeshProUGUI testText;

    public float[] diceProbe ={ 0.083f, 0.083f, 0.083f, 0.083f, 0.083f,
                                0.083f, 0.083f, 0.083f, 0.083f, 0.083f,
                                0.083f, 0.083f}; //�ӽ�

    public float Choose()
    {
        float[] probs = diceProbe;

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length-1;
    }


    public void onBtnClick()
    {
        testText.text = Choose().ToString();
    }
}
