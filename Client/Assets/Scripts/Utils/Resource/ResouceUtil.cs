using Apis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Apis
{
    public static class ResourceUtil
    {
        public static T Load<T>(string addressableName) where T : UnityEngine.Object
        {
            T resource;            
            try
            {
                if (typeof(T).IsSubclassOf(typeof(Component)))
                {
                    GameObject temp = Addressables.LoadAssetAsync<GameObject>(addressableName).WaitForCompletion();
                    resource = temp.GetComponent<T>();
                }
                else
                {
                    resource = Addressables.LoadAssetAsync<T>(addressableName).WaitForCompletion();
                }
            }
            catch
            {
                Debug.Log("Resource not found: " + addressableName);
                return null;
            }
            return resource;
        }
        public static T Load<T>(AssetReference assetRef) where T : UnityEngine.Object
        {
            T resource;
            try
            {
                if (typeof(T).IsSubclassOf(typeof(Component)))
                {
                    GameObject temp = Addressables.LoadAssetAsync<GameObject>(assetRef).WaitForCompletion();
                    resource = temp.GetComponent<T>();
                }
                else
                {
                    resource = Addressables.LoadAssetAsync<T>(assetRef).WaitForCompletion();
                }
            }
            catch
            {
                Debug.Log("Resource not found: " + assetRef);
                return null;
            }
            return resource;
        }


        public static T[] LoadAll<T>(string label) where T : UnityEngine.Object
        {
            T[] resources;

            try
            {
                if (typeof(T).IsSubclassOf(typeof(Component)))
                {
                    resources = Addressables.LoadAssetsAsync<GameObject>(label, obj => { }).WaitForCompletion().
                                Select(x => x.GetComponent<T>()).Where(x => x != null).ToArray();
                }
                else
                {
                    resources = Addressables.LoadAssetsAsync<T>(label, obj => { }).WaitForCompletion().ToArray();
                }
            }
            catch
            {
                Debug.Log("Label not found");
                return null;
            }

            return resources;
        }

        public static GameObject Instantiate(string addressableName,Transform parent = null)
        {
            GameObject go;
            try
            {
                go = AddressableUtil.InstantiateAsync($"Prefabs/{addressableName}").WaitForCompletion();
            }
            catch
            {
                Debug.Log("어드레서블 로딩 실패");
                return null;
            }

            if(parent != null) go.transform.SetParent(parent);

            int index = go.name.IndexOf("(Clone)");
            if (index > 0)
                go.name = go.name[..index];
            return go;
        }

        public static GameObject Instantiate(AssetReference assetRef, Transform parent = null)
        {
            GameObject go;
            try
            {
                go = AddressableUtil.InstantiateAsync(assetRef).WaitForCompletion();
            }
            catch
            {
                Debug.Log("어드레서블 로딩 실패");
                return null;
            }

            if (parent != null) go.transform.SetParent(parent);

            int index = go.name.IndexOf("(Clone)");
            if (index > 0)
                go.name = go.name[..index];
            return go;
        }

        public static void Destroy(GameObject go)
        {
            if(go == null)
                return;
            UnityEngine.Object.Destroy(go);           
        }

        public static void DontDestroyObject(GameObject go)
        {
            if (go == null)
                return;
            UnityEngine.Object.DontDestroyOnLoad(go);
        }
    }
}
