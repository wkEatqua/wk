using Shared.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public class ItemObject : TileObject
    {
        public long ItemID;

        public Item item;

        private void Start()
        {
            ItemData.TryGetItemInfo(ItemID, out EposItemInfo itemInfo);
            item = Activator.CreateInstance(Type.GetType("Epos." + itemInfo.Type.ToString()), itemInfo) as Item;
        }
        public virtual void Collect()
        {
            item.OnCollect();
            item = null;
            Die();
        }
    }
}