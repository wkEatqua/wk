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

        private void OnEnable()
        {
            ItemData.TryGetItemInfo(ItemID, out EposItemInfo itemInfo);
            item = Activator.CreateInstance(Type.GetType("Epos." + itemInfo.Type.ToString()), itemInfo, this) as Item;
        }
       
        public override void Collect()
        {
            base.Collect();
            item.OnMove();
            item = null;
            Die();
        }
    }
}