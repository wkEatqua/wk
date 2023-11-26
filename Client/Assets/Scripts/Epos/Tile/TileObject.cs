using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Epos
{
    public abstract class TileObject : MonoBehaviour
    {       
        [HideInInspector] public Tile tile;

        public virtual IEnumerator MoveTo(int x,int y)
        {
            var tileMap = TileManager.Instance.TileMap;
            int removePosX = tile.X;
            int removePosY = tile.Y;
            
            yield return transform.DOMove(tileMap[x][y].transform.position, 0.5f).WaitForCompletion();
            if (tileMap[x][y].Selector.Obj is ItemObject item)
            {
                item.Collect();
            }
            tileMap[x][y].SetObject(this);
            tileMap[removePosX][removePosY].SetObject(null);

            
            yield return new WaitForEndOfFrame();

        }

        public void Die()
        {
            transform.DOMoveZ(transform.position.z + 2, 0.5f).OnComplete(() => { Destroy(gameObject); tile.SetObject(null); });
        }
    }
}