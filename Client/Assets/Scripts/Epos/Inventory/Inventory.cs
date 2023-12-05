using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Epos
{
    public class Inventory : MonoBehaviour
    {
        List<ItemSlot> slots = new();

        public int maxSlot;

        private void Awake()
        {
            slots = GetComponentsInChildren<ItemSlot>().ToList();

            ResetSlots();
        }

        void ResetSlots()
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (i < maxSlot)
                {
                    slots[i].Active();
                }
                else
                {
                    slots[i].DeActive();
                }
            }
        }

        public void AddSlot(int amount)
        {
            maxSlot += amount;
            if(maxSlot > slots.Count)
            {
                maxSlot = slots.Count;
            }
        }

    }
}