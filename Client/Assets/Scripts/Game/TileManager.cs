using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
	[SerializeField]
	private long Level;

    [SerializeField]
    private long TileNumber;

	[SerializeField]
	private long TileInterval;

    [SerializeField]
    private GameObject TileContainer;

	[SerializeField]
    List<List<Tile>> TileMap;

	private void Init()
	{
		// Set TileContainer
		if (null == TileContainer)
		{
			TileContainer = GameObject.Find("TileContainer");
		}

		
	}
	private void Start()
	{
		Init();
	}

	private void RemoveTile(int x, int y)
	{
		if (TileMap[x][y] == null)
			return;

		Destroy(TileMap[x][y].gameObject);
		TileMap[x][y] = null;
	}

	private void RemoveAllTile()
	{
		for (int i = 0; i < TileMap.Count; i++)
		{
			for (int j = 0; j < TileMap[i].Count; j++)
			{
				RemoveTile(i, j);
			}
		}
	}

	private IEnumerator LoadLevel()
	{
		Debug.Log($"Level : {this.Level}");
		yield return null;
	}

	public void OnBtnClickLevelButton(int Level)
	{
		this.Level = Level;
		RemoveAllTile();
		StartCoroutine(LoadLevel());
	}
}
