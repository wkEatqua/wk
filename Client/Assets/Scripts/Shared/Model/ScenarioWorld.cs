﻿namespace Shared.Model
{
    
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
    [System.Serializable]
    public class ScenarioWorld
    {
        public long UniqueId { get; set; }
        public string Name { get; set; }
        public string LocalizeKey { get; set; }        
    }
    [System.Serializable]

    public class ScenarioChapter
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

    public class ScenarioPageImage
    {
        public long UniqueId { get; set; }
        public long PageId { get; set; }
        public string ImagePath { get; set; }
        public int ImageActiveOrder { get; set; }
    }
    [System.Serializable]
    public class ScenarioPage
    {
        public int ChapterId { get; set; }
        public long UniqueId { get; set; }
        public string DevName { get; set; }
        public long SelectGroupId { get; set; }
    }
    [System.Serializable]

    public class ScenarioPageText
    {
        public long UniqueId { get; set; }
        public long PageId { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
    }
    [System.Serializable]

    public class ScenarioSelect
    {
        public long UniqueId { get; set; }
        public long GroupId { get; set; }
        public string SelectText { get; set; }
        public SelectType SelectType { get; set; }
        public int SelectValue { get; set; }
        public int SelectEnergy { get; set; }
        public int SelectVerisimilitude { get; set; }
    }
    [System.Serializable]

    public class ScenarioDice
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

    public class ScenarioChapterReward
    {
        public long UniqueId { get; set; }
        public ParcelType RewardType { get; set; }
        public long RewardId { get; set; }
        public int RewardAmount { get; set; }

    }
    [System.Serializable]

    public class ScenarioEnding
    {
        public long UniqueId { get; set; }
        public long ChapterId { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
    }
}