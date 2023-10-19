using Shared.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Shared.Data
{
    public static class DataManager
    {
        public static Dictionary<uint, Database> DataDict = new();

        public static void Load(string path)
        {
            Initialize();

            foreach (var database in DataDict.Values)
            {
                database.ProcessDataLoad(path);
            }
        }

        public static void Initialize()
        {
            var types = typeof(Database).Assembly.GetTypes().Where(v => v.IsSubclassOf(typeof(Database)));

            foreach (var type in types)
            {
                AddData((Database)Activator.CreateInstance(type));
            }
        }


        private static void AddData(Database database)
        {
            uint key = XXHashService.CalculateHash(database.GetType().Name);

            if (DataDict.ContainsKey(key))
            {
#if UNITY_EDITOR
                Debug.LogError($"!! Warning. {database.GetType().Name} data duplicated. ");
#endif
            }

            DataDict.Add(key, database);
        }

        public static T Get<T>() where T : Database, new()
        {
            string name = typeof(T).Name;
            uint key = XXHashService.CalculateHash(name);
            if (DataDict != null && DataDict.ContainsKey(key))
                return (T)DataDict[key];
            else
                return null;
        }
    }
}
