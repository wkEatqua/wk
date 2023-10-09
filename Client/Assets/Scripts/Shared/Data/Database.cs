using Ganss.Excel;
using System.Collections.Generic;

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
            return new ExcelMapper($"{rootPath}{FolderName}/{typeof(T).Name}.xlsx").Fetch<T>();
        }
    }

}
