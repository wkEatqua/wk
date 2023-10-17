using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Shared.Data;
using UnityEngine.UI;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using Shared.Model;

public class UI_MainStory : MonoBehaviour
{
    [Header("프리팹")]
    public SelectButton selectButton;

    [Header("UI")]
    public TextMeshProUGUI story;
    public GameObject selectArea;
    public TextMeshProUGUI worldTitle;
    public TextMeshProUGUI chapterTitle;
    public Image illustration;
    public TypewriterCore typewriter;
    public TextMeshProUGUI progress;
    public TextMeshProUGUI verisimilitude;
    public Image staminaBar;
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI consoleText;
    public DiceWindow diceWindow;
   
    List<ScenarioPageTextInfo> pageTexts;
    List<ScenarioSelectInfo> selectInfoGroup;
    List<ScenarioPageImageInfo> pageImages;

    MainStoryManager Manager => MainStoryManager.Instance;

    readonly List<SelectButton> selectButtons = new();
    ScenarioChapterInfo ChapterInfo => Manager.ChapterInfo;
    ScenarioWorldInfo WorldInfo => Manager.WorldInfo;
    List<ScenarioPageInfo> Pages => Manager.Pages;
    int page;

    int curSelectedVeris;
    int curMaxVeris;
    int tempMaxVeris;

    int stamina;

    int Stamina { get { return stamina; }
        set 
        {
            if (value > ChapterInfo.DefaultEnergyMax)
            {
                stamina = ChapterInfo.DefaultEnergyMax;
            }
            else if(value < 0)
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
    
    private void Start()
    {       
        page = 0;
        worldTitle.text = WorldInfo.Name + ", ";
        chapterTitle.text = ChapterInfo.Name;
        typewriter.onMessage.AddListener(ShowImage);
        typewriter.onMessage.AddListener(EnergyDown);
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
    public void SetStoryText(int fontSize, int lineSpacing)
    {
        if (!PlayerPrefs.HasKey("FONTSIZE"))
        {
            PlayerPrefs.SetInt("FONTSIZE", fontSize);
        }

        if (!PlayerPrefs.HasKey("HANG"))
        {
            PlayerPrefs.SetInt("HANG", lineSpacing);
        }

        story.fontSize = PlayerPrefs.GetInt("FONTSIZE");
        story.lineSpacing = PlayerPrefs.GetInt("HANG");
    }
    void ShowPage()
    {
        consoleText.text = "";
        story.text = "";
        ScenarioData.TryGetPageText(Pages[page].UniqueId, out pageTexts);
        ScenarioData.TryGetPageImages(Pages[page].UniqueId, out pageImages);

        illustration.gameObject.SetActive(pageImages != null);
        
        for (int i = 0; i < pageTexts.Count; i++)
        {
            if (pageImages != null)
            {
                foreach (var x in pageImages)
                {
                    if (x.ImageActiveOrder == i)
                    {
                        story.text += $"<?ShowImage={x.ImagePath}>";
                        break;
                    }
                }
            }
            story.text += "<?EnergyDown>";
            story.text += pageTexts[i].Text.Replace("\\n", "\n");
            story.text += "\n";
        }

        foreach (var x in selectButtons)
        {
            Destroy(x.gameObject);
        }
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
    }
    public void ShowSelections()
    {
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
            Debug("마지막 페이지입니다.");
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
    public void Debug(string log)
    {
        consoleText.text = log;
    }
   
    public void ToNextPage(ScenarioSelectInfo info)
    {       
        page++;
        curSelectedVeris += info.SelectVerisimilitude;
        curMaxVeris += tempMaxVeris;

        Stamina += info.SelectEnergy;
        ShowPage();
    }
}
