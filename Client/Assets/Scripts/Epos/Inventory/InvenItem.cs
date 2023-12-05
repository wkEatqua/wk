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
        int UseCount;
        public virtual void Use()
        {
            UseCount--;
            if (UseCount == 0)
            {
                if(Slot != null)
                {
                    Slot.Remove();
                }
            }
        }

        protected InvenItem(EposItemInfo data) : base(data)
        {
            UseCount = data.UseCount;
        }

    }
}