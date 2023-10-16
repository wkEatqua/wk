using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WK.Battle
{

    public class EnemyBattleInfo : MonoBehaviour
    {
        //UI
        public Image uiIcon;
        public TextMeshProUGUI uiName; //ĳ�����̸�
        public Slider uiHPbar; //ü�¹�
        public Slider uiStamina; //�Ƿε�

        public DiceSlots[] uiDiceSlots; //�ֻ�������
        public TextMeshProUGUI uiATK;   //���ݷ�
        public TextMeshProUGUI uiCRI;   //ġ��Ÿ��


        public DiceSlots[] uiEnemyDiceSlots; //�ֻ�������
        public Skill[] uiSkills;


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
        public  int BaseDiceCount; //���� �ֻ��� ����
        public int MaxDiceCountFix; //�ִ� �ֻ��� ����
        private int[] DiceRate; //�ֻ��� �� ���� Ȯ�� (1~12)

        public Skill[] skills; // - ��ų (��ų�����ּ�/��ų���̵�/�⺻����Ȯ��/Ȯ�����Ҽ�ġ) > ��ų���̺����
        public int[] diceValues;




        public void init()
        {
            // uiEnemyDiceSlots; //�ֻ������� �ʱ�ȭ
        }

        public void initTurn()
        {
            //�� ���ƿö� �ʱ�ȭ ���� ��
        }
    }
}