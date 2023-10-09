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
       
        if(stroyState == StoryState.STORYING)           // Ÿ���� �� -> ��� �� ���� 
        {

        }
        else if (stroyState == StoryState.SHOWTOTAL)   //  ��� �� ���� -> ������
        {

        }
        else if (stroyState == StoryState.NEXTPAGE)     // ������ -> ������������
        {

        }

    }


}
    

