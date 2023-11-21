using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Data;
using Epos;
using DG.Tweening;
using Febucci.UI.Effects;

public class TileManager : Singleton<TileManager>
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

    AddressablePooling TilePool;

    [SerializeField]
    List<List<Tile>> TileMap;

    Tweener tweener = null;
    public Tweener Tweener => tweener;

    private void Init()
    {
        // Set TileContainer
        if (null == TileContainer)
        {
            TileContainer = GameObject.Find("TileContainer");
        }

        TileMap = new List<List<Tile>>();

        TilePool = new AddressablePooling("Tile");
    }
    private void Start()
    {
        Init();
    }

    public void RemoveTile(int x, int y)
    {
        if (TileMap[x][y] == null)
            return;

        TilePool.Return(TileMap[x][y].gameObject);
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
    }

    private IEnumerator CreateTile(int x, int y)
    {
        if (TileMap[x][y] != null) 
            yield break;

        // Instantiate -> Pooling
        //GameObject TileObject = Instantiate(TilePrefab, TileContainer.transform);
        GameObject TileObject = TilePool.Get("Tile");
        Tile TileComponent = TileObject.GetComponent<Tile>();
        TileComponent.SetScale(TileScale);
        int mid = (int)TileNumber / 2;
        float PosX = (x - mid) * TileInterval;
        float PosY = (y - mid) * TileInterval;
        TileComponent.SetPosition(PosX, PosY);
        TileComponent.SetIndex(x, y);
        TileMap[x][y] = TileComponent;
        yield return null;
    }

    public void CreateATile(int x, int y)
    {
        StartCoroutine(CreateTile(x, y));
    }

    public void PullTiles(int x, int y, Direction direction)
    {
        if (x < 0 || y < 0 || x >= TileMap.Count || TileMap[x].Count <= y)
        {
            return;
        }

        if (TileMap[x][y] != null) return;

        int mid = (int)TileNumber / 2;

        switch (direction)
        {
            case Direction.Left:

                for (int i = y; i > 0; i--)
                {
                    if (TileMap[x][i - 1] != null)
                    {
                        tweener = TileMap[x][i - 1].transform.DOMove(new Vector3((x - mid) * TileInterval, (i - mid) * TileInterval, 0), 0.5f).
                            SetEase(Ease.Linear);

                        if (i == 1) tweener.onComplete += () =>
                        {
                            StartCoroutine(CreateTile(x, 0));
                            tweener = null;
                        };
                    }
                    TileMap[x][i] = TileMap[x][i - 1];
                    TileMap[x][i].SetIndex(x, i);
                    TileMap[x][i - 1] = null;

                }
                if (tweener == null)
                {
                    StartCoroutine(CreateTile(x, 0));
                }
                break;
            case Direction.Right:

                for (int i = y; i < TileMap[x].Count - 1; i++)
                {
                    if (TileMap[x][i + 1] != null)
                    {
                        tweener = TileMap[x][i + 1].transform.DOMove(new Vector3((x - mid) * TileInterval, (i - mid) * TileInterval, 0), 0.5f).
                            SetEase(Ease.Linear);

                        if (i == TileMap[x].Count - 2)
                        {
                            tweener.onComplete += () =>
                            {
                                StartCoroutine(CreateTile(x, TileMap[x].Count - 1));
                                tweener = null;
                            };
                        }
                    }
                    TileMap[x][i] = TileMap[x][i + 1];
                    TileMap[x][i].SetIndex(x, i);
                    TileMap[x][i + 1] = null;
                }
                if (tweener == null)
                {                   
                    StartCoroutine(CreateTile(x, TileMap[x].Count - 1));
                }
                break;
            case Direction.Top:
                for (int i = x; i < TileMap.Count - 1; i++)
                {
                    if (TileMap[i + 1][y] != null)
                    {
                        tweener = TileMap[i + 1][y].transform.DOMove(new Vector3((i - mid) * TileInterval, (y - mid) * TileInterval, 0), 0.5f).
                            SetEase(Ease.Linear);

                        if( i == TileMap.Count - 2)
                        {
                            tweener.onComplete += () =>
                            {
                                StartCoroutine(CreateTile(TileMap.Count - 1, y));
                                tweener = null;
                            };
                        }
                    }
                    TileMap[i][y] = TileMap[i + 1][y];
                    TileMap[i][y].SetIndex(i, y);
                    TileMap[i + 1][y] = null;
                }
                if (tweener == null)
                {
                    StartCoroutine(CreateTile(TileMap.Count - 1, y));
                }
                break;
            case Direction.Bottom:
                for (int i = x; i > 0; i--)
                {
                    if (TileMap[i - 1][y] != null)
                    {
                        tweener = TileMap[i - 1][y].transform.DOMove(new Vector3((i - mid) * TileInterval, (y - mid) * TileInterval, 0), 0.5f).
                            SetEase(Ease.Linear);

                        if( i == 1)
                        {
                            tweener.onComplete += () =>
                            {
                                StartCoroutine(CreateTile(0, y));
                                tweener = null;
                            };
                        }
                    }

                    TileMap[i][y] = TileMap[i - 1][y];
                    TileMap[i][y].SetIndex(i, y);
                    TileMap[i - 1][y] = null;
                }

                if (tweener == null)
                {
                    StartCoroutine(CreateTile(0, y));
                }
                break;
        }
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
