using System;

namespace Shared
{
    public class SingletonLazy<T> where T : SingletonLazy<T>, new() 
    {
        private static Lazy<T> lazyInstance = null;

        public static T Instance
        {
            get
            {
                if (Exists() == false)
                {
                    var instance = new T();
                    lazyInstance = new Lazy<T>(() => instance);
                }

                return lazyInstance.Value;
            }
        }
        
        public static bool Exists()
        {
            return lazyInstance != null && lazyInstance.IsValueCreated;
        }
        
        public static void ClearInstance()
        {
            lazyInstance = null;
        }
    }
}
