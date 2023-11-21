using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableTest : MonoBehaviour
{
    AddressablePooling pool;
    void Start()
    {
        pool = new AddressablePooling("Tile");
    }

    void Update()
    {
    }
}
