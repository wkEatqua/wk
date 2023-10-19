using Ganss.Excel;
using Newtonsoft.Json;
using Shared.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#if UNITY_EDITOR
using UnityEngine;
#endif

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
            var json = string.Empty;
#if UNITY_EDITOR
            TextAsset jsonFile = Resources.Load<TextAsset>("JsonData/" + typeof(T).Name);
            if (jsonFile != null)
            {
                json = jsonFile.text;
            }
#else
            using (StreamReader r = new StreamReader($"JsonData/{typeof(T).Name}.json"))
            {
                json = r.ReadToEnd();
            }
#endif
            return JsonService.DeserializePlainObject<List<T>>(json).ToList();
        }
    }
}
