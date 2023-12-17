using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPool
{
    static AddressablePooling _pool;
    public static AddressablePooling Pool
    {
        get
        {
            _pool ??= new AddressablePooling("UI_", true);
            return _pool;
        }
    }
    
    public static GameObject Get(string name)
    {
        return Pool.Get(name);
    }
    public static void Return(GameObject obj)
    {
        Pool.Return(obj);
    }
}
