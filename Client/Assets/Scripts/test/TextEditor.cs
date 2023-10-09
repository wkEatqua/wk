using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Behaviors
{
    public string BTname = null;
    public string BTcommand = null;
    public bool BTstrength = false;
    public bool BTspeed = false;
    public bool BTwavesize = false;
    public bool BTdelay = false;
}


public class TextEditor : MonoBehaviour
{
    public List<Behaviors> LtypeOfBehavior = new List<Behaviors>();
    string[] options = { "a", "f", "w", "d" };

    public Behaviors selectedType = new Behaviors();
    int strength;//amplitude -a
    int speed; //frequency -f
    int wavesize; //w
    int delay; //d

    public TMP_Dropdown dropdownBehaviors;
    public Slider Sstrength;
    public Slider Sspeed;
    public Slider SwaveSize;
    public Slider Sdelay;

    public TMP_InputField inputArea;
    public TMP_InputField copyArea;
    public TextMeshProUGUI showArea;

    public void Start()
    {
        setTypeOfBehaviors();

        dropdownBehaviors.options.Clear();
        for (int i = 0; i < LtypeOfBehavior.Count; i++)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = LtypeOfBehavior[i].BTname;
            dropdownBehaviors.options.Add(option);
        }

        //default select
        selectedType = LtypeOfBehavior[0];
        OnValueChanged();
    }

    // '>' button
    public void OnClick()
    {
        copyArea.text = string.Empty;
        showArea.text = string.Empty;


        copyArea.text = "<" + selectedType.BTcommand;

        if (selectedType.BTstrength && Sstrength.value!=0)      { copyArea.text += " a=" + (int)Sstrength.value; }
        if (selectedType.BTspeed && Sspeed.value != 0)          { copyArea.text += " f=" + (int)Sspeed.value; }
        if (selectedType.BTwavesize && SwaveSize.value != 0)    { copyArea.text += " w=" + (int)SwaveSize.value; }
        if (selectedType.BTdelay && Sdelay.value != 0)          { copyArea.text += " d=" + (int)Sdelay.value; }

        copyArea.text += ">";

        copyArea.text += inputArea.text;

        copyArea.text += "</" + selectedType.BTcommand + ">";


        showArea.text = copyArea.text;
    }

    public void OnValueChanged()
    {
        selectedType = LtypeOfBehavior[dropdownBehaviors.value];
        Sstrength.interactable = selectedType.BTstrength;
        Sspeed.interactable = selectedType.BTspeed;
        SwaveSize.interactable = selectedType.BTwavesize;
        Sdelay.interactable = selectedType.BTdelay;

        Sstrength.value = 0;
        Sspeed.value = 0;
        SwaveSize.value = 0;
        Sdelay.value = 0;
    }

    public void setTypeOfBehaviors()
    {

        LtypeOfBehavior.Add(setBehavior("진자운동", "pend", true, true, true, false));
        LtypeOfBehavior.Add(setBehavior("매달림", "dangle", true, true, true, false));
        LtypeOfBehavior.Add(setBehavior("사라짐", "fade", false,false,false, true));
        LtypeOfBehavior.Add(setBehavior("레인보우", "rainb", false, true, true, false));
        LtypeOfBehavior.Add(setBehavior("회전", "rot", false, true, true, false));
        LtypeOfBehavior.Add(setBehavior("통통", "bounce", true, true, true, false));
        LtypeOfBehavior.Add(setBehavior("미끄럼틀", "slide", true, true, true, false));
        LtypeOfBehavior.Add(setBehavior("그네","swing", true, true, true, false));
        LtypeOfBehavior.Add(setBehavior("파도", "wave", true, true, true, false));
        LtypeOfBehavior.Add(setBehavior("띠요옹", "incr", true, true, true, false));
        LtypeOfBehavior.Add(setBehavior("지진", "shake", true, false,false, true));
        LtypeOfBehavior.Add(setBehavior("울렁울렁", "wiggle", true, true, false,false));

    }


    public Behaviors setBehavior(string name,string command, bool strength, bool speed, bool waveSize, bool delay)
    {
        Behaviors tempBehaviorType = new Behaviors();

        tempBehaviorType.BTname = name;
        tempBehaviorType.BTcommand = command;
        tempBehaviorType.BTstrength = strength;
        tempBehaviorType.BTspeed = speed;
        tempBehaviorType.BTwavesize = waveSize;
        tempBehaviorType.BTdelay = delay;

        return tempBehaviorType;
    }


}
