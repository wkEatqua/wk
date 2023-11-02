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
using Unity.VisualScripting;

public class UI_MainStory : MonoBehaviour
{
    [Header("프리팹")]
    public SelectButton selectButton;
    public UI_StoryShow storyTextPrefab;
    public Image storySDImage;
    public GameObject resultTextPrefab;

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
    public GameObject ending;
    
    List<ScenarioPageTextInfo> pageTexts;
    List<ScenarioSelectInfo> selectInfoGroup;
    List<ScenarioPageImageInfo> pageImages;


    readonly List<SelectButton> selectButtons = new();
    ScenarioChapterInfo ChapterInfo => MainStoryManager.ChapterInfo;
    ScenarioWorldInfo WorldInfo => MainStoryManager.WorldInfo;
    List<ScenarioPageInfo> Pages => MainStoryManager.Pages;
       
    int tempMaxVeris;
    
    int Stamina
    {
        get { return data.Stamina; }
        set
        {
            if (value > ChapterInfo.DefaultEnergyMax)
            {
                data.Stamina = ChapterInfo.DefaultEnergyMax;
            }
            else if (value < 0)
            {
                data.Stamina = 0;
            }
            else
            {
                data.Stamina = value;
            }
        }
    }
    
    int Verisimilitude
    {
        get
        {
            if (data.CurMaxVeris <= 0) return 0;
            int value = Mathf.RoundToInt((float)data.CurSelectedVeris / data.CurMaxVeris * 100f);
            return value > 100 ? 100 : value;
        }
    }
    StorySaveData data;

    ObjectPool<UI_StoryShow> storyPool;
    ObjectPool<Image> imagePool;
    private void Start()
    {
        storyPool = new ObjectPool<UI_StoryShow>(new UI_StoryShow[] { storyTextPrefab });
        imagePool = new ObjectPool<Image>(new Image[] { storySDImage });

        GameManager.Save.GameDatas.Datas.ForEach(x =>
        {
            if (x is StorySaveData)
            {
                data = x as StorySaveData;
                
                return;
            }
        });
        if (!data.isLoaded)
        {
            data.Stamina = ChapterInfo.DefaultEnergyMax;
            data.Hp = ChapterInfo.DefaultHealthMax;
        }
        
        worldTitle.text = WorldInfo.Name + ", ";
        chapterTitle.text = ChapterInfo.Name;       
        
        ending.SetActive(false);
        ShowPage();
    }
    private void Update()
    {
        verisimilitude.text = Verisimilitude.ToString() + "%";
        progress.text = Mathf.RoundToInt((float)data.Page / Pages.Count * 100).ToString() + "%";
        staminaBar.fillAmount = Stamina / 100f;
        staminaText.text = Stamina.ToString();
    }

