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

    public class BattleInfo : MonoBehaviour
    {
        //public Image uiIcon;
        //public TextMeshProUGUI uiName; //ĳ�����̸�
        //public Slider uiHPbar; //ü�¹�
        //public Slider uiStamina; //�Ƿε�




    }
}