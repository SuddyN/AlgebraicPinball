using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

public class PinballGame : MonoBehaviour
{

    public Text scoreText;
    public Text highScoreText;
    public Text winText;
    public Text ballsText;

    [SerializeField] private QuestionDisplay questionDisplay;
    public QuestionData Level1Question1; // TODO: Remove
    public QuestionData Level1Question2; // TODO: Remove
    public QuestionData Level1Question3; // TODO: Remove
    public QuestionData Level1Question4; // TODO: Remove
    public QuestionData Level1Question5; // TODO: Remove
    public QuestionData Level2Question1; // TODO: Remove
    public QuestionData Level2Question2; // TODO: Remove
    public QuestionData Level2Question3; // TODO: Remove
    public QuestionData Level2Question4; // TODO: Remove
    public QuestionData Level2Question5; // TODO: Remove
    public QuestionData Level3Question1; // TODO: Remove
    public QuestionData Level3Question2; // TODO: Remove
    public QuestionData Level3Question3; // TODO: Remove
    public QuestionData Level3Question4; // TODO: Remove
    public QuestionData Level3Question5; // TODO: Remove
    public QuestionData Level4Question1; // TODO: Remove
    public QuestionData Level4Question2; // TODO: Remove
    public QuestionData Level4Question3; // TODO: Remove
    public QuestionData Level4Question4; // TODO: Remove
    public QuestionData Level4Question5; // TODO: Remove
    public QuestionData Level5Question1; // TODO: Remove
    public QuestionData Level5Question2; // TODO: Remove
    public QuestionData Level5Question3; // TODO: Remove
    public QuestionData Level5Question4; // TODO: Remove
    public QuestionData Level5Question5; // TODO: Remove
    public QuestionData Level6Question1; // TODO: Remove
    public QuestionData Level6Question2; // TODO: Remove
    public QuestionData Level6Question3; // TODO: Remove
    public QuestionData Level6Question4; // TODO: Remove
    public QuestionData Level6Question5; // TODO: Remove
    public QuestionData Level7Question1; // TODO: Remove
    public QuestionData Level7Question2; // TODO: Remove
    public QuestionData Level7Question3; // TODO: Remove
    public QuestionData Level7Question4; // TODO: Remove
    public QuestionData Level7Question5; // TODO: Remove

    [SerializeField] private int maxBalls = 3;
    [SerializeField] private int score = 0;

    private float addPointsMultiplier = 1.0f;
    
    private int highscore = 0;

    public float plungerSpeed = 100;

    public AudioSource audioPlayer;
    public AudioClip plungerClip;
    public AudioClip soundtrackClip;
    public AudioClip gameoverClip;

    public KeyCode newGameKey;
    public KeyCode plungerKey;

    public KeyCode askQuestionKey; // TODO: Remove

    public int ballsLeft = 3;
    private bool gameOver = false;
    private GameObject ball;
    private GameObject plunger;
    private GameObject drain;

    private static PinballGame instance;

    private int level = 1;

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public static PinballGame Get() => instance;

    
    void Start()
    {
        plunger = GameObject.Find("Plunger");
        drain = GameObject.Find("Drain");
        ball = GameObject.Find("Ball");

        ball.SetActive(false);

        audioPlayer = GetComponent<AudioSource>();

        audioPlayer.loop = true;
        audioPlayer.clip = soundtrackClip;
        audioPlayer.volume = 0.3f;
        audioPlayer.Play(); 
    }

