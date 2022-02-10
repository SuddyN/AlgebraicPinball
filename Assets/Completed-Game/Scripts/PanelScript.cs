using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
    private bool triggered = false;
    [SerializeField] private Light light;
    [SerializeField] private float lightupDuration = 10;

    private Material material;
    private IEnumerator lightCoroutine;

    public bool IsActive => triggered;

    AudioSource audioData;

    void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
        audioData = GetComponent<AudioSource>();
        Reset();
    }

    public void Reset()
    {
        material.color = Color.white;
        triggered = false;
        light.intensity = 0;
        if (lightCoroutine != null)
        {
            StopCoroutine(lightCoroutine);
            lightCoroutine = null;
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (!triggered)
        {
            Activate();
            PinballGame.Get().AddScore(10);
        }
    }

    private void Activate()
    {
        material.color = Color.green;
        triggered = true;
        light.intensity = 2;
        lightCoroutine = Utility.DelayedFunction(this, lightupDuration, Deactivate);
        audioData.Play(0);

    }

    private void Deactivate()
    {
        material.color = Color.white;
        triggered = false;
        light.intensity = 0;
        lightCoroutine = null;
    }
}
