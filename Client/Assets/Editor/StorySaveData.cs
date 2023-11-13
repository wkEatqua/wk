using System;
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
        ChapterId = MainStoryManager.chapterId;
        SaveManager.SaveData data = gameData.Manager.DataList.Find(x => x.SaveId == ChapterId);
        
        if (data != null && data is SaveManager.StoryData storyData)
        {
            ChapterId = storyData.ChapterId;
            WorldId = storyData.WorldId;
            storyData.SaveId = ChapterId;
            Stamina = storyData.Stamina;
            Page = storyData.Page;
            CurSelectedVeris = storyData.CurSelectedVeris;
            CurMaxVeris = storyData.CurMaxVeris;
            Hp = storyData.Hp;
            isLoaded = true;
        }

    }

    public override void Save()
    {
        ChapterId = MainStoryManager.chapterId;
        WorldId = MainStoryManager.worldId;

        SaveManager.SaveData data = gameData.Manager.DataList.Find(x => x.SaveId == ChapterId);

        SaveManager.StoryData storyData = new();

        if(data != null && data is SaveManager.StoryData)
        {
            storyData = data as SaveManager.StoryData;
        }
        else
        {
            gameData.Manager.DataList.Add(storyData);
        }

        storyData.SaveId = ChapterId;
        storyData.ChapterId = ChapterId;
        storyData.WorldId = WorldId;
        storyData.Stamina = Stamina;
        storyData.Page = Page;
        storyData.CurSelectedVeris = CurSelectedVeris;
        storyData.CurMaxVeris = CurMaxVeris;
        storyData.Hp = Hp;
    }
}
