using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PoolingTest : MonoBehaviour
{
    public AssetReference reference;
    AddressablePooling pool;
    Queue<GameObject> queue = new Queue<GameObject>();
    private void Awake()
    {
        pool = new AddressablePooling("Test");
        for(int i = 0;i<10;i++)
        {
            queue.Enqueue(pool.Get(reference));
        }

        InvokeRepeating(nameof(Return), 2, 2);
        InvokeRepeating(nameof(Get),2.5f, 2.5f);
    }

    void Return()
    {
        if(queue.Count > 0)
        {
            pool.Return(queue.Dequeue());
        }
    }

    void Get()
    {
        queue.Enqueue(pool.Get(reference));
    }
}
