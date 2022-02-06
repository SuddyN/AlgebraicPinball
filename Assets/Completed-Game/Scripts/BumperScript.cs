using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{

    public int scoreIncrement = 100;
    public AudioSource bumperSound;
    public Material bumperOff;
    public Material bumperOn;
    private MeshRenderer meshRenderer;
    public float bumperForce;
    bool bHitLight = false;
    float hitLightTimer = 0;
    public Light light;

    private void Start() {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        bumperSound = GetComponent<AudioSource>();
    }

    private void Update() {
        // assign material depending on whether bumper hit or not
        Material[] materials = meshRenderer.materials;
        if ((bHitLight) && (hitLightTimer < 500)) {
            materials[2] = bumperOn;
            hitLightTimer++;
            light.intensity = 1.5f;
        } else {
            materials[2] = bumperOff;
            bHitLight = false;
            light.intensity = 0.25f;
        };
        meshRenderer.materials = materials;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bumper Hit");
        //trigger hit light (change material assigned to bumper object, so bumper "lights up"), reset hitlight timer 
        bHitLight = true;
        hitLightTimer = 0;
        Vector3 diff = collision.gameObject.transform.position - this.gameObject.transform.position;
        diff.y = 0;
        collision.gameObject.GetComponent<Rigidbody>().AddForce(diff.normalized * bumperForce);
        PinballGame.Get().AddScore(scoreIncrement);
    }
}
