using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject scoreLabel;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject ballsLabel;
    [SerializeField] private GameObject ballsText;
    [SerializeField] private GameObject help;
    [SerializeField] private GameObject highScoreLabel;
    [SerializeField] private GameObject highScoreText;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject image1;
    [SerializeField] private GameObject image2;

    // Start is called before the first frame update
    void Start()
    {
        scoreLabel.SetActive(false);
        scoreText.SetActive(false);
        ballsLabel.SetActive(false);
        ballsText.SetActive(false);
        help.SetActive(false);
        highScoreLabel.SetActive(false);
        highScoreText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        scoreLabel.SetActive(true);
        scoreText.SetActive(true);
        ballsLabel.SetActive(true);
        ballsText.SetActive(true);
        help.SetActive(true);
        highScoreLabel.SetActive(true);
        highScoreText.SetActive(true);

        title.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);

        image1.SetActive(false);
        image2.SetActive(false);

        camera.transform.position += new Vector3(70, 0, 0);
    }

    public void QuitGame() {
        Utility.QuitGame();
    }
}
