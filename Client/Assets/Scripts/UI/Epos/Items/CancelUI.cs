using Epos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Epos.UI
{
    public class CancelUI : MonoBehaviour
    {
        public Button accept;
        public Button cancel;

        public void Init()
        {
            accept.onClick.RemoveAllListeners();
            cancel.onClick.RemoveAllListeners();

            accept.onClick.AddListener(() =>
            {
                GetComponentInParent<RangeWeaponCanvas>().Return();
            });
        }
       
    }
    
}