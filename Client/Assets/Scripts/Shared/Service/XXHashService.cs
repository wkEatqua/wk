using System.Text;
using YYProject.XXHash;

namespace Shared.Service
{
    public class XXHashService
    {
        public static uint CalculateHash(string name)
        {
            if (string.IsNullOrEmpty(name))
                return 0;
            else
            {
                byte[] bytes = Encoding.UTF8.GetBytes(name);
                var hash = XXHash32.Create();

                hash.ComputeHash(bytes);
                return hash.HashUInt32;
            }
        }
    }
}
