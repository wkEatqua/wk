using Epos;
using Shared.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Epos
{
    public class ItemFactory
    {
        public static Item Create(long ItemID)
        {
            ItemData.TryGetItemInfo(ItemID, out EposItemInfo itemInfo);
            return Activator.CreateInstance(Type.GetType("Epos." + itemInfo.Type.ToString()), itemInfo) as Item;
        }
    }
}