using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private void Awake()
        {
            isActive = false;
            itemImage = GetComponent<Image>();
            button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                item?.Use();
            });
        }
        public void Add(InvenItem item)
        {
            this.item = item;
            if(item != null)
            {                
                item.Slot = this;
            }
        }
        public InvenItem Remove()
        {
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