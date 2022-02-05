using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerupManager : MonoBehaviour
{

    private static PowerupManager instance;
    
    private GameObject pinballTable;
    private PinballGame pinballGame;

    protected void Awake()
    {
        pinballTable = GameObject.Find("Pinball Table");
        pinballGame = pinballTable.GetComponent<PinballGame>();
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }

    public static PowerupManager Get() => instance;
    
    public void DoubleScore() {
        pinballGame.score *= 2;
    }

    public void AddLife() {
        pinballGame.ballsLeft++;
    }
}
