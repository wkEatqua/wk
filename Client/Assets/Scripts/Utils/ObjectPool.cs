using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    // ������Ʈ Ǯ��
    // ���׸��� ������ Ÿ�� �Է� �� ���

    readonly GameObject pool; // ������ ������Ʈ�� ������, ���� �� �뵵 X


    // ������ ������ Dictionary, Key ������ ������ �̸� ��
    readonly IDictionary<string, T> prefabs = new Dictionary<string, T>();

    // ������Ʈ�� ������ Queue Dictionary, Key ������ �����ϰ� ������ �̸� ��
    readonly IDictionary<string, Queue<T>> queue = new Dictionary<string, Queue<T>>();


    // ������, ������ ������ �迭 �Է�

    public ObjectPool(T[] objs, bool isDontDestroy = false)
    {
        // Ŭ���� �̸����� pool ������Ʈ �̸� ����
        pool = new GameObject(typeof(T).FullName + " Pool");
        if (isDontDestroy) Object.DontDestroyOnLoad(pool);
        // ������ ������ŭ Dictionary�� ��� �߰�
        for (int i = 0; i < objs.Length; i++)
        {
            prefabs.Add(objs[i].name, objs[i]);
            queue.Add(objs[i].name, new Queue<T>());
        }
    }

    void CreateNew(string name) // ���ο� ������Ʈ ���� �Լ�, ������ �̸� �Ű������� ����
    {
        // Queue�� ���� ������Ʈ�� ��� ���� �����ؾ��� �� ȣ����

        // ������ ��Ͽ� ���ԵǾ� ���� ��� ���� �����Ͽ� ��ȯ
        if (prefabs.ContainsKey(name))
        {
            T obj = Object.Instantiate(prefabs[name]).GetComponent<T>();
            obj.name = name;
            obj.transform.SetParent(pool.transform);
            obj.gameObject.SetActive(false);
            queue[name].Enqueue(obj);
        }
    }
    public T Get(string name) // �ܺο��� ������Ʈ ������ �� ���, �Ű������� ������ �̸� ����
    {
        T obj;

        //ť ��Ͽ� ���ԵǾ� ���� ��� �������� ��ȯ��
        if (queue.ContainsKey(name))
        {
            // ������Ʈ�� �������� �� Dequeue�� ������
            if (queue[name].Count == 0)
            {
                CreateNew(name);
            }

            obj = queue[name].Dequeue();
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(null);

            return obj;
        }
        else
        {
            return null;
        }
    }

    // �ܺο��� ȣ���ϴ� ������Ʈ ��ȯ�� �Լ�
    // ������Ʈ Ǯ���� ����� ��, Destroy�� �ƴ� �� �Լ��� �̿� �ؾ���
    public void Return(T obj)
    {
        // Queue�� ���Ե� ������Ʈ�Ͻ� queue�� �߰�, �ƴҽ� �׳� Destroy

        if (queue.ContainsKey(obj.name))
        {
            queue[obj.name].Enqueue(obj);
            obj.transform.SetParent(pool.transform);
            obj.gameObject.SetActive(false);
        }
        else
        {
            Object.Destroy(obj.gameObject);
        }
    }
}