    private void Update()
    {

        if (Input.GetKey(newGameKey) == true) NewGame();
        if (Input.GetKey(plungerKey) == true) Plunger();

        // detect ball going past flippers into "drain"
        if ((ball.activeSelf == true) && (ball.transform.position.z < drain.transform.position.z))
        {
            ball.SetActive(false);
            int rand = Random.Range(1, 5);
            if (level == 1) {
                if (rand == 1) {
                    AskQuestion(Level1Question1);
                } else if (rand == 2) {
                    AskQuestion(Level1Question2);
                } else if (rand == 3) {
                    AskQuestion(Level1Question3);
                } else if (rand == 4) {
                    AskQuestion(Level1Question4);
                } else if (rand == 5) {
                    AskQuestion(Level1Question5);
                } 
            } else if (level == 2) {
                if (rand == 1) {
                    AskQuestion(Level2Question1);
                } else if (rand == 2) {
                    AskQuestion(Level2Question2);
                } else if (rand == 3) {
                    AskQuestion(Level2Question3);
                } else if (rand == 4) {
                    AskQuestion(Level2Question4);
                } else if (rand == 5) {
                    AskQuestion(Level2Question5);
                } 
            } else if (level == 3) {
                if (rand == 1) {
                    AskQuestion(Level3Question1);
                } else if (rand == 2) {
                    AskQuestion(Level3Question2);
                } else if (rand == 3) {
                    AskQuestion(Level3Question3);
                } else if (rand == 4) {
                    AskQuestion(Level3Question4);
                } else if (rand == 5) {
                    AskQuestion(Level3Question5);
                } 
            } else if (level == 4) {
                if (rand == 1) {
                    AskQuestion(Level4Question1);
                } else if (rand == 2) {
                    AskQuestion(Level4Question2);
                } else if (rand == 3) {
                    AskQuestion(Level4Question3);
                } else if (rand == 4) {
                    AskQuestion(Level4Question4);
                } else if (rand == 5) {
                    AskQuestion(Level4Question5);
                } 
            } else if (level == 5) {
                if (rand == 1) {
                    AskQuestion(Level5Question1);
                } else if (rand == 2) {
                    AskQuestion(Level5Question2);
                } else if (rand == 3) {
                    AskQuestion(Level5Question3);
                } else if (rand == 4) {
                    AskQuestion(Level5Question4);
                } else if (rand == 5) {
                    AskQuestion(Level5Question5);
                } 
            } else if (level == 6) {
                if (rand == 1) {
                    AskQuestion(Level6Question1);
                } else if (rand == 2) {
                    AskQuestion(Level6Question2);
                } else if (rand == 3) {
                    AskQuestion(Level6Question3);
                } else if (rand == 4) {
                    AskQuestion(Level6Question4);
                } else if (rand == 5) {
                    AskQuestion(Level6Question5);
                } 
            } else if (level >= 7) {
                if (rand == 1) {
                    AskQuestion(Level7Question1);
                } else if (rand == 2) {
                    AskQuestion(Level7Question2);
                } else if (rand == 3) {
                    AskQuestion(Level7Question3);
                } else if (rand == 4) {
                    AskQuestion(Level7Question4);
                } else if (rand == 5) {
                    AskQuestion(Level7Question5);
                } 
            }
            level++;
        }

        if (ball.transform.position.y > 2)
        {
            ball.transform.position = new Vector3(0, -1, 0);
        }

        if ((ball.activeSelf == false) && (ballsLeft == 0))
        {
            if (gameOver == false)
            {
                gameOver = true;
                audioPlayer.PlayOneShot(gameoverClip);
            }
        }

        SetText();
    }

    // Each physics step..
    void FixedUpdate()
    {

    }

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    void SetText()
    {
        // Update the text field of our 'countText' variable
        scoreText.text = score.ToString();

        ballsText.text = ballsLeft.ToString();

        // Check if our 'count' is equal to or exceeded 12
        if (gameOver) winText.text = "Game Over";
        else if (score == 5000) winText.text = "Superstar!";
        else if (score >= 100000) winText.text = "You won!";
        else winText.text = "";

        if (score > highscore) highscore = score;
        highScoreText.text = highscore.ToString();
    }

    void NewGame()
    {
        ballsLeft = 3;
        gameOver = false;
        ball.SetActive(false);
        ResetScore();

        GameObject[] bumpers;
        bumpers = GameObject.FindGameObjectsWithTag("Bumper");

        foreach (GameObject bumper in bumpers)
        {
            bumper.GetComponent<MeshRenderer>().enabled = true;
            bumper.GetComponent<BoxCollider>().enabled = true;
        }

        GameObject[] powerups;
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject powerup in powerups) {
            powerup.GetComponent<PowerupProvider>().Reset();
        }
    }

    void Plunger()
    {
        if ((ballsLeft > 0) && (ball.activeSelf == false))
        {
            ball.SetActive(true);

            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            Vector3 movement = new Vector3(0.0f, 0.0f, 1.0f);
            rb.AddForce(movement * plungerSpeed);

            // set ball position to location of plunger
            ball.transform.position = plunger.transform.position;
            ballsLeft = ballsLeft - 1;

            audioPlayer.PlayOneShot(plungerClip);
        }
    }

    public void AskQuestion(QuestionData question)
    {
        questionDisplay.Display(question);
    }

    public void AddScore(int points)
    {
        score += (int)(points * addPointsMultiplier);
    }

    public void MultiplyScore(float multiplier)
    {
        score = (int)(multiplier * score);
    }

    public void SetPointsMultiplier(float multiplier)
    {
        addPointsMultiplier = multiplier;
    }

    public int Score => score;

    public void ResetScore()
    {
        score = 0;
    }

    public void AddLife()
    {
        ballsLeft += ballsLeft < maxBalls ? 1 : 0;
    }

}


