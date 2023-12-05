using Epos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public abstract class InvenItem : MonoBehaviour
    {
        public ItemSlot Slot;
        public virtual void Use()
        {
            useCount--;
            if (useCount == 0)
            {
                if(Slot != null)
                {
                    Slot.Remove();
                }
            }
        }
        protected int useCount;
        public int UseCount => useCount;
    }
}