    public readonly List<UI_StoryShow> storyList = new();
    public readonly List<Image> storyImageList = new();
    public void ShowPage()
    {
        consoleText.text = "";
        nextPageButton.enabled = false;
        
        ScenarioData.TryGetPageText(Pages[data.Page].UniqueId, out pageTexts);
        ScenarioData.TryGetPageImages(Pages[data.Page].UniqueId, out pageImages);
        skipStrategy = new SkipTypeWriter(this);
        illustration.gameObject.SetActive(pageImages != null);

        ClearStoryBoard();
        foreach (var x in selectButtons)
        {
            Destroy(x.gameObject);
        }
        selectButtons.Clear();

        if (pageTexts != null)
        {
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
        }

        storyList[0].typeWriter.StartShowingText();

        for (int i = 0; i < storyList.Count - 1; i++)
        {
            int temp = i;
            storyList[i].typeWriter.onTextShowed.AddListener(() => storyList[temp + 1].typeWriter.StartShowingText());
        }
        storyList.Last().typeWriter.onTextShowed.AddListener(ShowSelections);       
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
        if (Stamina <= 0)
        {
            return;
        }       

        ScenarioData.TryGetSelectGroup(Pages[data.Page].SelectGroupId, out selectInfoGroup);

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
            obj.OnClick.AddListener(DisableSelections);           
            selectButtons.Add(obj);
            tempMaxVeris = Mathf.Max(tempMaxVeris, selectInfoGroup[i].SelectVerisimilitude);
        }
    }

    void ClearStoryBoard()
    {
        foreach (var x in storyList)
        {
            x.typeWriter.ShowText("");
            
            storyPool.Return(x);
        }
        foreach (var x in storyImageList)
        {
            imagePool.Return(x);
        }
        storyList.Clear();
        storyImageList.Clear();
    }
    void DisableSelections(SelectButton button)
    {
        foreach(var x in selectButtons)
        {
            x.tmp.fontStyle = FontStyles.Normal;
            x.tmp.color = Color.gray;
            x.DisablePointer();
            x.GetComponent<Button>().enabled = false;
            x.GetComponent<Image>().raycastTarget = false;
        }
        button.tmp.fontStyle = FontStyles.Underline;
        button.tmp.color = Color.white;
        
    }
    public void EnableSelections()
    {
        foreach (var x in selectButtons)
        {
            x.tmp.fontStyle = FontStyles.Normal;
            x.tmp.color = Color.white;
            x.EnablePointer();
            x.GetComponent<Button>().enabled = true;
            x.GetComponent<Image>().raycastTarget = true;
        }
    }
    public void OnSelect(SelectButton select)
    {
        switch (select.info.SelectType)
        {
            case SelectType.None:
                ToNextPage(select.info);
                break;
            case SelectType.Dice:
                if (select.isDiced) return;
                diceWindow.gameObject.SetActive(true);
                diceWindow.Init(20, select, this);
                select.isDiced = true;
                break;
        }
    }
    public void DebugText(string log)
    {
        consoleText.text = log;
    }

    public void ToNextPage(ScenarioSelectInfo info)
    {
        string str = "";
        str += info.ResultText;
        data.Page++;
        data.CurSelectedVeris += info.SelectVerisimilitude;
        data.CurMaxVeris += tempMaxVeris;

        Stamina += info.SelectEnergy;
        
        if (str == "")
        {
            if (data.Page >= Pages.Count - 1)
            {
                ShowEnding();
                return;
            }
            ShowPage();
        }
        else
        {
            var obj = storyPool.Get("Story Show");           
            obj.transform.SetParent(storyParent);
            obj.typeWriter.ShowText(str);
            obj.typeWriter.onMessage.RemoveAllListeners();
            skipStrategy = new SkipResultText(obj);
            obj.typeWriter.onTextShowed.RemoveAllListeners();
            obj.typeWriter.onTextShowed.AddListener(() => skipStrategy = new SkipShowStoryText(this));
            ClearStoryBoard();
            storyList.Add(obj);
            obj.typeWriter.StartShowingText();
        }
        GameManager.Save.SaveAllDatas(SaveManager.SaveType.Story);
    }
    public void ShowEnding()
    {
        ending.SetActive(true);
    }
    public void GameOver()
    {
        gameOverWindow.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneChangeManager.instance.SceneMove(sceneName);
    }
    ISkipStrategy skipStrategy;

    public void Skip()
    {
        skipStrategy.Skip();       
    }

    interface ISkipStrategy
    {
        void Skip();
    }

    class SkipTypeWriter : ISkipStrategy
    {
        readonly List<UI_StoryShow> storyList;
        public void Skip()
        {
            foreach (var x in storyList)
            {
                x.typeWriter.SkipTypewriter();
            }
        }

        public SkipTypeWriter(UI_MainStory main)
        {
            this.storyList = main.storyList;
        }
    }

    class SkipShowStoryText : ISkipStrategy
    {
        readonly UI_MainStory main;
        public SkipShowStoryText(UI_MainStory main)
        {
            this.main = main;
        }
        public void Skip()
        {
            if(main.data.Page >= main.Pages.Count)
            {
                main.ShowEnding();
                return;
            }
            main.ShowPage();
        }
    }
    class SkipResultText : ISkipStrategy
    {
        readonly UI_StoryShow storyShow;
        public SkipResultText(UI_StoryShow storyShow)
        {
            this.storyShow = storyShow;
        }
        public void Skip()
        {
            storyShow.typeWriter.SkipTypewriter();
        }
    }
}
