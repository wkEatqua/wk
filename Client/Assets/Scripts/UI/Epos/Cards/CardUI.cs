using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace Epos.UI
{
    public class CardUI : UI_Base
    {
        enum GameObjects
        {
            Selection,Card,CardTail
        }

        [HideInInspector]public CardSelections selection;
        [HideInInspector]public CardFront cardFront;
        [HideInInspector]public CardTail cardTail;
        
        [HideInInspector]public EposCardEventInfo cardEventInfo;

        private void OnEnable()
        {
            
        }

        public void Init(EposCardEventInfo info)
        {
            cardEventInfo = info;
            selection.gameObject.SetActive(true);
            cardFront.gameObject.SetActive(false);
            cardTail.gameObject.SetActive(false);

            selection.Init(this);
            cardFront.Init(this);
            cardTail.Init(this);
        }

        private void Awake()
        {
            Bind<GameObject>(typeof(GameObjects));
            selection = Get<GameObject>((int)GameObjects.Selection).GetComponent<CardSelections>();
            cardFront = Get<GameObject>((int)GameObjects.Card).GetComponent<CardFront>();
            cardTail = Get<GameObject>((int)GameObjects.CardTail).GetComponent<CardTail>();
        }
    }
}