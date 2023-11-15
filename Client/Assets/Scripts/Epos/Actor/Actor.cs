using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public abstract partial class Actor : MonoBehaviour
    {
        protected virtual void Awake()
        {
            if (curHp == 0) curHp = MaxHp;

            InitStatStrategy();
            bonusStats.Add(bonusStat);
        }
        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}