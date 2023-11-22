using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Epos
{
    public class TileSelector : MonoBehaviour
    {
        [HideInInspector] public UnityEvent OnSelected = new();
        [HideInInspector] public UnityEvent OnConfirmed = new();
        Tile tile;
        TileObject obj;
        public TileObject Obj => obj;
        Renderer render;
        public bool selectable;
        private void Awake()
        {
            tile = GetComponent<Tile>();
            selectable = false;      
            render = GetComponent<Renderer>();
            OnConfirmed.AddListener(() => TileManager.Instance.Traverse(x => x.Selector.selectable = false));
        }

        private void OnMouseDown()
        {
            if (selectable)
            {
                OnSelected.Invoke();
            }
        }

        private void OnDisable()
        {
            if(obj != null)
            {
                Destroy(obj.gameObject);
            }
        }

        public void SetObject(TileObject obj)
        {
            this.obj = obj;
            obj.transform.position = transform.position;
            obj.transform.SetParent(transform);
            obj.tile = tile;
        }

        private void Update()
        {
            switch(tile.Type)
            {
                case Tile.TileType.Normal:
                    if (selectable)
                    {
                        render.material.color = Color.green;
                    }
                    else
                    {
                        render.material.color = Color.white;
                    }
                    break;
                case Tile.TileType.Grace:
                    render.material.color = Color.black;
                    selectable = false;
                    break;
            }
        }       
    }
}