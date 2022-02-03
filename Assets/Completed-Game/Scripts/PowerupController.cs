using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{

    public enum PowerUpType {
        DoubleScore, AddBall, Slowdown
    }

    public int deleteAfterHits;
    private int hitCount = 0;
    public PowerUpType powerupType;
    private GameObject pinballTable;
    private PinballGame pinballGame;

    // Start is called before the first frame update
    void Start()
    {
        pinballTable = GameObject.Find("Pinball Table");
        pinballGame = pinballTable?.GetComponent<PinballGame>();
        if (hitCount >= deleteAfterHits) {
            DeletePowerup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Ball") {
            hitCount++;
            if (powerupType == PowerUpType.DoubleScore)
                DoubleScore();
            if (powerupType == PowerUpType.AddBall)
                AddBall();
            if (powerupType == PowerUpType.Slowdown)
                Slowdown();
            if (hitCount >= deleteAfterHits) {
                DeletePowerup();
            }
        }
    }

    private void DoubleScore() {
        pinballGame.score = pinballGame.score * 2;
    }

    private void AddBall() {
        
    }

    private void Slowdown() {
    
    }

    private void DeletePowerup() {
        // Don't display or collide with this object
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
