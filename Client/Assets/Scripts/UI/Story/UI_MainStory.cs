using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Shared.Data;
using UnityEngine.UI;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using Shared.Model;
using Febucci.UI;
using System.Linq;

public class UI_MainStory : MonoBehaviour
{
    [Header("프리팹")]
    public SelectButton selectButton;
    public UI_StoryShow storyTextPrefab;
    public Image storySDImage;

    [Header("UI")]
    public GameObject selectArea;
    public TextMeshProUGUI worldTitle;
    public TextMeshProUGUI chapterTitle;
    public Image illustration;
    public TextMeshProUGUI progress;
    public TextMeshProUGUI verisimilitude;
    public Image staminaBar;
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI consoleText;
    public DiceWindow diceWindow;
    public GameObject gameOverWindow;
    public Transform storyParent;
    public Button nextPageButton;
    public TextMeshProUGUI selectText;

    List<ScenarioPageTextInfo> pageTexts;
    List<ScenarioSelectInfo> selectInfoGroup;
    List<ScenarioPageImageInfo> pageImages;


    readonly List<SelectButton> selectButtons = new();
    ScenarioChapterInfo ChapterInfo => MainStoryManager.ChapterInfo;
    ScenarioWorldInfo WorldInfo => MainStoryManager.WorldInfo;
    List<ScenarioPageInfo> Pages => MainStoryManager.Pages;
    int page;

    int curSelectedVeris;
    int curMaxVeris;
    int tempMaxVeris;

    int stamina;

    int Stamina
    {
        get { return stamina; }
        set
        {
            if (value > ChapterInfo.DefaultEnergyMax)
            {
                stamina = ChapterInfo.DefaultEnergyMax;
            }
            else if (value < 0)
            {
                stamina = 0;
            }
            else
            {
                stamina = value;
            }
        }
    }
    int hp;
    int Verisimilitude
    {
        get
        {
            if (curMaxVeris <= 0) return 0;
            int value = Mathf.RoundToInt((float)curSelectedVeris / curMaxVeris * 100f);
            return value > 100 ? 100 : value;
        }
    }


    ObjectPool<UI_StoryShow> storyPool;
    ObjectPool<Image> imagePool;
    private void Start()
    {
        storyPool = new ObjectPool<UI_StoryShow>(new UI_StoryShow[] { storyTextPrefab });
        imagePool = new ObjectPool<Image>(new Image[] { storySDImage });
        page = 0;
        worldTitle.text = WorldInfo.Name + ", ";
        chapterTitle.text = ChapterInfo.Name;

        stamina = ChapterInfo.DefaultEnergyMax;
        hp = ChapterInfo.DefaultHealthMax;
        ShowPage();
    }
    private void Update()
    {
        verisimilitude.text = Verisimilitude.ToString() + "%";
        progress.text = Mathf.RoundToInt((float)page / Pages.Count * 100).ToString() + "%";
        staminaBar.fillAmount = Stamina / 100f;
        staminaText.text = Stamina.ToString();
    }

    List<UI_StoryShow> storyList = new List<UI_StoryShow>();
    List<Image> storyImageList = new List<Image>();
    public void ShowPage()
    {
        consoleText.text = "";
        selectText.text = "";
        nextPageButton.enabled = false;
        ScenarioData.TryGetPageText(Pages[page].UniqueId, out pageTexts);
        ScenarioData.TryGetPageImages(Pages[page].UniqueId, out pageImages);

        illustration.gameObject.SetActive(pageImages != null);

        foreach (var x in storyList)
        {
            storyPool.Return(x);
        }
        foreach(var x in storyImageList)
        {
            imagePool.Return(x);
        }
        storyList.Clear();
        storyImageList.Clear();
        for (int i = 0; i < pageTexts.Count; i++)
        {
            var st = storyPool.Get("Story Show");
            st.transform.SetParent(storyParent);
            ScenarioPageImageInfo imageInfo = null;
            if (pageImages != null)
            {
                foreach (var x in pageImages)
                {
                    if (x.ImageActiveOrder == i)
                    {
                        imageInfo = x;
                        break;
                    }
                }
            }
            st.Init(this, pageTexts[i], imageInfo);
            storyList.Add(st);
            Image img = imagePool.Get("Image");
            img.gameObject.SetActive(false);
            img.transform.SetParent(storyParent);
            storyImageList.Add(img);
            st.typeWriter.onTextShowed.AddListener(() =>
            {
                img.gameObject.SetActive(true);
            });
        }

        storyList[0].typeWriter.StartShowingText();

        for (int i = 0; i < storyList.Count - 1; i++)
        {
            int temp = i;
            storyList[i].typeWriter.onTextShowed.AddListener(() => storyList[temp + 1].typeWriter.StartShowingText());
        }
        storyList.Last().typeWriter.onTextShowed.AddListener(ShowSelections);
        
        selectButtons.Clear();
    }

    public void ShowImage(EventMarker eventMarker)
    {
        if (!(eventMarker.name == nameof(ShowImage))) return;
        illustration.sprite = Resources.Load<Sprite>("image/Story/" + eventMarker.parameters[0]);
    }

    public void EnergyDown(EventMarker eventMarker)
    {
        if (!(eventMarker.name == nameof(EnergyDown))) return;
        Stamina--;

        if (Stamina == 0)
        {
            GameOver();
        }
    }
    public void ShowSelections()
    {
        if (stamina <= 0)
        {

            return;
        }
        ScenarioData.TryGetSelectGroup(Pages[page].SelectGroupId, out selectInfoGroup);

        for (int i = 0; i < selectInfoGroup.Count; i++)
        {
            SelectButton obj = Instantiate(selectButton, selectArea.transform);
            obj.tmp.text = selectInfoGroup[i].SelectText;
            obj.tmp2.text = $"(기력 {selectInfoGroup[i].SelectEnergy} 회복)";

            switch (selectInfoGroup[i].SelectType)
            {
                case SelectType.None:
                    break;
                case SelectType.Dice:
                    obj.tmp2.text += $" 주사위 {selectInfoGroup[i].SelectValue} 필요";
                    break;
            }

            obj.info = selectInfoGroup[i];
            obj.OnClick.AddListener(OnSelect);
            selectButtons.Add(obj);
            tempMaxVeris = Mathf.Max(tempMaxVeris, selectInfoGroup[i].SelectVerisimilitude);
        }
    }
    public void OnSelect(ScenarioSelectInfo info)
    {
        if (page >= Pages.Count - 1)
        {
            DebugText("마지막 페이지입니다.");
            return;
        }
        switch (info.SelectType)
        {
            case SelectType.None:
                ToNextPage(info);
                break;
            case SelectType.Dice:
                diceWindow.gameObject.SetActive(true);
                if (!info.isDiced)
                {
                    diceWindow.Init(20, info, this);
                    info.isDiced = true;
                }
                break;
        }
    }
    public void DebugText(string log)
    {
        consoleText.text = log;
    }

    public void ToNextPage(ScenarioSelectInfo info)
    {
        selectText.text = info.ResultText;
        page++;
        curSelectedVeris += info.SelectVerisimilitude;
        curMaxVeris += tempMaxVeris;

        Stamina += info.SelectEnergy;
        foreach (var x in selectButtons)
        {
            Destroy(x.gameObject);
        }
        if (selectText.text == "")
        {
            ShowPage();
        }
    }

    public void GameOver()
    {
        gameOverWindow.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneChangeManager.instance.SceneMove(sceneName);
    }

    public void Skip()
    {
        foreach (var x in storyList)
        {
            x.typeWriter.SkipTypewriter();
        }
    }
}
