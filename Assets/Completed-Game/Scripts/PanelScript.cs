using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
    int triggered = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (triggered != 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            triggered--;
        } else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (triggered == 0)
        {
            triggered = 1000;
            GameObject.Find("Pinball Table").GetComponent<PinballGame>().score = GameObject.Find("Pinball Table").GetComponent<PinballGame>().score + 10;
        }
    }
}
