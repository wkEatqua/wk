namespace Shared.Model
{
    /// <summary>
    /// 추출 가능한 데이터 클래스
    /// </summary>
    public class JsonModel
    {

    }

    #region Enum
    public enum StarFlagType
    {
        Progress,
        Verisimilitude,
        ItemMemory
    }

    public enum SelectType
    {
        Dice,
        Heal,
        Wisdom,
        None
    }
    public enum ParcelType
    {
        Character,
    }

    public enum TileDifficulty
    {
        None, Easy, Normal, Danger, Hard, Nightmare, Disaster
    }

    public enum TileState
    {
        Close,
        SemiOpen,
        Open,
    }

    public enum ItemType
    {
        MeleeWeapon,RangeWeapon,HpPotion,Armour,Gold,
    }
    public enum ActorStatType
    {
        MaxHp, Atk, AtkRange, Def, DmgTake, CritProb, CritDmg, MoveSpeed, Sight, Damage, GoldGain
    }
    public enum AddType
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
    public class EposGameText : JsonModel
    {
        public long ID { get; set; }
        public string Kor { get; set; }
    }

    #endregion

    #region 사상지평
    [System.Serializable]
    public class ScenarioWorld : JsonModel
    {
        public long UniqueId { get; set; }
        public string Name { get; set; }
        public string LocalizeKey { get; set; }
    }

    [System.Serializable]
    public class ScenarioChapter : JsonModel
    {
        public long WorldId { get; set; }
        public string Name { get; set; }
        public long UniqueId { get; set; }

        public long HeroId { get; set; }
        public int DefaultHealthMax { get; set; }
        public int DefaultEnergyMax { get; set; }
        public int VerisimilitudeMin { get; set; }
        public int VerisimilitudeMax { get; set; }

        public long OpenConditionId { get; set; }

        public long StarRewardId { get; set; }
    }
    
    [System.Serializable]
    public class ScenarioPageImage : JsonModel
    {
        public long UniqueId { get; set; }
        public long GroupId { get; set; }
        public string ImagePath { get; set; }
        public int ImageActiveOrder { get; set; }
    }
    
    [System.Serializable]
    public class ScenarioPage : JsonModel
    {
        public int ChapterId { get; set; }
        public long UniqueId { get; set; }
        public string DevName { get; set; }
        public long SelectGroupId { get; set; }
        public long TextContentId { get; set; }      
        public long ResultContentGroupId { get; set; }
    }
    
    [System.Serializable]
    public class ScenarioPageText : JsonModel
    {
        public long UniqueId { get; set; }
        public long GroupId { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
    }
    
    [System.Serializable]
    public class ScenarioSelect : JsonModel
    {
        public long UniqueId { get; set; }
        public long GroupId { get; set; }
        public string SelectText { get; set; }
        public SelectType SelectType { get; set; }
        public int SelectValue { get; set; }
        public int SelectEnergy { get; set; }
        public int SelectVerisimilitude { get; set; }
        public long ResultTextContentId { get; set; }        
    }
    
    [System.Serializable]
    public class ScenarioTextContent: JsonModel
    {
        public long GroupId { get; set; }
        public long UniqueId { get; set; }
        public long TextGroupId { get; set; }
        public long ImageGroupId { get; set; }
    }

    [System.Serializable]
    public class ScenarioDice : JsonModel
    {
        public long ChapterId { get; set; }
        public int DiceProbSum { get; set; }

        public int? DiceProb1 { get; set; }
        public int? DiceProb2 { get; set; }
        public int? DiceProb3 { get; set; }
        public int? DiceProb4 { get; set; }
        public int? DiceProb5 { get; set; }
        public int? DiceProb6 { get; set; }
        public int? DiceProb7 { get; set; }
        public int? DiceProb8 { get; set; }
        public int? DiceProb9 { get; set; }
        public int? DiceProb10 { get; set; }
        public int? DiceProb11 { get; set; }
        public int? DiceProb12 { get; set; }
        public int? DiceProb13 { get; set; }
        public int? DiceProb14 { get; set; }
        public int? DiceProb15 { get; set; }
        public int? DiceProb16 { get; set; }
        public int? DiceProb17 { get; set; }
        public int? DiceProb18 { get; set; }
        public int? DiceProb19 { get; set; }
        public int? DiceProb20 { get; set; }
    }
    
    [System.Serializable]
    public class ScenarioChapterReward : JsonModel
    {
        public long UniqueId { get; set; }
        public ParcelType RewardType { get; set; }
        public long RewardId { get; set; }
        public int RewardAmount { get; set; }

    }
    
    [System.Serializable]
    public class ScenarioEnding : JsonModel
    {
        public long UniqueId { get; set; }
        public long ChapterId { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
    }

    [System.Serializable]
    public class ScenarioIntro : JsonModel
    {
        public long ChapterId { get; set; }
        public float BackGroundPosX { get; set; }
        public float BackGroundPosY { get; set; }
        public float BackGroundColorR { get; set; }
        public float BackGroundColorG { get; set; }
        public float BackGroundColorB { get; set; }
        public float BackGroundColorA { get; set; }
        public string CharacterClass { get; set; }
        public string CharacterName { get; set; }

    }

    [System.Serializable]
    public class CharacterScript : JsonModel
    {
        public long GroupId { get; set; }
        public string ScriptText { get; set; }
        public long UniqueId { get; set; }
    }
    #endregion

    #region 에포스
    [System.Serializable]
    public class EposLevel : JsonModel
	{
        public long Level { get; set; }
        public long TileNumber { get; set; }

        public float TileInterval { get; set; }

        public float TileScale { get; set; }
	}
    [System.Serializable]
    public class EposLevelExp : JsonModel
    {
        public long Level { get; set; }
        public int Exp { get; set; }
    }
    [System.Serializable]
    public class EposTileExp : JsonModel
    {
        public TileDifficulty Difficulty { get; set; }
        public int Exp { get; set; }
    }

    [System.Serializable]
    public class EposTile : JsonModel
    {
        public long ID { get; set; }
        public long Lv { get; set; }
        public long Tile1 { get; set; }
        public long TileRate1 { get; set; }
        public long Tile2 { get; set; }
        public long TileRate2 { get; set; }
        public long Tile3 { get; set; }
        public long TileRate3 { get; set; }
        public long Tile4 { get; set; }
        public long TileRate4 { get; set; }
        public long Tile5 { get; set; }
        public long TileRate5 { get; set; }
        public long Tile6 { get; set; }
        public long TileRate6 { get; set; }
        public long Tile7 { get; set; }
        public long TileRate7 { get; set; }
        public long Tile8 { get; set; }
        public long TileRate8 { get; set; }
        public long Tile9 { get; set; }
        public long TileRate9 { get; set; }
    }

    [System.Serializable]
    public class EposTileInfo : JsonModel
    {
        public long GroupID { get; set; }
        public string Tile { get; set; }
        public int Tier { get; set; }
        public string TileModel { get; set; }
        public string Tilepattern { get; set; }
        public int EXP { get; set; }
    }

    [System.Serializable]
    public class EposTierTile : JsonModel
    {
        public long GroupID { get; set; }
        public string Tile { get; set; }
        public long Object { get; set; }
        public long ObjectRate { get; set; }
    }

    [System.Serializable]
    public class EposInteractionTile : JsonModel
    {
        public long GroupID { get; set; }
        public string Tile { get; set; }
        public long Object { get; set; }
        public long ObjectRate { get; set; }
    }

    [System.Serializable]
    public class EposEnvironmentTile : JsonModel
    {
        public long GroupID { get; set; }
        public string Tile { get; set; }
        public long Object { get; set; }
        public long ObjectRate { get; set; }
    }

    [System.Serializable]
    public class EposBlankTile : JsonModel
    {
        public long ID { get; set; }
        public string Tile { get; set; }
        public long Object { get; set; }
        public long ObjectRate { get; set; }
    }

    [System.Serializable]
    public class EposObject : JsonModel
    {
        public long ID { get; set; }
        public string Tile { get; set; }
        public string ItemType { get; set; }
        public string Type { get; set; }
        public int Tier { get; set; }
        public string Modeling { get; set; }
        public long ProduceInfo { get; set; }
        public string RewardType { get; set; }
        public long Reward { get; set; }
        public long Trophy { get; set; }
    }

    [System.Serializable]
    public class EposTrophy : JsonModel
    {
        public long ID { get; set; }
        public long TrophyID1 { get; set; }
        public string Type1 { get; set; }
        public long Rate1 { get; set; }
        public long TrophyID2 { get; set; }
        public string Type2 { get; set; }
        public long Rate2 { get; set; }
        public long TrophyID3 { get; set; }
        public string Type3 { get; set; }
        public long Rate3 { get; set; }
        public long TrophyID4 { get; set; }
        public string Type4 { get; set; }
        public long Rate4 { get; set; }
    }

    public class EposCardEvent : JsonModel
    {
        public long Index { get; set; }
        public long BgTitle { get; set; }
        public long BgText { get; set; }
        public CardPattern Pattern { get; set; }
        public long EventName { get; set; }
        public long EventText { get; set; }
    }
    
    public class EposBuff : JsonModel
    {
        public long Index { get; set; }      
        public string BuffType { get; set; }
        public BuffEventType ActiveType { get; set; }
        public float Amount { get; set; }
        public ActorStatType StatType { get; set; }
        public AddType ValueType { get; set; }
        public int BuffCondition { get; set; }
        public int ConditionParam { get; set; }
    }
    public class EposBuffGroup : JsonModel
    {
        public long GroupIndex { get; set; }
        public long BuffIndex { get; set; }
        public int Chance { get; set; }
    }
    #region 아이템 관련

    public class EposItem : JsonModel
    {
        public long ID { get; set; }
        public ItemType Type { get; set; }
        public string Name { get; set; }
        public long DescText { get; set; }
        public int BaseStat { get; set; }
        public int UseCount { get; set; }
        public int Range { get; set; }

    }
    public class EposItemRate : JsonModel
    {
        public long ID { get; set; }
        public long ObjectID { get; set; }
        public int Tier { get; set; }
        public long Item1 { get; set; }
        public int ItemRate1 { get; set; }
        public long Item2 { get; set; }
        public int ItemRate2 { get; set; }

        public long Item3 { get; set; }
        public int ItemRate3 { get; set; }

        public long Item4 { get; set; }
        public int ItemRate4 { get; set; }

        public long Item5 { get; set; }
        public int ItemRate5 { get; set; }
    }
    #endregion

}
#endregion