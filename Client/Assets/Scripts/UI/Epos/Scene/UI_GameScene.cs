using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Epos;
using System.Text;
using Epos.Mission;
using Shared.Data;
using Shared.Model;

public class UI_GameScene : UI_Scene
{
    enum GameObjects
    {

    }

    enum Texts
    {
        HPText,
        AdditionalHPText,
        AttackText,
        AdditionalAttackText,
        DefenceText,
        AdditionalDefenceText,
        SightText,
        AdditionalSightText,
        TotalTurnText,
        MovedTileText,
        TotalItemText,
        LevelText,
        ExpText,
        TurnText,
        ConditionText,
        RewardText,
        FailText,
    }

    Player player;

    protected override void Init()
    {
        base.Init();
        player = FindAnyObjectByType<Player>();
        if (player)
        {
            player.OnStatChange.AddListener(UpdateStatTexts);
        }

        TurnManager.Instance.OnEnemyTurnEnd += UpdateGameInfoTexts;
        UpdateStatTexts();
        MissionManager.Instance.OnMissionStart.AddListener(UpdateMissionTexts);
        MissionManager.Instance.OnMissionGiven.AddListener(UpdateMissionTexts);
        MissionManager.Instance.OnMissionComplete.AddListener(UpdateMissionTexts);
        MissionManager.Instance.OnMissionFail.AddListener(UpdateMissionTexts);
    }

    protected override void BindUI()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    public override void Refresh()
    {
        UpdateStatTexts();
        UpdateExpTexts();
        UpdateMissionTexts();
    }

    protected void UpdateMissionTexts()
    {
        // 잔여 턴
        GetText((int)Texts.TurnText).text = MissionManager.Instance.subject.curTurn.ToString();

        // 조건
        GetText((int)Texts.ConditionText).text = MissionManager.Instance.subject.data.Desc;

        // 보상
        var rewardInfo = MissionManager.Instance.reward.info;
        string rewardName = "";
        switch(rewardInfo.RewardType)
        {
            case RewardType.Item:
                ItemData.TryGetItemInfo(rewardInfo.RewardID, out var itemInfo);
                rewardName = itemInfo.Name;
                break;
            case RewardType.Stat:
                StringBuilder stb = new StringBuilder();
                stb.Append(rewardInfo.Desc);
                stb.Append(" ");
                stb.Append(MissionManager.Instance.reward.value.ToString());
                stb.Append(rewardInfo.AddType == AddType.Value ? "+" : "%");
                rewardName = stb.ToString();
                break;
        }
        GetText((int)Texts.RewardText).text = rewardName;

        // 실패
        var penaltyInfo = MissionManager.Instance.penalty.info;
        string penaltyName = "";
        StringBuilder sb = new StringBuilder();
        switch (penaltyInfo.Type)
        {
            case PenaltyType.Stat:
                {
                    sb.Append(penaltyInfo.Desc);
                }
                break;
            case PenaltyType.HP:
                {
                    sb.Append(PenaltyType.HP.ToString());
                }
                break;
        }
        sb.Append(" ");
        sb.Append(MissionManager.Instance.penalty.value.ToString());
        sb.Append(penaltyInfo.AddType == AddType.Value ? "-" : "%");
        penaltyName = sb.ToString();
        GetText((int)Texts.FailText).text = penaltyName;
    }

    protected IEnumerator UpdateGameInfoTexts()
    {
        GetText((int)Texts.MovedTileText).text = EposManager.Instance.moveCount.ToString();
        GetText((int)Texts.TotalTurnText).text = TurnManager.Instance.TurnCount.ToString();
        yield return null;
    }

    protected void UpdateExpTexts()
    {
        GetText((int)Texts.LevelText).text = EposManager.Instance.level.ToString();

        int exp = EposManager.Instance.Exp;
        int maxExp = EposManager.Instance.MaxExp;
        float progress = 0;
        if (maxExp != 0)
        {
            progress = (float)exp / maxExp;
        }

        GetText((int)Texts.ExpText).text = Mathf.Round(progress).ToString();
    }

    protected void UpdateStatTexts()
    {
        Debug.Log("update");
        // Base Stat
        if (player == null)
        {
            Debug.Log("Player is null");
            var playerInScene = FindAnyObjectByType<Player>();
            if (playerInScene == null)
            {
                Debug.Log("Player In Scene is null");
                return;
            }
            
            player = playerInScene;
            player.OnStatChange.AddListener(UpdateStatTexts);
        }

        var baseStat = player.BaseStat;

        baseStat.stats.TryGetValue(Shared.Model.ActorStatType.MaxHp, out int maxHP);
        GetText((int)Texts.HPText).text = maxHP.ToString();

        baseStat.stats.TryGetValue(Shared.Model.ActorStatType.Atk, out int attack);
        GetText((int)Texts.AttackText).text = attack.ToString();

        baseStat.stats.TryGetValue(Shared.Model.ActorStatType.Def, out int defence);
        GetText((int)Texts.DefenceText).text = defence.ToString();

        baseStat.stats.TryGetValue(Shared.Model.ActorStatType.Sight, out int sight);
        GetText((int)Texts.SightText).text = sight.ToString();

        // Bonus Stat
        StringBuilder sb = new StringBuilder();
        var bonusHPText = GetText((int)Texts.AdditionalHPText);
        bonusHPText.gameObject.SetActive(true);
        int bonusHP = player.BonusStat(Shared.Model.ActorStatType.MaxHp);
        if (bonusHP > 0)
            sb.Append("+");
        else if (bonusHP < 0)
            sb.Append("-");
        else
            bonusHPText.gameObject.SetActive(false);    

        sb.Append(bonusHP.ToString());
        bonusHPText.text = sb.ToString();


        sb = new StringBuilder();
        var bonusAttackText = GetText((int)Texts.AdditionalAttackText);
        bonusAttackText.gameObject.SetActive(true);
        int bonusAttack = player.BonusStat(Shared.Model.ActorStatType.Atk);
        if (bonusAttack > 0)
            sb.Append("+");
        else if (bonusAttack < 0)
            sb.Append("-");
        else
            bonusAttackText.gameObject.SetActive(false);

        sb.Append(bonusAttack.ToString());
        bonusAttackText.text = sb.ToString();

        sb = new StringBuilder();
        var bonusDefenceText = GetText((int)Texts.AdditionalDefenceText);
        bonusDefenceText.gameObject.SetActive(true);
        int bonusDefence = player.BonusStat(Shared.Model.ActorStatType.Def);
        if (bonusDefence > 0)
            sb.Append("+");
        else if (bonusDefence < 0)
            sb.Append("-");
        else
            bonusDefenceText.gameObject.SetActive(false);

        sb.Append(bonusDefence.ToString());
        bonusDefenceText.text = sb.ToString();

        sb = new StringBuilder();
        var bonusSightText = GetText((int)Texts.AdditionalSightText);
        bonusSightText.gameObject.SetActive(true);
        int bonusSight = player.BonusStat(Shared.Model.ActorStatType.Sight);
        if (bonusSight > 0)
            sb.Append("+");
        else if (bonusSight < 0)
            sb.Append("-");
        else
            bonusSightText.gameObject.SetActive(false);

        sb.Append(bonusSight.ToString());
        bonusSightText.text = sb.ToString();
    }
}
