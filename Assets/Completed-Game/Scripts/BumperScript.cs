using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{

    [SerializeField] private int scoreIncrement = 100;
    [SerializeField] private AudioSource bumperSound;
    [SerializeField] private Material bumperOff;
    [SerializeField] private Material bumperOn;
    [SerializeField] private float bumperForce;
    [SerializeField] private float hitLightTime = 1.0f;
    
    [SerializeField] private Light bumperLight;
    
    private static int lightMaterialSlot = 2;
    
    private MeshRenderer _meshRenderer = null;
    private bool _isHit = false;
    private IEnumerator _lightCoroutine = null;

    private void Start() {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        bumperSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bumper Hit");
        //trigger hit light or reset hit light timer 
        if (_isHit) ResetLightTimer();
        else ActivateLight();
        
        // Bounce the ball
        Vector3 diff = collision.gameObject.transform.position - this.gameObject.transform.position;
        diff.y = 0;
        collision.gameObject.GetComponent<Rigidbody>().AddForce(diff.normalized * bumperForce);
        PinballGame.Get().AddScore(scoreIncrement);
    }

    private void ActivateLight()
    {
        Material[] materials = _meshRenderer.materials;
        materials[lightMaterialSlot] = bumperOn;
        bumperLight.intensity = 1.5f;
        _meshRenderer.materials = materials;
        _lightCoroutine = Utility.DelayedFunction(this, hitLightTime, DeactivateLight);
    }

    private void ResetLightTimer()
    {
        StopCoroutine(_lightCoroutine);
        _lightCoroutine = Utility.DelayedFunction(this, hitLightTime, DeactivateLight);
    }

    private void DeactivateLight()
    {
        Material[] materials = _meshRenderer.materials;
        materials[lightMaterialSlot] = bumperOff;
        bumperLight.intensity = 0.0f;
        _meshRenderer.materials = materials;
        _lightCoroutine = null;
    }
}
