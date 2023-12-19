using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Epos;
using System.Text;

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
        AdditionalSightText
    }

    Player player;

    protected override void Init()
    {
        base.Init();
        player = FindAnyObjectByType<Player>();
        if (player)
        {
            player.OnStatChange.AddListener(ChangeStatTexts);
        }
    }

    protected override void BindUI()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    protected void ChangeStatTexts()
    {
        // Base Stat
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
