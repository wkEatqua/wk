using Epos;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField] int x, y;   
    public int X => x;
    public int Y => y;

    TileSelector selector;
    public TileSelector Selector => selector;
   
    public enum TileType
    {
        Normal,Grace,UnMovable
    }
    public enum TileDifficulty
    {
        Empty, Easy, Normal, Danger, Hard, Nightmare, Disaster
    }
    public TileType Type;
    public TileDifficulty Difficulty;

    private void Awake()
    {
        selector = GetComponent<TileSelector>();
    }

    private void OnEnable()
    {
        Type = TileType.Normal;
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

    public void SetObject(TileObject obj)
    {
        selector.SetObject(obj);
    }     
}
