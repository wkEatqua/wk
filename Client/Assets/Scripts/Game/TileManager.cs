using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Data;

public class TileManager : MonoBehaviour
{
	[SerializeField]
	private long Level;

    [SerializeField]
    private long TileNumber;

	[SerializeField]
	private float TileInterval;

	[SerializeField]
	private float TileScale;

    [SerializeField]
    private GameObject TileContainer;

	[SerializeField]
	private GameObject TilePrefab;

	[SerializeField]
    List<List<Tile>> TileMap;

	private void Init()
	{
		// Set TileContainer
		if (null == TileContainer)
		{
			TileContainer = GameObject.Find("TileContainer");
		}

		TileMap = new List<List<Tile>>();
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
		TileMap = null;
	}

	private void LoadLevel()
	{
		Debug.Log($"Level : {this.Level}");

		// Load TileNumber and TileInterver
		EposData.TryGetEposLevel(Level, out EposLevelInfo info);
		if (null == info)
		{
			Debug.Log($"info is null");
			TileNumber = 5;
			TileInterval = 1.1f;
			TileScale = 0.9f;
			return;
		}
		TileNumber = info.TileNumber;
		TileInterval = info.TileInterval;
		TileScale = info.TileScale;

		// Debug Code 시작 (삭제할 것)
		//switch(Level)
		//{
		//	case 1:
		//		TileNumber = 7;
		//		TileInterval = 20;
		//		TileScale = 1;
		//		break;
		//	case 2:
		//		TileNumber = 11;
		//		TileInterval = 10;
		//		TileScale = 0.9f;
		//		break;
		//	case 3:
		//		TileNumber = 15;
		//		TileInterval = 5;
		//		TileScale = 0.8f;
		//		break;
		//}
		// DebugCode 끝
		
		
	}

	private IEnumerator CreateTile(int x, int y)
	{
		GameObject TileObject = Instantiate(TilePrefab, TileContainer.transform);
		Tile TileComponent = TileObject.GetComponent<Tile>();
		TileComponent.SetScale(TileScale);
		int mid = (int)TileNumber / 2;
		float PosX = (x - mid) * TileInterval;
		float PosY = (y - mid) * TileInterval;
		TileComponent.SetPosition(PosX, PosY);
		TileMap[x][y] = TileComponent;
		yield return null;
	}

	private void CreateAllTile()
	{
		TileMap = new List<List<Tile>>();
		for (int i = 0; i < TileNumber; i++)
		{
			TileMap.Add(new List<Tile>());
		}

		for (int i = 0; i < TileNumber; i++)
		{
			for (int j = 0; j < TileNumber; j++)
			{
				TileMap[i].Add(null);
				StartCoroutine(CreateTile(i, j));
			}
		}
	}
	public void OnBtnClickLevelButton(int Level)
	{
		this.Level = Level;
		RemoveAllTile();
		LoadLevel();
		CreateAllTile();
	}
}
