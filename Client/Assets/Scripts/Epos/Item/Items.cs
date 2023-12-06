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
        public override void Use()
        {
            base.Use();
            Debug.Log("남은 사용 횟수 : " + UseCount);
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