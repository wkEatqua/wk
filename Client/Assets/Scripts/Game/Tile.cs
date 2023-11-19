using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	int x, y;

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

    private void OnMouseDown()
    {
		if (TileManager.Instance.Tweener != null) return;

		Debug.Log(x +" " + y);
		TileManager.Instance.RemoveTile(x, y);
		var enumValues = Enum.GetValues(typeof(Epos.Direction));
		Epos.Direction dir = (Epos.Direction)enumValues.GetValue(UnityEngine.Random.Range(0, enumValues.Length));
		TileManager.Instance.PullTiles(x, y, dir);
    }
}
