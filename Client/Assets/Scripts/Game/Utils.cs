

namespace WK.Battle
{

    /// <summary>
    /// 무슨종류 스킬?
    /// </summary>
    public enum SkillOption
    {
        Attack,
        Buff

    }

    /// <summary>
    /// 스킬종류 
    /// </summary>
    public enum SkillName
    {
        Skill0, Skill1, Skill2, Skill3, Skill4, Skill5, Skill6
    }

    /// <summary>
    /// 주사위 - 무엇을 강화?
    /// </summary>
    public enum DiceOption
    {
        Dice0, Dice1, Dice2, Dice3, Dice4, Dice5, Dice6, Dice7, Dice8, Dice9, Dice10, Dice11, Dice12, Dice13, Dice14, Dice15, Dice16, Dice17, Dice18, Dice19, Dice20
    }



    /// <summary>
    /// 무기 - 무기종류
    /// </summary>
    public enum WeaponType
    {
        PUNCH,      //1. 주먹
        SWORD,      //2. 검
        MAGIC,      //3. 지팡이
        GUN,        //4. 총
        BATTA       //5. 몽둥이
    }



    /// <summary>
    /// 방어구 - 방어구종류
    /// </summary>
    public enum DefenceType
    {
        NONE,           //0. 주먹
        METAL,          //1. 판금
        LIGHTARMOR,     //2. 경갑
        LEATHER,        //3. 가죽
        CLOTHE,         //4. 천
        HARDARMOR       //5 - 방탄
    }

    public enum NameText
    {
        NAME0, NAME1, NAME2, NAME3, NAME4, NAME5, NAME6, NAME7, NAME8, NAME9, NAME10
    }

}