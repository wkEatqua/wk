using Febucci.UI.Effects;
using NPOI.POIFS.Properties;
using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelection : MonoBehaviour
{
    Button button;
    
    float rotation;

    [HideInInspector] public bool selected = false;
   
    public TextMeshProUGUI titleText;

    public long worldId;
    ScenarioWorldInfo worldInfo;
    

    void Start()
    {
        ScenarioData.TryGetWorld(worldId, out worldInfo);
        button = GetComponent<Button>();
        selected = false;
        button.onClick.AddListener(() => { StartCoroutine(MoveToSelection()); });
        button.onClick.AddListener(() =>
        {
            if (selected)
            {
                LibraryLobbyManager.Instance.OpenChaterList(worldId);
            }
        });
        rotation = transform.localEulerAngles.z;
        if (rotation > 180)
        {
            rotation -= 360;
        }
    }      
    
    IEnumerator MoveToSelection()
    {
        if (selected) yield break;
        if (!LibraryLobbyManager.Instance.IsMoving)
        {
            LibraryLobbyManager.Instance.IsMoving = true;
            float z = transform.parent.localEulerAngles.z;
            if (z > 180) z -= 360;
            z = -z;
           
            float myZ = rotation;

            float difference = Mathf.Abs(myZ - z);

            float time = 0.25f * difference / 25;
            float startTime = Time.time;
           
            Quaternion start = transform.parent.localRotation;
            
            Quaternion last = transform.localRotation;
            last.z = -last.z;
            while (Time.time < startTime + time)
            {                
                transform.parent.localRotation = Quaternion.Slerp(start, last, (Time.time - startTime) / time);
                yield return new WaitForEndOfFrame();
            }

            transform.parent.localRotation = last;          
            LibraryLobbyManager.Instance.IsMoving = false;
            Select();
        }
    }

    public void Select()
    {
        if(worldInfo == null)
        {
            ScenarioData.TryGetWorld(worldId, out worldInfo);
        }
        LibraryLobbyManager.Instance.worldId = worldId;
        selected = true;
        transform.SetAsLastSibling();
        if(worldInfo != null)
        {
            titleText.text = worldInfo.Name;
        }
    }
}
