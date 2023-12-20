using DG.Tweening;
using System.Collections;
using Shared.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Epos
{
    public abstract class TileObject : BuffEvent
    {       
        [HideInInspector] public Tile tile;

        protected EposObjectInfo objectInfo;

        public void DoRange(int range,Action<Tile> action)
        {
            for(int i = tile.X - range; i <= tile.X + range; i++)
            {
                for(int j = tile.Y - range; j <= tile.Y + range; j++)
                {
                    if(TileManager.Instance.CheckIndex(i, j))
                    {
                        action(TileManager.Instance.TileMap[i][j]);
                    }
                }
            }
        }
        public void SetObjectInfo(EposObjectInfo Info)
        {
            objectInfo = Info;
        }

        public virtual IEnumerator MoveTo(int x,int y)
        {
            var tileMap = TileManager.Instance.TileMap;
            int removePosX = tile.X;
            int removePosY = tile.Y;
            
            yield return transform.DOMove(tileMap[x][y].transform.position, 0.5f).WaitForCompletion();
            
            tileMap[x][y].SetObject(this);
            tileMap[removePosX][removePosY].SetObject(null);
            
            yield return new WaitForEndOfFrame();

        }

        public void Die()
        {
            transform.DOMoveZ(transform.position.z + 2, 0.5f).OnComplete(() => { tile.SetObject(null); TileManager.Instance.Return(gameObject);  });
        }

        public virtual void Collect()
        {

        }
    }
}