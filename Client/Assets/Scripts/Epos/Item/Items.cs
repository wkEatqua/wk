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
        public Item(EposItemInfo data)
        {
            this.data = data;
        }
        public abstract void OnCollect();
    }

    public class RangeWeapon : InvenItem
    {
        public RangeWeapon(EposItemInfo data) : base(data)
        {
        }

        public override void OnCollect()
        {
            Inventory.Instance.AddItem(this);
        }
        public override void Use()
        {
            base.Use();
        }
    }
    public class GoldItem : Item
    {
        public GoldItem(EposItemInfo data) : base(data)
        {
        }

        public override void OnCollect()
        {
            EposManager.Instance.gold += data.BaseStat;
        }
    }

    public class HpPotion : Item
    {
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
        public int durability;

        public MeleeWeapon(EposItemInfo data) : base(data)
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

        public Armour(EposItemInfo data) : base(data)
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