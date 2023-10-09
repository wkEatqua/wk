using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SettingPopupManager : MonoBehaviour
{
    public Slider SliderBGSound;
    public Slider SliderEFSound;

    public TextMeshProUGUI FontSize;
    public TextMeshProUGUI RatioSize;
    public TextMeshProUGUI hangSize;


    string[] RatioSizeValue = { "1920 x 1080" };
    int ratioCount = 2;

    string[] FontSizeValue = { "매우작게","작게","보통","크게","매우크게" };
    int[] fontSize = {25, 30, 35, 40, 45};
    int fontCount = 0;
    int hangCount = 10;


    public void OnClickBtnRatio(int value)
    {
        if (ratioCount + value >= 0 && ratioCount + value < RatioSizeValue.Length)
        {
            RatioSize.text = RatioSizeValue[ratioCount + value];

            ratioCount += value;
            PlayerPrefs.GetInt("RATIO", ratioCount);
        }
    }

    public void OnClickBtnFonts(int value)
    {
        if (fontCount + value >= 0 && fontCount + value < FontSizeValue.Length)
        {
            FontSize.text = FontSizeValue[fontCount + value];

            fontCount += value;

            PlayerPrefs.GetInt("FONTSIZE", fontSize[fontCount]);
        } 
    }

    public void OnClickBtnHang(int value)
    {
        hangCount += value;

        hangSize.text = hangCount.ToString();


        PlayerPrefs.GetInt("HANG", hangCount);
    }


}
