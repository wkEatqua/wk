using System.Collections;
using System.Collections.Generic;

namespace Epos
{
    public enum ActorStatType
    {
        MaxHp, Atk, AtkRange, Def, DmgTake, CritProb, CritDmg, MoveSpeed, Sight,Damage,GoldGain
    }
    public enum StatType
    {
        Value, Ratio
    }

    public enum Direction
    {
       Left,Right, Up, Down
    }

    public enum BuffEventType
    {
        OnRangeAttack,
        OnMeleeAttack,
        OnHit,
        OnItemObtain,
        None
    }
}