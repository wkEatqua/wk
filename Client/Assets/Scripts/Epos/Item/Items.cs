using Apis;
using Epos.UI;
using Org.BouncyCastle.Bcpg;
using Shared.Data;
using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public abstract class Item
    {
        protected readonly EposItemInfo data;
        public readonly ItemObject tileObj;
        public Item(EposItemInfo data,ItemObject tileObj)
        {
            this.data = data;
            ScriptData.TryGetGameText(data.DescText, out EposGameTextInfo info);
            Desc = info.Kor;

            this.tileObj = tileObj;
        }

        public string Name => data.Name;
        public readonly string Desc;
        public abstract void OnCollect();
    }
    public abstract class InvenItem : Item
    {
        public ItemSlot Slot;
        int useCount;
        public int UseCount => useCount;
        public virtual void OnClick()
        {
            
        }

        public virtual void Use()
        {
            useCount--;
            if (useCount == 0)
            {
                if (Slot != null)
                {
                    Slot.Remove();
                }
            }
        }
        protected InvenItem(EposItemInfo data, ItemObject itemObj) : base(data, itemObj)
        {
            useCount = data.UseCount;
        }

    }
    public class RangeWeapon : InvenItem
    {
        public readonly int Atk;
        public readonly int Range;
        public static AddressablePooling pool;
        public RangeWeapon(EposItemInfo data,ItemObject itemObj) : base(data, itemObj)
        {
            pool ??= new("RangeWeaponUI");
            Atk = data.BaseStat;
            Range = data.Range;
        }

        public override void OnCollect()
        {
            GameObject obj = pool.Get("RangeWeaponCanvas");
            RangeWeaponCanvas canvas = obj.GetComponent<RangeWeaponCanvas>();
            canvas.Init(tileObj);
        }
        public override void OnClick()
        {
            base.OnClick();
        }
        public override void Use()
        {
            base.Use();
            Player player = EposManager.Instance.Player;
            player.DoRange(Range, tile => tile.Selector.OnClicked.AddListener(() =>
            {
                tile.Selector.selectable = false;
                if(tile.Selector.Obj != null && tile.Selector.Obj is Monster monster)
                {
                    player.RangeAttack(monster);
                    TurnManager.Instance.EndTurn();
                }
                else
                {
                    EposManager.Instance.StartCoroutine(TileManager.Instance.MakeInjectedSelectable());
                }
            }));
        }
    }
    public class GoldItem : Item
    {
        public GoldItem(EposItemInfo data,ItemObject itemObj) : base(data, itemObj)
        {
        }

        public override void OnCollect()
        {
            EposManager.Instance.gold += data.BaseStat;
        }
    }

    public class HpPotion : Item
    {
        public HpPotion(EposItemInfo data, ItemObject itemObj) : base(data, itemObj)
        {
        }

        public override void OnCollect()
        {
            EposManager.Instance.Player.CurHp += data.BaseStat;
        }
    }

    public class MeleeWeapon : Item
    {
        public int durability;

        public MeleeWeapon(EposItemInfo data, ItemObject itemObj) : base(data, itemObj)
        {
            durability = data.BaseStat;
        }

        public override void OnCollect()
        {
            EposManager.Instance.Player.Equip(this);
        }
    }

    public class Armour : Item
    {
        public int durability;
        public int count;

        public Armour(EposItemInfo data, ItemObject itemObj) : base(data, itemObj)
        {
            durability = data.BaseStat;
            count = data.UseCount;
        }

        public override void OnCollect()
        {
            EposManager.Instance.Player.Equip(this);
        }
    }
}