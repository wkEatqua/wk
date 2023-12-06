using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Epos.UI
{
    public class ChangeWeaponUI : MonoBehaviour
    {
        InvenItem leftItem;
        RangeWeapon rightItem;
        public Button accept;
        public Button cancel;
        public CancelUI cancelUI;

        public void Init(InvenItem leftItem, RangeWeapon rightItem)
        {
            this.leftItem = leftItem;
            this.rightItem = rightItem;
            accept.onClick.RemoveAllListeners();
            cancel.onClick.RemoveAllListeners();

            accept.onClick.AddListener(() =>
            {
                ItemSlot slot = leftItem.Slot;
                slot.Remove();
                slot.Add(rightItem);
                GetComponentInParent<RangeWeaponCanvas>().Return();
                Debug.Log("확인");
            });
            cancel.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                cancelUI.gameObject.SetActive(true);
                cancelUI.Init();
            });
        }
    }
}