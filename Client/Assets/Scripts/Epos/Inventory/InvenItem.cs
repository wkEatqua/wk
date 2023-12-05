using Epos;
using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public abstract class InvenItem : Item
    {
        public ItemSlot Slot;
        int useCount;
        public int UseCount => useCount;
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

        protected InvenItem(EposItemInfo data) : base(data)
        {
            useCount = data.UseCount;
        }

    }
}