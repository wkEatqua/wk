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
        ICommand command;

        private void Awake()
        {
            tile = GetComponent<Tile>();
            selectable = false;
            render = GetComponent<Renderer>();
            OnSelected.AddListener(() =>
            {
                command = CommandFactory.CreateCommand(tile, obj);

                if (command != null)
                {
                    StartCoroutine(command.Excute());
                }
            });
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
            if (obj != null)
            {
                Destroy(obj.gameObject);
            }
        }

        public void SetObject(TileObject obj)
        {
            this.obj = obj;
            if (obj == null) return;

            obj.transform.position = transform.position;
            obj.transform.SetParent(transform);
            obj.tile = tile;
        }

        private void Update()
        {
            switch (tile.Type)
            {
                case Tile.TileType.Tier:
                case Tile.TileType.Unmovable:
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