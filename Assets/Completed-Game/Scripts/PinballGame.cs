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
    public QuestionData testQuestion; // TODO: Remove

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
        if (Input.GetKey(askQuestionKey) == true) AskQuestion(testQuestion); // TODO: Remove

        // detect ball going past flippers into "drain"
        if ((ball.activeSelf == true) && (ball.transform.position.z < drain.transform.position.z))
        {
            ball.SetActive(false);
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
        else if (score == 1500) winText.text = "Superstar!";
        else if (score >= 2100) winText.text = "You won!";
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


