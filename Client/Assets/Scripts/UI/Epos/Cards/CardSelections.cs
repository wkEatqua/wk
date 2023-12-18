using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Epos.UI
{
    public class CardSelections : UI_Base
    {
        enum Texts
        {
            Title,Story,
        }

        enum Buttons
        {
            Accept,Close
        }

        EposCardEventInfo info;
        TextMeshProUGUI title;
        TextMeshProUGUI story;
        CardUI parent;
        Button accept;
        Button close;
        private void Awake()
        {
            Bind<TextMeshProUGUI>(typeof(Texts));
            Bind<Button>(typeof(Buttons));
            title = Get<TextMeshProUGUI>((int)Texts.Title);
            story = Get<TextMeshProUGUI>((int)Texts.Story);
            accept = Get<Button>((int)Buttons.Accept);
            close = Get<Button>((int)Buttons.Close);

            accept.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
        }

        public void Init(CardUI cardUI)
        {
            info = cardUI.cardEventInfo;
            parent = cardUI;
            ScriptData.TryGetGameText(info.BgTitle, out EposGameTextInfo text);
            title.text = text.Kor;
            ScriptData.TryGetGameText(info.BgText, out text);

            story.text = text.Kor;
        }
    }
}