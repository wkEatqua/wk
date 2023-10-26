using Febucci.UI.Core;
using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStoryManager
{

    public static long worldId = -1;
    public static long chapterId = -1;

    static ScenarioChapterInfo chapterInfo;
    static ScenarioWorldInfo worldInfo;
    static List<ScenarioPageInfo> pages;

    public static ScenarioChapterInfo ChapterInfo { get
        {
            if(chapterInfo == null || chapterInfo.UniqueId != chapterId)
            {
                ScenarioData.TryGetChapter(chapterId, out chapterInfo);
            }
            return chapterInfo;
        }
    }
    public static ScenarioWorldInfo WorldInfo
    {
        get
        {
            if(worldInfo == null || worldInfo.UniqueId != worldId)
            {
                ScenarioData.TryGetWorld(worldId, out worldInfo);
            }
            return worldInfo;
        }
    }
    public static List<ScenarioPageInfo> Pages
    {
        get
        {
            if(pages == null || pages[0].ChapterId != chapterId)
            {
                ScenarioData.TryGetPageGroup(chapterId, out pages);
            }
            return pages;
        }
    }
      
}
