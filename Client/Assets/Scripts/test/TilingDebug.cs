using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Data;
using TMPro;

public class TilingDebug : MonoBehaviour
{
    [SerializeField]
    TMP_InputField InputField;

    public void OnClickPlayerLevelButton()
    {
        int PlayerLevel = int.Parse(InputField.text);
        TileManager.Instance.GetTilePercent(PlayerLevel);
        TileManager.Instance.ShowPlayerLevelButton();
    }

    public void OnClickCheckInfoButton()
    {
        EposObjectPercentInfo objectInfo;
        EposData.TryGetEposObjectPercent(1, out objectInfo);
        Debug.Log(objectInfo.ToString());

        EposTileObjectPercentInfo tileObjectInfo;
        EposData.TryGetEposTileObjectPercent(6, out tileObjectInfo);
        Debug.Log(tileObjectInfo.ToString());
    }
}
