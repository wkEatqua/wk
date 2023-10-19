using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceWindow : MonoBehaviour
{
    readonly List<DiceSlot> slots = new();
    public DiceSlot slotPrefab;
    public GameObject slotParent;

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI difficulty;
    public Button rerollButton;
    public Button finishButton;
    ScenarioSelectInfo info;

    UI_MainStory canvas;
    
    public void Init(int slotNum, ScenarioSelectInfo info,UI_MainStory canvas)
    {
        this.info = info;
        resultText.text = "";
        rerollButton.onClick.RemoveAllListeners();
        finishButton.onClick.RemoveAllListeners();
        this.canvas = canvas;
       
        rerollButton.onClick.AddListener(() => StartCoroutine(StartSlot()));

        for (int i = 0; i < slotNum; i++)
        {
            slots.Add(Instantiate(slotPrefab, slotParent.transform));
            slots[i].Init(i + 1);

            slots[i].name = "slot" + (i + 1);
        }
        DiceSlot slot = Instantiate(slotPrefab, slotParent.transform);
        slot.Init(1);
        slot.name = "slot" + (slotNum + 1);
        
        difficulty.text = info.SelectValue.ToString();
    }   

    IEnumerator StartSlot()
    {
        finishButton.enabled = false;
        rerollButton.enabled = false;
        int rand = Random.Range(0, slots.Count);
        Debug.Log(rand);
        int count = slots.Count * 2 + rand - 1;


        for (int i = 0, x = 0; i < count; i++, x++)
        {
            Vector2 startPos = slotParent.transform.localPosition;
            Vector2 endPos = slotParent.transform.localPosition - new Vector3(slotPrefab.GetComponent<RectTransform>().rect.width, 0, 0);

            float moveTime = Mathf.Lerp(0.02f, 0.4f, (float)i / count);
            float curTime = 0;

            while (curTime < moveTime)
            {
                slotParent.transform.localPosition = Vector2.Lerp(startPos, endPos, curTime / moveTime);
                yield return new WaitForEndOfFrame();
                curTime += Time.deltaTime;
                if (curTime > moveTime) curTime = moveTime;
            }

            slotParent.transform.localPosition = endPos;
            if (x == slots.Count - 1)
            {
                slotParent.transform.localPosition = Vector2.zero;
                x = -1;
            }
        }

        finishButton.enabled = true;
        
        if (rand >= info.SelectValue)
        {
            resultText.text = "성공";
            resultText.color = Color.green;
            finishButton.onClick.AddListener(() => canvas.ToNextPage(info));
        }
        else
        {
            resultText.text = "실패";
            resultText.color = Color.red;
        }
        finishButton.onClick.AddListener(() => { gameObject.SetActive(false); });

        rerollButton.enabled = true;
        rerollButton.onClick.RemoveAllListeners();
        rerollButton.onClick.AddListener(() => { canvas.Debug("더이상 굴릴 수 없습니다"); });
    }
}
