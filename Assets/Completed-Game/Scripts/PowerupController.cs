using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{

    public enum PowerUpType {
        DoubleScore, AddLife
    }

    public int deleteAfterHits;
    public int hitCount = 0;
    public PowerUpType powerupType;
    private GameObject pinballTable;
    private PinballGame pinballGame;

    // Start is called before the first frame update
    void Start()
    {
        pinballTable = GameObject.Find("Pinball Table");
        pinballGame = pinballTable.GetComponent<PinballGame>();
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
            else if (powerupType == PowerUpType.AddLife)
                AddLife();
            if (hitCount >= deleteAfterHits) {
                DeletePowerup();
            }
        }
    }

    private void DoubleScore() {
        pinballGame.score *= 2;
    }

    private void AddLife() {
        pinballGame.ballsLeft++;
    }

    public void DeletePowerup() {
        // Don't display or collide with this object
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void ResetPowerup() {
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        hitCount = 0;
    }
}
