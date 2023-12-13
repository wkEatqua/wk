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

        CardSelections selection;
        CardFront card;
        CardTail cardTail;

        private void OnEnable()
        {
            selection.gameObject.SetActive(true);
            card.gameObject.SetActive(false);
            cardTail.gameObject.SetActive(false);
        }

        private void Awake()
        {
            Bind<GameObject>(typeof(GameObjects));
            selection = Get<GameObject>((int)GameObjects.Selection).GetComponent<CardSelections>();
            card = Get<GameObject>((int)GameObjects.Card).GetComponent<CardFront>();
            cardTail = Get<GameObject>((int)GameObjects.CardTail).GetComponent<CardTail>();
        }
    }
}