using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.UI
{
    public class CardTail : UI_Base
    {
        CardUI cardUI;
        public void Init(CardUI cardUI)
        {
            this.cardUI = cardUI;
        }
    }
}