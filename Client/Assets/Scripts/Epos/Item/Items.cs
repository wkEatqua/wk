using Apis;
using Epos.UI;
using NPOI.SS.Formula.Functions;
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

        public int Durability;
        public int Count;
        public int Atk;
        public int Range;
        public int Type;
        public Item(EposItemInfo data, ItemObject tileObj)
        {
            this.data = data;
            ScriptData.TryGetGameText(data.DescText, out EposGameTextInfo info);

            Desc = info?.Kor;

            this.tileObj = tileObj;
        }

        public Item(EposItemInfo data)
        {
            this.data = data;
            ScriptData.TryGetGameText(data.DescText, out EposGameTextInfo info);

            Desc = info?.Kor;
        }
        public string Name => data.Name;
        public readonly string Desc;
        public abstract void OnCollect();
        public virtual void AddStat(ActorStatType statType, AddType addType, int amount)
        {

        }
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
        public InvenItem(EposItemInfo data, ItemObject itemObj) : base(data, itemObj)
        {
            useCount = data.UseCount;
        }
        public InvenItem(EposItemInfo data) : base(data)
        {
            useCount = data.UseCount;
        }
    }
    public class RangeWeapon : InvenItem
    {
        public RangeWeapon(EposItemInfo data, ItemObject itemObj) : base(data, itemObj)
        {
            Atk = data.BaseStat;
            Range = data.Range;
        }
        public RangeWeapon(EposItemInfo data) : base(data)
        {
            Atk = data.BaseStat;
            Range = data.Range;
        }
        public override void OnCollect()
        {
            GameObject obj = UIPool.Get("RangeWeaponCanvas");
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
                if (tile.Selector.Obj != null && tile.Selector.Obj is Monster monster)
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
        public override void AddStat(ActorStatType statType, AddType addType, int amount)
        {
            base.AddStat(statType, addType, amount);

            switch (statType)
            {
                case ActorStatType.Atk:
                    switch (addType)
                    {
                        case AddType.Value:
                            Atk += amount;
                            break;
                        case AddType.Ratio:
                            Atk *= 100 + amount;
                            Atk /= 100;
                            break;
                    }
                    break;
                case ActorStatType.AtkRange:
                    switch (addType)
                    {
                        case AddType.Value:
                            Range += amount;
                            break;
                        case AddType.Ratio:
                            Range *= 100 + amount;
                            Range /= 100;
                            break;
                    }
                    break;
            }
        }
    }
    public class GoldItem : Item
    {
        public GoldItem(EposItemInfo data, ItemObject itemObj) : base(data, itemObj)
        {
        }
        public GoldItem(EposItemInfo data) : base(data)
        {
        }

        public override void OnCollect()
        {
            EposManager.Instance.Gold += data.BaseStat;
        }
    }

    public class HpPotion : Item
    {
        public HpPotion(EposItemInfo data, ItemObject itemObj) : base(data, itemObj)
        {
        }
        public HpPotion(EposItemInfo data) : base(data)
        {
        }

        public override void OnCollect()
        {
            EposManager.Instance.Player.CurHp += data.BaseStat;
        }
    }

    public class MeleeWeapon : Item
    {
        public MeleeWeapon(EposItemInfo data, ItemObject itemObj) : base(data, itemObj)
        {
            Durability = data.BaseStat;
        }
        public MeleeWeapon(EposItemInfo data) : base(data)
        {
            Durability = data.BaseStat;
        }
        public override void OnCollect()
        {
            EposManager.Instance.Player.Equip(this);
        }
        public override void AddStat(ActorStatType statType, AddType addType, int amount)
        {
            base.AddStat(statType, addType, amount);
            switch (statType)
            {
                case ActorStatType.Atk:
                    switch (addType)
                    {
                        case AddType.Value:
                            Durability += amount;
                            break;
                        case AddType.Ratio:
                            Durability *= 100 + amount;
                            Durability /= 100;
                            break;
                    }
                    break;
            }
        }
    }

    public class Armour : Item
    {

        public Armour(EposItemInfo data, ItemObject itemObj) : base(data, itemObj)
        {
            Durability = data.BaseStat;
            Count = data.UseCount;
        }
        public Armour(EposItemInfo data) : base(data)
        {
            Durability = data.BaseStat;
            Count = data.UseCount;
        }
        public override void OnCollect()
        {
            EposManager.Instance.Player.Equip(this);
        }

        public override void AddStat(ActorStatType statType, AddType addType, int amount)
        {
            base.AddStat(statType, addType, amount);
            switch (statType)
            {
                case ActorStatType.Def:
                    switch (addType)
                    {
                        case AddType.Value:
                            Durability += amount;
                            break;
                        case AddType.Ratio:
                            Durability *= 100 + amount;
                            Durability /= 100;
                            break;
                    }
                    break;
            }
        }
    }
}