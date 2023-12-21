using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Epos.UI
{
    public class ItemUI : UI_Base
    {
        enum Texts
        {
            Desc,Stat
        }
        enum Images
        {

        }
        enum Buttons
        {
            Main
        }
        TextMeshProUGUI desc;
        TextMeshProUGUI stat;
        Button button;
        bool isInit = false;
        InstantItem item;
        private void Awake()
        {
            isInit = false;
        }
        protected override void Init()
        {
            base.Init();           
        }
        public void Init(InstantItem item)
        {
            this.item = item;
            if (!isInit)
            {
                Bind<TextMeshProUGUI>(typeof(Texts));
                Bind<Button>(typeof(Buttons));
                desc = Get<TextMeshProUGUI>((int)Texts.Desc);
                stat = Get<TextMeshProUGUI>((int)Texts.Stat);
                button = Get<Button>((int)Buttons.Main);

                button.onClick.AddListener(() =>
                {
                    UIPool.Return(gameObject);
                    this.item.Collect();
                    TurnManager.Instance.EndTurn();
                });
            }
            isInit = true;
            stat.text = item.StatDesc + " : " + item.data.BaseStat;
            desc.text = item.Desc;
        }
    }
}