using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibraryLobbyManager : MonoBehaviour
{
    public WorldSelection[] worldSelections;
    [HideInInspector] public bool IsMoving = false;

    [SerializeField] GameObject chapterList;
    [SerializeField] Transform chapterParent;
    [SerializeField] GameObject popUp;
    [SerializeField] Button chapterButtonPrefab;
    [SerializeField] GameObject speechBubble;
    [SerializeField] TextMeshProUGUI bubbleText;
    [HideInInspector] public long worldId;
    [HideInInspector] public long chapterId;

    [TextArea] [SerializeField] string[] libraryScripts;

    static LibraryLobbyManager instance;
    public static LibraryLobbyManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        isShowing = false;
        instance = this;
        IsMoving = false;

        for (int i = 0; i < worldSelections.Length; i++)
        {
            Button button = worldSelections[i].GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            int temp = i;

            button.onClick.AddListener(() =>
            {
                if (IsMoving) return;
                for (int j = 0; j < worldSelections.Length; j++)
                {
                    if (j == temp) continue;
                    
                    worldSelections[j].selected = false;
                    worldSelections[j].titleText.text = "";
                    worldSelections[j].transform.SetSiblingIndex(i);
                }
            });
        }

        worldSelections[2].Select();       

        if(GameManager.Instance.scriptIndex >= 0)
        {
            ShowSpeechBubble();
        }
        else
        {
            GameManager.Instance.scriptIndex = 0;
        }
    }
    public void OnClickBackBtn()
    {
        SceneChangeManager.instance.SceneMove("MainLobby");
    }

    public void OnClickMainStory()
    {
        SceneChangeManager.instance.SceneMove("MainStory");
        MainStoryManager.chapterId = chapterId;
        MainStoryManager.worldId = worldId;
        
    }

    public void OpenChapterList(long worldIndex)
    {
        chapterList.SetActive(true);

        ScenarioData.TryGetChapterGroup(worldIndex, out List<ScenarioChapterInfo> list);

        foreach (var chapter in list)
        {
            Button chapterButton = Instantiate(Resources.Load<Button>("Prefabs/ChapterButton"), chapterParent);
            chapterButton.GetComponentInChildren<TextMeshProUGUI>().text = chapter.Name;
            chapterButton.onClick.RemoveAllListeners();
            chapterButton.onClick.AddListener(() =>
            {
                popUp.SetActive(true);
                chapterId = chapter.UniqueId;
            });
        }
    }
    bool isShowing;
    public void ShowSpeechBubble()
    {
        if (isShowing) return;
        isShowing = true;
        int index = Random.Range(0, libraryScripts.Length);
        while (index == GameManager.Instance.scriptIndex)
        {
            index = Random.Range(0, libraryScripts.Length);
        }
        GameManager.Instance.scriptIndex = index;
        speechBubble.SetActive(true);
        bubbleText.text = libraryScripts[index];

        Invoke(nameof(TurnOffSpeechBubble), 4);
    }

    void TurnOffSpeechBubble()
    {
        speechBubble.SetActive(false);
        isShowing = false;
    }
}
