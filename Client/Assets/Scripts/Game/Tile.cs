using Epos;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Model;
using TMPro;
using Shared.Data;
using System.Text.RegularExpressions;

public class Tile : MonoBehaviour
{
	[SerializeField] int x, y, tier;   
    public int X => x;
    public int Y => y;

    public int Tier
    {
        get
        {
            return tier;
        }
        set
        {
            tier = value;
            if (TileLevelText != null)
            {
                string text;
                if (tier != 0)
                    text = tier.ToString();
                else
                    text = Type.ToString()[0].ToString();

                TileLevelText.text = text;
            }
        }
    }

    TileSelector selector;
    public TileSelector Selector => selector;
    public enum TileType
    {
        Tier,        
        BlankTile,
        EnvironmentTile,
        InteractionTile,
        Grace,
    }

    EposTileInfoInfo TileInfo;

    public TileType Type;
    public TileDifficulty Difficulty = TileDifficulty.None;

    // Debug
    [SerializeField]
    TextMeshPro TileLevelText;

    private void Awake()
    {
        selector = GetComponent<TileSelector>();
    }

    private void OnEnable()
    {
        Type = TileType.Tier;
    }
  
    public void SetScale(float Scale)
	{
		transform.localScale = new Vector3(Scale, 0.1f, Scale);
	}

	public void SetPosition(float x, float y)
	{
		transform.localPosition = new Vector3(x, y, 0);
	}
	public void SetIndex(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

    public void SetTileInfo(EposTileInfoInfo Info)
    {
        this.TileInfo = Info;
        SetTier(Info.Tier);

        var TileType = (TileType)Enum.Parse(typeof(TileType), Info.Tile);
        SetTileType(TileType);
    }

    public void SetTileType(TileType type)
    {
        this.Type = type;
    }

    public void SetTier(int tier)
    {
        this.Tier = tier;
    }

    public void SetObject(TileObject obj)
    {
        selector.SetObject(obj);
    }
    private void OnDisable()
    {
        SetTileType(TileType.Tier);
    }
}
