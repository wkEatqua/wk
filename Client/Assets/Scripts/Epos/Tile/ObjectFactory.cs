using Apis;
using Shared.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public enum ItemType
    {
        Monster,
        Item,
        Card,
        Gold,
        Environment,
        Interaction,
        BlankTile
    }
    public class ObjectFactory : MonoBehaviour
    {
        private static bool initialized = false;

        public static TileObject CreateObject(EposTileInfoInfo tileInfo, GameObject gameObject)
        {
            if (tileInfo == null || gameObject == null)
            {
                Debug.Log("tileInfo or gameObject is null");
                return null;
            }

            long ObjectID = GetObjectID(tileInfo);
            EposData.TryGetEposObject(ObjectID, out EposObjectInfo ObjectInfo);
            // Check Object Info
            ItemType type = (ItemType)Enum.Parse(typeof(ItemType), ObjectInfo.ItemType);
            switch(type)
            {
                case ItemType.Item:
                    long itemID = GetItemID(ObjectInfo.ProduceInfo);
                    if (itemID == 0) break;
                    gameObject.SetActive(false);
                    ItemObject item = gameObject.AddComponent<ItemObject>();
                    item.ItemID = itemID;
                    gameObject.SetActive(true);
                    break;
                case ItemType.Card:
                case ItemType.Gold:
                case ItemType.Environment:
                case ItemType.BlankTile:
                case ItemType.Interaction:
                    break;
            }

            var objectComponent = gameObject.GetComponent<TileObject>();
            if (objectComponent != null)
            {
                objectComponent.SetObjectInfo(ObjectInfo);
            }
                
            return objectComponent;
        }

        static long GetItemID(long itemRateID)
        {
            ItemData.TryGetItemRateInfo(itemRateID,out var ItemRateInfo);
            
            Dictionary<long, long> itemDict = new();


            if (ItemRateInfo == null)
            {
                Debug.Log(itemRateID);

                return 0;
            }
            itemDict.TryAdd(ItemRateInfo.Item1, ItemRateInfo.ItemRate1);
            itemDict.TryAdd(ItemRateInfo.Item2, ItemRateInfo.ItemRate2);
            itemDict.TryAdd(ItemRateInfo.Item3, ItemRateInfo.ItemRate3);
            itemDict.TryAdd(ItemRateInfo.Item4, ItemRateInfo.ItemRate4);
            itemDict.TryAdd(ItemRateInfo.Item5, ItemRateInfo.ItemRate5);

            int rand = UnityEngine.Random.Range(0, 10001);
            long value = 0;
            long itemId = 0;
            foreach (var x in itemDict.Keys)
            {
                if (x == 0) continue;

                value += itemDict[x];
                if (rand <= value)
                {
                    itemId = x;
                    break;
                }
            }
            return itemId;
        }
        private static long GetObjectID(EposTileInfoInfo tileInfo)
        {
            Tile.TileType tileType = (Tile.TileType)Enum.Parse(typeof(Tile.TileType), tileInfo.Tile);
            long ObjectID = 0;
            switch (tileType)
            {
                case Tile.TileType.Tier:
                    {
                        EposData.TryGetEposTierTile(tileInfo.GroupID, out List<EposTierTileInfo> Infos);
                        ObjectID = GetObjectID<EposTierTileInfo>(Infos).Object;
                    }
                    break;
                case Tile.TileType.BlankTile:
                    {
                        EposData.TryGetEposBlankTile(tileInfo.GroupID, out List<EposBlankTileInfo> Infos);
                        ObjectID = GetObjectID<EposBlankTileInfo>(Infos).Object;
                    }
                    break;
                case Tile.TileType.InteractionTile:
                    {
                        EposData.TryGetEposInteractionTile(tileInfo.GroupID, out List<EposInteractionTileInfo> Infos);
                        ObjectID = GetObjectID<EposInteractionTileInfo>(Infos).Object;
                    }
                    break;
                case Tile.TileType.EnvironmentTile:
                    {
                        EposData.TryGetEposEnvironmentTile(tileInfo.GroupID, out List<EposEnvironmentTileInfo> Infos);
                        ObjectID = GetObjectID<EposEnvironmentTileInfo>(Infos).Object;
                    }
                    break;
            }

            return ObjectID;
        }
        private static T GetObjectID<T>(List<T> Infos)
        {
            long maxRates = 0;
            List<Tuple<long, long>> rates = new List<Tuple<long, long>>();
            var fields = typeof(T).GetProperties();
            System.Reflection.PropertyInfo field = null;
            foreach (var item in fields)
            {
                if (item.Name == "ObjectRate")
                {
                    field = item;
                }
            }

            foreach (var Info in Infos)
            {
                maxRates += (long)field.GetValue(Info);
            }
            long rand = UnityEngine.Random.Range(0, (int)maxRates);
            long sum = 0;
            foreach(var Info in Infos)
            {
                sum += (long)field.GetValue(Info);
                if (sum >= rand)
                {
                    return Info;
                }
            }

            // error;
            return Infos[0];
        }
    }
}

