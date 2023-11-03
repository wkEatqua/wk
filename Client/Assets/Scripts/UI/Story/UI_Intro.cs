using Febucci.UI;
using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Intro : MonoBehaviour
{
    public TextMeshProUGUI stageTitle;
    public TextMeshProUGUI worldTitle;
    public TextMeshProUGUI classText;
    public TextMeshProUGUI nameText;

    public Button button;

    public List<TypewriterByCharacter> typeWriters = new();
    public ImageFade imageFade;

    public GameObject mainStoryCanvas;

    ScenarioIntroInfo info;

    public Image bg;
    void Start()
    {
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() =>
        {
            imageFade.SkipFadeIn();

            foreach (var typewriter in typeWriters)
            {
                typewriter.SkipTypewriter();
            }
        });
        
        ScenarioData.TryGetIntro(MainStoryManager.ChapterInfo.UniqueId, out info);

        bg.rectTransform.anchoredPosition = new Vector2(info.BackGroundPosX, info.BackGroundPosY);
        bg.color = new Color(info.BackGroundColorR,info.BackGroundColorG,info.BackGroundColorB,info.BackGroundColorA);

        stageTitle.text = MainStoryManager.ChapterInfo.Name;
        worldTitle.text = MainStoryManager.WorldInfo.Name;
        classText.text = info.CharacterClass;
        nameText.text = info.CharacterName;

    }

    public void ChangeButton()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => gameObject.SetActive(false));
        button.onClick.AddListener(() => mainStoryCanvas.SetActive(true));
    }


}
