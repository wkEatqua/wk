using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Epos.UI
{
    public class RangeWeaponCanvas : MonoBehaviour
    {
        public TextMeshProUGUI descText;
        public TextMeshProUGUI statText;
        public Image weaponImage;

        public Button AddButton;
        public Button RemoveButton;

        public GameObject Emphasize;

        public void Init(ItemObject obj)
        {
            RangeWeapon weapon = obj.item as RangeWeapon;
            descText.text = weapon.Desc;
            descText.text = $"공격력 : {weapon.Atk}\n사거리 : {weapon.Range}\n사용 횟수 : {weapon.UseCount}";
            AddButton.onClick.RemoveAllListeners();
            RemoveButton.onClick.RemoveAllListeners();
            Emphasize.SetActive(false);
            AddButton.onClick.AddListener(() =>
            {
                if (Inventory.Instance.AddItem(weapon))
                {
                    
                }
                else
                {
                    Emphasize.SetActive(true);
                    Inventory.Instance.ChangeClickEvents(slot =>
                    {
                        Emphasize.SetActive(false);

                    });
                }
            });
            RemoveButton.onClick.AddListener(() =>
            {
                obj.Die();
            });
        }
    }
}