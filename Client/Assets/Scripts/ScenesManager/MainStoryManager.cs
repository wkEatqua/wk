using Febucci.UI.Core;
using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStoryManager : Singleton<MainStoryManager> 
{
    [Header("테이블 데이터")]
    [SerializeField] long worldId;
    [SerializeField] long chapterId;

    ScenarioChapterInfo chapterInfo;
    ScenarioWorldInfo worldInfo;
    List<ScenarioPageInfo> pages;

    public ScenarioChapterInfo ChapterInfo => chapterInfo;
    public ScenarioWorldInfo WorldInfo => worldInfo;
    public List<ScenarioPageInfo> Pages => pages;

    protected override void Awake()
    {
        base.Awake();

        ScenarioData.TryGetChapter(chapterId, out chapterInfo);
        ScenarioData.TryGetWorld(worldId, out worldInfo);
        ScenarioData.TryGetPageGroup(chapterId, out pages);

    }
}
