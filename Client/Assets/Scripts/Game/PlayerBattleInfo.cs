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



    public class PlayerBattleInfo : MonoBehaviour
    {
        //UI
        public Image uiIcon;
        public TextMeshProUGUI uiName; //ĳ�����̸�
        public Slider uiHPbar; //ü�¹�
        public Slider uiStamina; //�Ƿε�

        public TextMeshProUGUI uiDEM;   //DEM ������
        public TextMeshProUGUI uiCost; //������

        public TextMeshProUGUI uiATK; //���ݷ� 
        public TextMeshProUGUI uiDEF; //����
        public TextMeshProUGUI uiCRT; //ġ��Ÿ��
        public TextMeshProUGUI uiLUK; //���


        public DiceInventory[] uiDiceSlots; //�ֻ�������
       




        //DATA
        public string HeroName; //�̸�
        public int MaxHPvalue; // ����
        public int CurHPvalue; // ����ü��
        public int Stamina; //�Ƿε�
        public int DEMGuage; //DEM(��)
        public int COSTGuage; //������

        public int HavingDice; //���� �ֻ���
        public int MaxDiceSlot; //�ִ��ֻ���
                            
        public string ATK; //���ݷ�
        public string DEF; //����
        public string CRT; //ġ��Ÿ��
        public string LUK; //���

        public Hero hero;
        public Skill[] Skills;
        public int[] diceValues;

        public void init()
        {
            //uiPlayerDiceSlots �ʱ�ȭ
        }

        public void initTurn()
        {
            //�� ���ƿö� �ʱ�ȭ ���� ��
        }
    }
}