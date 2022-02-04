using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
    int triggered = 0;
    public Light light;
    public int lightupTimer = 500;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    void Update()
    {
        if (triggered != 0)
        {
            triggered--;
        } else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            light.intensity = 1;
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (triggered == 0)
        {
            triggered = lightupTimer;
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            light.intensity = 2;
            GameObject.Find("Pinball Table").GetComponent<PinballGame>().score = GameObject.Find("Pinball Table").GetComponent<PinballGame>().score + 10;
        }
    }
}
