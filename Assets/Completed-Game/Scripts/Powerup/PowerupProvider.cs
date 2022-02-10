using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupProvider : MonoBehaviour
{

    [SerializeField] private int deleteAfterHits = 1;
    private int hitCount = 0;
    
    [SerializeField] private Powerup powerup;

    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        // Check if the hit object has a power-up listener component
        PowerupListener other = collider.transform.GetComponent<PowerupListener>();
        Debug.Log(collider.gameObject.name);
        audioData.Play(0);
        if (other != null)
        {            
            hitCount++;
            powerup.Grant();
            if (hitCount >= deleteAfterHits) gameObject.SetActive(false);
        }
    }

    public void Reset() {
        gameObject.SetActive(true);
        hitCount = 0;
    }
}
