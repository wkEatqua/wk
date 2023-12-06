using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Epos
{
    public class ItemSlot : MonoBehaviour
    {
        InvenItem item;
        public InvenItem Item => item;
        Button button;
        Image itemImage;

        bool isActive;

        public bool IsActive => isActive;

        readonly public UnityEvent<ItemSlot> clickEvent = new();

        public void ChangeClickEvent(UnityAction<ItemSlot> action)
        {
            clickEvent.RemoveAllListeners();
            clickEvent.AddListener(action);
        }
        private void Awake()
        {
            isActive = false;
            itemImage = GetComponent<Image>();
            button = GetComponent<Button>();
            clickEvent.AddListener(slot => item?.Use());
                
            button.onClick.AddListener(() =>
            {
                clickEvent.Invoke(this);
            });
        }
        public void Add(InvenItem item)
        {
            this.item = item;
            if(item != null)
            {                
                item.Slot = this;
                itemImage.color = Color.green;
            }
        }
        public InvenItem Remove()
        {
            item.Slot = null;
            InvenItem temp = item;
            item = null;
            return temp;
        }

        public void Active()
        {
            isActive = true;
            itemImage.color = Color.white;
            button.enabled = true;
        }

        public void DeActive()
        {
            isActive = false;
            itemImage.color = Color.black;
            button.enabled = false;
        }
    }
}