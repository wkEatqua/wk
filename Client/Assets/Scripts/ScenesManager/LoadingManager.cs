using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;




public class LoadingScript : MonoBehaviour
{
    public TextMeshProUGUI TLoaingText;
    int count = 0;

    public Transform logo;
    public Slider loadingbar;

    public void Start()
    {
        InvokeRepeating("LoadingTextChangedInfinite", 1.5f,1.5f);
    }

    public void Update()
    {
        logo.transform.Rotate(0,0,-1);
        loadingbar.value += 0.001f;
    }

}
