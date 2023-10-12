using Ganss.Excel;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using System.Linq;

namespace Shared.Data
{
    public abstract class Database
    {
        public abstract void ProcessDataLoad(string path);
    }

    public class Data<T> where T : new()
    {
        protected readonly string FolderName = "Excel";

        public IEnumerable<T> GetData(string rootPath)
        {
#if UNITY_EDITOR

            return new ExcelMapper($"{Application.dataPath}/{FolderName}/{typeof(T).Name}.xlsx").Fetch<T>();


#else
            TextAsset jsonFile = Resources.Load<TextAsset>("JsonData/" + typeof(T).Name);
            string json = "";
            if (jsonFile != null)
            {
                json = jsonFile.text;
            }
            
            var x = JsonConvert.DeserializeObject<Dictionary<string,T>>(json).Values;
            
            return x.ToList();
#endif
        }
    }

}
