using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    float bumperForce = 10;
 
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hello");
        collision.rigidbody.AddExplosionForce(10f, transform.position, 5f, 0f);
        GameObject.Find("Pinball Table").GetComponent<PinballGame>().score = GameObject.Find("Pinball Table").GetComponent<PinballGame>().score + 20;
    }
}
