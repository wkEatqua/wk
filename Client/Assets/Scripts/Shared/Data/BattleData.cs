using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Shared.Model;
using WK.Battle;

namespace Shared.Data
{

    public class BattleData 
    {
       
    }


    public class Global
    {
        public int MaxDiceCount;
        public int BaseHP;
        public int BaseAttack;
        public int BaseDefence;
        public int BaseLuck;
    }

    public class Hero
    {
        public int ID;
        //public NameText Name;
        public string Name;
        public string HeroIcon;
        public WeaponType WeaponType;
        public string MainIllust;
        public int HeroStamina;
        public int NeedTransForm;
        public string model3D;

        public int[] skillID; //6
        public int[] skillProbabilityCost;  //6
        public int[] skillStamina;  //6

        public int StaminaDownSection;

        public float[] StaminaDown; //4
        public float[] StaminaDownRate; //4

    }

    public class BattleMonster
    {
        public int GroupID;
        public int ID;
        public int WorldID;
        public int ChapterID;
        //public Enum GameText;
        public string GameText;
        public string HeroIcon;
        public int DefenceType;
        public string MainIllust;
        public int TransForm;
        public string DiceSkin;
        public string BaseDiceCount;
        public string MaxDiceCountFix;
        public float[] DiceRate; // 12

        public int[] SkillSlotNum; //6
        public int[] SkillID; //6
        public float[] SelectBaseRate; //6
        public float[] UseDownRate; //6

    }





}
