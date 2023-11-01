using Febucci.UI;
using NPOI.HPSF;
using Shared.Data;
using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;

public class UI_StoryShow : MonoBehaviour
{
    [HideInInspector] public TypewriterByCharacter typeWriter;
    [HideInInspector] public TextMeshProUGUI tmp;

    UI_MainStory main;

    private void Awake()
    {
        typeWriter = GetComponent<TypewriterByCharacter>();
        tmp = GetComponent<TextMeshProUGUI>();
        typeWriter.onMessage.AddListener(ShowImage);
        typeWriter.onMessage.AddListener(EnergyDown);
    }

    private void OnDisable()
    {
        typeWriter.onTextShowed.RemoveAllListeners();
    }
    public void Init(UI_MainStory main, ScenarioPageTextInfo textInfo, ScenarioPageImageInfo imageInfo)
    {       
        this.main = main;
        if (imageInfo != null)
        {
            tmp.text += $"<?ShowImage={imageInfo.ImagePath}>";
        }
        tmp.text += "<?EnergyDown>";
        tmp.text += textInfo.Text.Replace("\\n", "\n");

        typeWriter.ShowText(tmp.text);
    }

    void ShowImage(EventMarker eventMarker)
    {
        main.ShowImage(eventMarker);
    }

    public void EnergyDown(EventMarker eventMarker)
    {
        main.EnergyDown(eventMarker);
    }
}
