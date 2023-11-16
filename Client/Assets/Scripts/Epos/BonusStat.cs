using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StatKeyPair<T> where T : Enum
{
    public T Key;
    public int Value;
}
[System.Serializable]
public class BonusStat<T> where T : Enum
{
    // 영구 추가 스탯 관련 클래스 (무기, 악세서리 등)

    public readonly IDictionary<T, int> Value = new Dictionary<T, int>(); // 추가값
    public readonly IDictionary<T, int> Ratio = new Dictionary<T, int>(); // 추가 배율

    [Header("추가 스탯 값, 스탯 타입과 정수 입력")]
    [SerializeField] List<StatKeyPair<T>> values = new();

    [Header("추가 스탯 배율, 스탯 타입과 백분율 입력")]
    [SerializeField] List<StatKeyPair<T>> ratios = new();

    public BonusStat()
    {
        foreach (T x in Enum.GetValues(typeof(T)))
        {
            Value.Add(x, 0);
            Ratio.Add(x, 0);
        }
    }

    public void Init() // 인스펙터창에서 값 조절할 시 호출 해야하는 함수
    {
        Reset();
        foreach (var x in values)
        {
            Value[x.Key] += x.Value;
        }
        foreach (var x in ratios)
        {
            Ratio[x.Key] += x.Value;
        }
    }
    public void Reset() // 초기화 함수
    {
        foreach (T x in Enum.GetValues(typeof(T)))
        {
            if (Value.ContainsKey(x))
                Value[x] = 0;
        }
        foreach (T x in Enum.GetValues(typeof(T)))
        {
            if (Ratio.ContainsKey(x))
                Ratio[x] = 0;
        }
    }
    public void AddValue(T type, int value) // 값 추가
    {
        Value[type] = value;
    }
    public void AddRatio(T type, int ratio) // 배율 추가
    {
        Ratio[type] = ratio;
    }
    public static BonusStat<T> operator +(BonusStat<T> a, BonusStat<T> b)
    {
        BonusStat<T> c = new();
        foreach (T x in Enum.GetValues(typeof(T)))
        {
            if (a.Value.ContainsKey(x) && b.Value.ContainsKey(x))
            {
                c.Value[x] = a.Value[x] + b.Value[x];
            }
        }
        foreach (T y in Enum.GetValues(typeof(T)))
        {
            if (a.Ratio.ContainsKey(y) && b.Ratio.ContainsKey(y))
            {
                c.Ratio[y] = a.Ratio[y] + b.Ratio[y];
            }
        }

        return c;
    }
    public static BonusStat<T> operator -(BonusStat<T> a, BonusStat<T> b)
    {
        BonusStat<T> c = new();
        foreach (T x in Enum.GetValues(typeof(T)))
        {
            if (a.Value.ContainsKey(x) && b.Value.ContainsKey(x))
            {
                c.Value.Add(x, 0);
                c.Value[x] = a.Value[x] - b.Value[x];
            }
        }
        foreach (T y in Enum.GetValues(typeof(T)))
        {
            if (a.Ratio.ContainsKey(y) && b.Ratio.ContainsKey(y))
            {
                c.Ratio.Add(y, 0);
                c.Ratio[y] = a.Ratio[y] - b.Ratio[y];
            }
        }

        return c;
    }
}
