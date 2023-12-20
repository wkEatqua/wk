using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Epos
{
    public class TileSelector : MonoBehaviour,IRaycast
    {
        [HideInInspector] public UnityEvent OnSelected = new();
        [HideInInspector] public UnityEvent OnConfirmed = new();
        Tile tile;
        TileObject obj;
        public TileObject Obj => obj;
        Renderer render;
        public bool selectable;
        ICommand command;

        public UnityEvent OnClicked = new();

        private void Awake()
        {
            tile = GetComponent<Tile>();
            selectable = false;
            render = GetComponent<Renderer>();
            OnClicked.AddListener(() =>
            {
                if (selectable)
                {
                    OnSelected.Invoke();
                }
            });
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

        private void OnDisable()
        {
            if (obj != null)
            {
                Destroy(obj.gameObject);
                obj = null;
            }
            selectable = false;
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
                case Tile.TileType.Grace:
                    render.material.color = Color.black;
                    selectable = false;
                    break;
                default:
                    if (selectable)
                    {
                        render.material.color = Color.green;
                    }
                    else
                    {
                        if (tile.State == Shared.Model.TileState.Open)
                            render.material.color = Color.white;
                        else if (tile.State == Shared.Model.TileState.SemiOpen)
                            render.material.color = Color.gray;
                        else
                            render.material.color = Color.black;

                    }
                    break;
            }
        }

        public void OnRayCast()
        {
            OnClicked.Invoke();           
        }
    }
}