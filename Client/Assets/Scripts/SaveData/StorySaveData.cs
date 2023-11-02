using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySaveData : SaveData
{
    public StorySaveData(GameSaveDatas gameData) : base(gameData)
    {
    }

    public long ChapterId { get; set; }
    public long WorldId { get; set; }
    public int Hp { get; set; }
    public int Stamina { get; set; }
    public int Page { get; set; }
    public int CurSelectedVeris { get; set; }
    public int CurMaxVeris { get; set; }

    public override void Load()
    {
        if (gameData.Manager.Data is SaveManager.StoryData storyData)
        {
            ChapterId = storyData.ChapterId;
            WorldId = storyData.WorldId;
            Stamina = storyData.Stamina;
            Page = storyData.Page;
            CurSelectedVeris = storyData.CurSelectedVeris;
            CurMaxVeris = storyData.CurMaxVeris;
            Hp = storyData.Hp;
        }

    }

    public override void Save()
    {
        if (gameData.Manager.Data is SaveManager.StoryData storyData)
        {           
            storyData.ChapterId = ChapterId;
            storyData.WorldId = WorldId;
            storyData.Stamina = Stamina;
            storyData.Page = Page;
            storyData.CurSelectedVeris = CurSelectedVeris;
            storyData.CurMaxVeris = CurMaxVeris;
            storyData.Hp = Hp;
        }
    }
}
