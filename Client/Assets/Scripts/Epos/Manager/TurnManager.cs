using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    int turnCount;
    public int TurnCount => turnCount;

    protected override void Awake()
    {
        base.Awake();
        turnCount = 0;
    }

}
