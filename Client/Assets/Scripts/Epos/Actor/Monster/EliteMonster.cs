using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Epos
{
    public class EliteMonster : Monster
    {
        public enum MoveType
        {
            Random, ToClosest
        }

        public MoveType moveType;
        IMoveStrategy moveStrategy;

        public override void Start()
        {
            base.Start();

            switch (moveType)
            {
                case MoveType.Random:
                    moveStrategy = new MoveRandomly();
                    break;
                case MoveType.ToClosest:
                    moveStrategy = new MoveToClosest();
                    break;
            }
        }
        public interface IMoveStrategy
        {
            IEnumerator Move(Actor obj);
        }


        public class MoveToClosest : IMoveStrategy
        {
            public IEnumerator Move(Actor obj)
            {
                Dictionary<Tile, bool> visited = new();
                TileManager.Instance.Traverse(x => visited.Add(x, false));
                Player player = FindAnyObjectByType<Player>();

                (int x, int y) endPos = (player.tile.X, player.tile.Y);

                (int x, int y)[] dirs = new (int x, int y)[]
                {
                (1,0),(0,1),(-1,0),(0,-1)
                };

                Queue<Tile> queue = new();
                queue.Enqueue(obj.tile);
                var tileMap = TileManager.Instance.TileMap;

                List<List<(int x, int y)>> parents = new();

                foreach (var tiles in tileMap)
                {
                    parents.Add(tiles.Select(tile => (-1, -1)).ToList());
                }

                visited[obj.tile] = true;
                parents[obj.tile.X][obj.tile.Y] = (obj.tile.X, obj.tile.Y);
                while (queue.Count > 0)
                {
                    Tile curTile = queue.Dequeue();

                    for (int i = 0; i < dirs.Length; i++)
                    {
                        int nextX = curTile.X + dirs[i].x;
                        int nextY = curTile.Y + dirs[i].y;


                        if (nextX < 0 || nextY < 0 || nextX >= tileMap.Count || nextY >= tileMap[nextX].Count)
                        {
                            continue;
                        }

                        Tile nextTile = tileMap[nextX][nextY];

                        if (nextTile.Selector.Obj != null)
                        {
                            if (nextTile.Selector.Obj is not Player)
                                continue;
                        }

                        if (nextTile.Type == Tile.TileType.Grace) continue;

                        if (visited[nextTile]) continue;

                        queue.Enqueue(nextTile);
                        visited[nextTile] = true;
                        parents[nextX][nextY] = (curTile.X, curTile.Y);
                    }
                }

                int x = endPos.x;
                int y = endPos.y;

                List<(int x, int y)> list = new();

                while (parents[x][y].y != y || parents[x][y].x != x)
                {
                    list.Add((x, y));
                    (int x, int y) pos = parents[x][y];
                    y = pos.y;
                    x = pos.x;
                }

                list.Reverse();
                list.RemoveAt(list.Count - 1);

                for (int i = 0; i < obj.MoveSpeed && i < list.Count; i++)
                {
                    (int x, int y) pos = list[i];
                    int removePosX = obj.tile.X;
                    int removePosY = obj.tile.Y;
                    yield return obj.MoveTo(pos.x, pos.y);
                                     
                    TileManager.Instance.RemoveTile(removePosX, removePosY);
                    List<Direction> directions = new() { Direction.Left, Direction.Right, Direction.Up, Direction.Down };
                    directions = directions.Where(dir => !TileManager.Instance.Check(dir, removePosX, removePosY,
                        tile =>
                        {                           
                            return tile.Selector.Obj != null && (tile.Selector.Obj == obj || tile.Selector.Obj is Player);
                        })).ToList();

                    yield return obj.StartCoroutine(TileManager.Instance.PullTiles(removePosX, removePosY,
                        directions[UnityEngine.Random.Range(0, directions.Count)]));
                }

            }
        }

        public class MoveRandomly : IMoveStrategy
        {
            public IEnumerator Move(Actor obj)
            {
                for (int i = 0; i < obj.MoveSpeed; i++)
                {
                    var tileMap = TileManager.Instance.TileMap;

                    int x = obj.tile.X;
                    int y = obj.tile.Y;

                    List<(int x, int y)> list = new()
                {
                    (x + 1, y),
                    (x - 1, y),
                    (x, y + 1),
                    (x, y - 1)
                };

                    list = list.Where(pos => TileManager.Instance.CheckIndex(pos.x, pos.y) && tileMap[pos.x][pos.y].Selector.Obj == null
                    && tileMap[pos.x][pos.y].Type != Tile.TileType.Grace).ToList();
                   
                    if (list.Count == 0) yield break;

                    (int x, int y) pos = list[UnityEngine.Random.Range(0, list.Count)];
                    int removePosX = obj.tile.X;
                    int removePosY = obj.tile.Y;

                    yield return obj.MoveTo(pos.x, pos.y);
                    
                    TileManager.Instance.RemoveTile(removePosX, removePosY);

                    List<Direction> directions = new() { Direction.Left,Direction.Right,Direction.Up,Direction.Down };
                    directions = directions.Where(dir => !TileManager.Instance.Check(dir, removePosX, removePosY,
                        tile =>
                        {
                            return tile.Selector.Obj != null && (tile.Selector.Obj == obj || tile.Selector.Obj is Player);
                        })).ToList();
                    
                    yield return obj.StartCoroutine(TileManager.Instance.PullTiles(removePosX, removePosY, 
                        directions[UnityEngine.Random.Range(0, directions.Count)]));
                }
            }
        }
        protected override IEnumerator UseTurn()
        {
            yield return moveStrategy.Move(this);
        }
    }
}