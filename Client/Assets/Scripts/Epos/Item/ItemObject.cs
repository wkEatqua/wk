using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public abstract class ItemObject : TileObject
    {
        public virtual void Collect()
        {
            Die();
        }
    }
}