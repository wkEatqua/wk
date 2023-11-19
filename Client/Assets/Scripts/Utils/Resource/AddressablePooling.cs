using Apis;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablePooling
{
    readonly GameObject pool;

    readonly Dictionary<string, Queue<GameObject>> queue = new Dictionary<string, Queue<GameObject>>();
    public AddressablePooling(string name, bool isDontDestroy = false)
    {
        pool = new GameObject(name + " Pool");

        if (isDontDestroy) Object.DontDestroyOnLoad(pool);

    }

    void EnQueue(GameObject obj)
    {
        if (obj != null)
        {
            if (!queue.ContainsKey(obj.name))
            {
                queue.Add(obj.name, new Queue<GameObject>());
            }

            obj.SetActive(false);
            queue[obj.name].Enqueue(obj);
        }
    }
    void CreateNew(string name)
    {
        GameObject obj = ResourceUtil.Instantiate(name, pool.transform);

        EnQueue(obj);
    }

    void CreateNew(AssetReference assetReference)
    {
        GameObject obj = ResourceUtil.Instantiate(assetReference, pool.transform);

        EnQueue(obj);
    }
    
    public GameObject Get(string addressableName)
    {
        GameObject obj = ResourceUtil.Load<GameObject>(addressableName);
         
        if(obj != null)
        {
            string name = obj.name;
            if (!queue.ContainsKey(name))
            {
                queue.Add(name, new Queue<GameObject>());
            }

            if (queue[name].Count == 0)
            {
                CreateNew(addressableName);
            }

            if (queue[name].Count > 0)
            {
                obj = queue[name].Dequeue();
                obj.SetActive(true);
                obj.transform.SetParent(null);
            }
        }
        Addressables.Release(obj);

        return obj;
    }

    public GameObject Get(AssetReference assetReference)
    {
        GameObject obj = ResourceUtil.Load<GameObject>(assetReference);

        if (obj != null)
        {
            string name = obj.name;
            Addressables.Release(obj);
            if (!queue.ContainsKey(name))
            {
                queue.Add(name, new Queue<GameObject>());
            }

            if (queue[name].Count == 0)
            {
                CreateNew(assetReference);
            }

            if (queue[name].Count > 0)
            {
                obj = queue[name].Dequeue();
                obj.SetActive(true);
                obj.transform.SetParent(null);
            }
        }

        return obj;
    }

    public void Return(GameObject obj)
    {
        if (queue.ContainsKey(obj.name))
        {
            queue[obj.name].Enqueue(obj);
            obj.transform.SetParent(pool.transform);
            obj.SetActive(false);
        }
        else
        {
            Object.Destroy(obj);
        }
    }
}
