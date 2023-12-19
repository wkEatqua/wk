using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Data;
using Epos;
using DG.Tweening;
using Apis;
using System;
using System.Linq;
using Shared.Model;
using System.Text.RegularExpressions;
using System.Text;

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

    // TilePool
    AddressablePooling TilePool;

    // ObjectPool
    AddressablePooling ObjectPool;

    public List<List<Tile>> TileMap;

    // Create Tile, Object percentage
    private List<Tuple<long, long>> TileRateList = new List<Tuple<long, long>>();

    private float MaxTileRates = 0;

    Tweener tweener = null;

    public readonly Queue<(int x, int y)> graceTiles = new();
    public (int x, int y) playerLastPos = (-1, -1);

    private Player player;
    public int playerX = 0;
    public int playerY = 0;

    private void Init()
    {
        // Set TileContainer
        if (null == TileContainer)
        {
            TileContainer = GameObject.Find("TileContainer");
        }

        TileMap = new List<List<Tile>>();

        TilePool = new AddressablePooling("Tile");

        ObjectPool = new AddressablePooling("Object");
    }
    private void Start()
    {
        Init();

        TurnManager.Instance.OnPlayerTurnStart += MakeInjectedSelectable;
        TurnManager.Instance.OnPlayerTurnEnd += ResetTiles;
        TurnManager.Instance.OnPlayerTurnEnd += CheckSight;
        TurnManager.Instance.OnEnemyTurnStart += RemoveAndCreateGrace;
        TurnManager.Instance.OnEnemyTurnEnd += CheckSight;
    }

    public IEnumerator ResetTiles()
    {
        Traverse(x => x.Selector.selectable = false);
        yield return null;
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

    private IEnumerator CheckSight()
    {
        int SightRange = player.Sight / 10;
        int SubSightRange = player.Sight % 10;
        Debug.Log($"Player Sight : {player.Sight}");
        bool CheckSemiOpen = false;
        if (SubSightRange >= 5)
        {
            CheckSemiOpen = true;
            SightRange += 1;
        }
        else
        {
            CheckSemiOpen = false;
        }

        int maxX = playerX + SightRange;
        int maxY = playerY + SightRange;
        int minX = playerX - SightRange;
        int minY = playerY - SightRange;

        for (int x = 0; x < TileMap.Count; x++)
        {
            for (int y = 0; y < TileMap[x].Count; y++)
            {
                if (playerX == x && playerY == y)
                {
                    TileMap[x][y].SetState(TileState.Open);
                }

                if (x >= minX && x <= maxX && y >= minY && y <= maxY)
                {
                    if (CheckIndex(x, y) == false)
                        continue;

                    if (CheckSemiOpen == true && (x == minX || x == maxX || y == minY || y == maxY))
                    {
                        TileMap[x][y].SetState(TileState.SemiOpen);
                    }
                    else
                    {
                        TileMap[x][y].SetState(TileState.Open);
                    }
                        
                }
                else
                {
                    TileMap[x][y].SetState(TileState.Close);
                }
                    
            }
        }

        yield return null;
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

    private void LoadTilePercents()
    {
        int PlayerLevel = EposManager.Instance.level;
        if (PlayerLevel <= 0)
            PlayerLevel = 1;


        bool success = EposData.TryGetEposTile(PlayerLevel, out Shared.Data.EposTileInfo TileInfo);
        if (success == false)
            return;

        var fields = typeof(Shared.Data.EposTileInfo).GetProperties(System.Reflection.BindingFlags.Public |
                                                                System.Reflection.BindingFlags.NonPublic |
                                                                System.Reflection.BindingFlags.Instance);

        Dictionary<long, long> TempDict = new Dictionary<long, long>();
        long key = 0;
        string keyName = "";
        foreach (var field in fields)
        {
            string name = field.Name;
            if (!name.Contains("Tile"))
                continue;

            if (!name.Contains("Rate"))
            {
                string numstr = Regex.Replace(name, @"\D", "");
                StringBuilder sb = new StringBuilder();
                sb.Append(Regex.Replace(name, @"\d", ""));
                sb.Append("Rate");
                sb.Append(numstr);
                keyName = sb.ToString();
                key = (long)field.GetValue(TileInfo);
                TempDict.Add(key, 0);
            }
            else
            {
                if (field.Name == keyName)
                {
                    long value = (long)field.GetValue(TileInfo);
                    TempDict[key] = value;
                }
            }
        }

        foreach (var data in TempDict)
        {
            TileRateList.Add(new Tuple<long, long>(data.Key, data.Value));
            MaxTileRates += data.Value;
        }
    }

    private EposTileInfoInfo GetTileInfo()
    {
        float rand = UnityEngine.Random.Range(0, MaxTileRates);

        float sum = 0;
        long result = 0;
        foreach (var percent in TileRateList)
        {
            sum += percent.Item2;
            if (sum >= rand)
            {
                result = percent.Item1;
                break;
            }
        }

        EposData.TryGetEposTileInfo(result, out EposTileInfoInfo Info);
        return Info;
    }

    public IEnumerator CreateTile(int x, int y)
    {
        if (TileMap[x][y] != null)
            yield break;

        // Create Tile and Set TileInfo
        GameObject TilePrefab = TilePool.Get("Assets/Prefabs/Debug/Tile.prefab");
        Tile TileComponent = TilePrefab.GetComponent<Tile>();
        var TileInfo = GetTileInfo();
        TileComponent.SetTileInfo(TileInfo);
        TileComponent.SetScale(TileScale);
        TileComponent.SetState(TileState.Close);

        // Create Object on Tile
        var TempObject = ObjectPool.Get("DebugObj");
        TileObject TileObject = ObjectFactory.CreateObject(TileInfo, TempObject);
        if (TileObject == null)
            ObjectPool.Return(TempObject);
        else
            TileComponent.SetObject(TileObject);

        // Set TilePosition
        int mid = (int)TileNumber / 2;
        float PosX = (x - mid) * TileInterval;
        float PosY = (y - mid) * TileInterval;
        TileComponent.SetPosition(PosX, PosY);
        TileComponent.SetIndex(x, y);

        // Keep TileComponent in memory
        TileMap[x][y] = TileComponent;
        TilePrefab.transform.SetParent(TileContainer.transform);
        yield return null;
    }
    public bool CheckIndex(int x, int y)
    {
        if (x < 0 || y < 0 || x >= TileMap.Count || TileMap[x].Count <= y)
        {
            return false;
        }

        return true;
    }
    public IEnumerator PullTiles(int x, int y, Direction direction)
    {
        if (x < 0 || y < 0 || x >= TileMap.Count || TileMap[x].Count <= y)
        {
            yield break;
        }

        if (TileMap[x][y] != null) yield break;

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
            case Direction.Up:
                for (int i = x; i < TileMap.Count - 1; i++)
                {
                    if (TileMap[i + 1][y] != null)
                    {
                        tweener = TileMap[i + 1][y].transform.DOMove(new Vector3((i - mid) * TileInterval, (y - mid) * TileInterval, 0), 0.5f).
                            SetEase(Ease.Linear);

                        if (i == TileMap.Count - 2)
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
            case Direction.Down:
                for (int i = x; i > 0; i--)
                {
                    if (TileMap[i - 1][y] != null)
                    {
                        tweener = TileMap[i - 1][y].transform.DOMove(new Vector3((i - mid) * TileInterval, (y - mid) * TileInterval, 0), 0.5f).
                            SetEase(Ease.Linear);

                        if (i == 1)
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

        if (tweener != null) 
            yield return tweener.WaitForCompletion();
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
        int mid = (int)TileNumber / 2;

        if (player != null)
        {
            ResourceUtil.Destroy(player.gameObject);
            player = null;
        }

        player = ResourceUtil.Instantiate("Player").GetComponent<Player>();
        TileMap[mid][mid].SetObject(player);
        SetPlayerPos(mid, mid);
    }

    public void SetPlayerPos(int x, int y)
    {
        playerX = x;
        playerY = y;
    }

    public delegate bool CheckHanlder(Tile tile);
    public bool Check(Direction dir, int x, int y, CheckHanlder handler)
    {
        if (!CheckIndex(x, y)) return false;

        switch (dir)
        {
            case Direction.Left:

                if (y - 1 < 0) return true;
                for (int i = y - 1; i >= 0; i--)
                {
                    if (handler(TileMap[x][i])) return true;
                }

                break;
            case Direction.Right:

                if (y + 1 >= TileMap[x].Count) return true;
                for (int i = y + 1; i < TileMap[x].Count; i++)
                {
                    if (handler(TileMap[x][i])) return true;
                }
                break;
            case Direction.Up:
                if (x + 1 >= TileMap.Count) return true;
                for (int i = x + 1; i < TileMap.Count; i++)
                {
                    if (handler(TileMap[i][y])) return true;
                }

                break;
            case Direction.Down:
                if (x - 1 < 0) return true;
                for (int i = x - 1; i >= 0; i--)
                {
                    if (handler(TileMap[i][y])) return true;
                }
                break;
        }

        return false;
    }
    public void OnBtnClickLevelButton(int Level)
    {
        this.Level = Level;
        RemoveAllTile();
        LoadLevel();
        LoadTilePercents();
        GetTileInfo();
        CreateAllTile();
        CheckSight();
        // To do - Player 생성 따로 뺄 것
        TurnManager.Instance.StartTurn();
    }

    void MakeSelectable(int x, int y)
    {
        if (x >= 0 && x < TileMap.Count && y >= 0 && y < TileMap.Count)
        {
            TileMap[x][y].Selector.selectable = true;
        }
    }
    public IEnumerator MakeInjectedSelectable()
    {
        Player player = FindObjectOfType<Player>();

        if (player != null)
        {
            int x = player.tile.X;
            int y = player.tile.Y;

            MakeSelectable(x + 1, y);
            MakeSelectable(x - 1, y);
            MakeSelectable(x, y + 1);
            MakeSelectable(x, y - 1);
        }

        yield return null;
    }

    public void Traverse(Action<Tile> action)
    {
        foreach (var x in TileMap)
        {
            foreach (var y in x)
            {
                action(y);
            }
        }
    }

    public IEnumerator RemoveAndCreateGrace()
    {
        Queue<Tile> queue = new();

        Traverse(x =>
        {
            if (x.Type == Tile.TileType.Grace) queue.Enqueue(x);
        });

        while (queue.Count > 0)
        {
            Tile grace = queue.Dequeue();
            int x = grace.X;
            int y = grace.Y;

            List<Direction> directions = new()
                {
                    Direction.Left,Direction.Right,Direction.Up,Direction.Down
                };

            directions = directions.Where(dir => !Check(dir, x, y, (tile) =>
            {
                if (tile.Selector.Obj != null && tile.Selector.Obj is Player)
                {
                    return true;
                }
                if (playerLastPos.x == tile.X && playerLastPos.y == tile.Y)
                {
                    return true;
                }

                return false;
            })).ToList();

            RemoveTile(x, y);
            if (directions.Count == 0)
            {
                yield return StartCoroutine(CreateTile(x, y));
            }
            else
            {
                yield return PullTiles(x, y, directions[UnityEngine.Random.Range(0, directions.Count)]);
            }
        }

        while (graceTiles.Count > 0)
        {
            (int x, int y) = graceTiles.Dequeue();

            if (CheckIndex(x, y))
            {
                TileMap[x][y].Type = Tile.TileType.Grace;
            }
        }
    }

    public void Return(GameObject obj)
    {
        ObjectPool.Return(obj);
    }
    public void Return(Tile tile)
    {
        TilePool.Return(tile.gameObject);
    }
}
