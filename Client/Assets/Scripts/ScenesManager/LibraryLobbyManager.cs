using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibraryLobbyManager : Singleton<LibraryLobbyManager>
{
    public WorldSelection[] worldSelections;
    [HideInInspector] public bool IsMoving = false;

    [SerializeField] GameObject chapterList;
    [SerializeField] Transform chapterParent;
    [SerializeField] GameObject popUp;
    [SerializeField] Button chapterButtonPrefab;

    [HideInInspector] public long worldId;
    [HideInInspector] public long chapterId;
    protected override void Awake()
    {
        base.Awake();
        IsMoving = false;
        for (int i = 0; i < worldSelections.Length; i++)
        {
            Button button = worldSelections[i].GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            int temp = i;

            button.onClick.AddListener(() =>
            {
                if (Instance.IsMoving) return;
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
    }
    public void OnClickBackBtn()
    {
        SceneChangeManager.instance.SceneMove("MainLobby");
    }

    public void OnClickMainStory()
    {
        SceneChangeManager.instance.SceneMove("MainStory");
        MainStoryManager.Instance.chapterId = chapterId;
        MainStoryManager.Instance.worldId = worldId;
    }

    public void OpenChaterList(long worldIndex)
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

}
