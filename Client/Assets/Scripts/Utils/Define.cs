using System.Collections;
using System.Collections.Generic;


public class Define
{
    public enum ActorStatType
    {
        MaxHp, Atk, AtkRange, Def, DmgTake, CritProb, CritDmg, MoveSpeed, Sight, Damage, GoldGain
    }
    public enum ValueType
    {
        Value, Ratio
    }

    public enum Direction
    {
        Left, Right, Up, Down
    }

    public enum BuffEventType
    {
        OnRangeAttack,
        OnMeleeAttack,
        OnHit,
        OnItemObtain,
        None
    }
    public enum CardPattern
    {
        Clover, Heart, Diamond, Spade
    }
    public enum UIEvent
    {
        Click,
        Drag,
        Drop,
        PointUp,
        PointEnter,
        PointExit,
        PointStay
    }
}