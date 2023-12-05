using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelTest : MonoBehaviour
{
    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(model);
        }
    }
}
