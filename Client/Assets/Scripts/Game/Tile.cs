using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public void SetScale(float Scale)
	{
		transform.localScale = new Vector3(Scale, 0.1f, Scale);
	}

	public void SetPosition(float x, float y)
	{
		transform.localPosition = new Vector3(x, y, 0);
	}
}
