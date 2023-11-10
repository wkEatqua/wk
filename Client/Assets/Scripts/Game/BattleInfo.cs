using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WK.Battle
{
    public class Hero // #������
    {
        public int id; //���̵�
        public int Name; //�̸�
        public int HeroIcon; //������
        public WeaponType weaponType; //����Ÿ�� (ENUM)
        public int MainIllust; //�Ϸ���Ʈ
        public int HeroStamina; //
        public int NeedTransForm; //���ż�ġ
        public int model3D;

        //��ų[6]
        public int[] SkillID;
        public int[] SkillProbabilityCost;
        public int[] SkillStamina; //6��

        public int StaminaDownSection; //�Ƿε�����[4]

        public int[] StaminaDown; // �Ƿε��ѷ�[4] 
        public int[] StaminaDownRate; //4�� �Ƿε����Ҽ�ġ

    }


    [Serializable]
    public class Enemy
    {
        //DATA  
        private string GroupID; //�׷� ���̵�
        private string ID; //���̵�
        private string WorldID; //���� ���̵�
        private string ChapterID; //é�� ���̵�
        private string Name; //�̸� �ؽ�Ʈ
        private string HeroIcon; //��� ������
        private DefenceType defenceType; //��Ÿ��
        private string MainIllust; //��� �Ϸ���Ʈ
        private string TransFormValue; //���ż�ġ ���ſ䱸ġ
        private string DiceSkin; //�ֻ�����Ų
        public int BaseDiceCount; //���� �ֻ��� ����
        public int MaxDiceCountFix; //�ִ� �ֻ��� ����
        private int[] DiceRate; //�ֻ��� �� ���� Ȯ�� (1~12)
    }

    [Serializable]
    public class Player
    {
        //DATA
        public string HeroName; //�̸�
        public int MaxHPvalue; // ����
        public int CurHPvalue; // ����ü��
        public int Stamina; //�Ƿε�
    }

    public class BattleInfo : MonoBehaviour
    {
        //public Image uiIcon;
        //public TextMeshProUGUI uiName; //ĳ�����̸�
        //public Slider uiHPbar; //ü�¹�
        //public Slider uiStamina; //�Ƿε�




    }
}