using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum StoryState
{
    STORYING,
    SHOWTOTAL,
    NEXTPAGE,
}

public class StoryBlock
{
    public int storyBlockID;
    public string storyTexts;
}


public class StoryLoad : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI tTitle;
    [SerializeField] private TextMeshProUGUI tStory;

    public StoryBlock[] Texts;

    public StoryState stroyState;
    public void StateChange()
    {
       
        if(stroyState == StoryState.STORYING)           // 타이핑 중 -> 모든 글 보기 
        {

        }
        else if (stroyState == StoryState.SHOWTOTAL)   //  모든 글 보기 -> 밑으로
        {

        }
        else if (stroyState == StoryState.NEXTPAGE)     // 밑으로 -> 다음페이지로
        {

        }

    }


}
    

