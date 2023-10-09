using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelTest2 : MonoBehaviour
{
    void Start()
    {
        DataManager.Load("C:\\Users\\kg881\\Perforce\\wk_program_4958\\");
        
        ScenarioData.TryGetWorld(10001, out ScenarioWorldInfo worldInfo);
        ScenarioData.TryGetChapter(10001, out ScenarioChapterInfo chapterInfo);
        ScenarioData.TryGetPage(1000001, out ScenarioPageInfo pageInfo);
        ScenarioData.TryGetPageGroup(10001, out List<ScenarioPageInfo> pageGroupInfo);
        ScenarioData.TryGetPageText(1000001, out List<ScenarioPageTextInfo> pageTextInfo);
        ScenarioData.TryGetSelectGroup(1000010, out List<ScenarioSelectInfo> selectInfo);
        ScenarioData.TryGetDice(10001, out ScenarioDiceInfo diceInfo);
        ScenarioData.TryGetChapterReward(10000001, out ScenarioChapterRewardInfo rewardInfo);
        ScenarioData.TryGetEnding(10011, out ScenarioEndingInfo endingInfo);

        Debug.Log("---World----");
        Debug.Log(worldInfo);
        Debug.Log("---Chapter----");
        Debug.Log(chapterInfo);
        Debug.Log("---Page----");
        Debug.Log(pageInfo);
        Debug.Log("---PageText----");
        foreach (var x in pageTextInfo)
        {
            Debug.Log(x.Order);
            Debug.Log(x);
        }
        Debug.Log("---Select----");
        foreach (var x in selectInfo)
        {
            Debug.Log(x.GroupId);
            Debug.Log(x);
        }
        Debug.Log("---Dice----");
        Debug.Log(diceInfo); 
        Debug.Log("---ChapterReward----");
        Debug.Log(rewardInfo);
        Debug.Log("---Ending----");
        Debug.Log(endingInfo); 
      
    }  
}
