using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Epos
{
    public class Inventory : Singleton<Inventory>
    {
        List<ItemSlot> slots = new();

        public int maxSlot;       

        protected override void Awake()
        {
            slots = GetComponentsInChildren<ItemSlot>().ToList();
            maxSlot = Mathf.Clamp(maxSlot, 0, slots.Count);
        }

        private void Start()
        {
            ResetSlots();
        }
        public void ChangeClickEvents(UnityAction<ItemSlot> clickEvent)
        {
            foreach (ItemSlot slot in slots)
            {
                slot.ChangeClickEvent(clickEvent);
            }
        }
        public void ClickEventsBackToNormal()
        {
            foreach (ItemSlot slot in slots)
            {
                slot.ChangeClickEvent(s => s.Item?.OnClick());
            }
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
            if (maxSlot > slots.Count)
            {
                maxSlot = slots.Count;
            }
        }

        public bool AddItem(InvenItem item)
        {
            for (int i = 0; i < maxSlot; i++)
            {
                if (slots[i].Item == null)
                {
                    slots[i].Add(item);
                    return true;
                }
            }
            return false;
        }
    }
}