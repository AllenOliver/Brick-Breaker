using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int lives = 3;
    public int bricks = 20;
    public int score = 0;
    public int NumberOfAliens;
    public float resetDelay = 1f;
    public Text livesText;
    public Text scoreText;
    public GameObject gameOver;
    public GameObject youWon;
    public GameObject bricksPrefab;
    public GameObject ballPrefab;
    public GameObject paddle;
    public GameObject alienPrefab;
    public GameObject brickParticle;
    public GameObject loadingScreen;
    public GameObject Pause;
    public Transform paddleStart;
    public Transform ballStart;
    public Ball ball;
    public AudioClip level_Start;
    [Header("If true aliens win, if false bricks do")]
    public bool BricksOrKills;
   // public GameObject deathParticles;
    public static GameManager instance = null;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        ball = FindObjectOfType<Ball>();


    }

    void Start()
    {

        Setup();

    }

    void Update()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text =score.ToString();
        PauseGame();
        CheckGameOver();
    }

    IEnumerator playLevelStart()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = level_Start;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        Racket_New.instance.canMove = true;
       // Setup();

    }

    public void Setup()
    {
        StartCoroutine(setUpBallAndPaddle());
        livesText.text = "Lives: " + lives;
        score = 0;
        StartCoroutine(playLevelStart());
    }
    
    public void OneUp()
    {
        switch(score)
        {
            case (10000):
                lives++;
                break;
        }
    }

    public void DestroyBrick()
    {
        bricks--;
        //Instantiate(brickParticle, bricksPrefab.transform.position, Quaternion.identity);
        
    }

    public void PauseGame()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(Time.timeScale == 1)
            {
                Racket_New.instance.canMove = false;
                Time.timeScale = 0;
                Pause.SetActive(true);
            }
            else
            {
                Racket_New.instance.canMove = true;
                Time.timeScale = 1;
                Pause.SetActive(false);
            }
        }
    }


    //void Reset()
    //{
    //    int c = SceneManager.GetActiveScene().buildIndex;
    //    SceneManager.LoadScene(c, LoadSceneMode.Single);
    //}

    IEnumerator setUpBallAndPaddle()
    {
        Debug.Log("enum");
        SetUpBall();
        SetUpPaddle();
        yield return new WaitForSeconds(2f);


    }

    IEnumerator WaitForButtonDownWin()
    {
        while (!Input.GetButtonDown("Submit"))
            yield return null;
        if (Input.GetButtonDown("Submit"))
        {
            loadingScreen.SetActive(true);
            if (SceneManager.GetActiveScene().buildIndex <= 14)
            {
                loadingScreen.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                loadingScreen.SetActive(true);
                SceneManager.LoadScene("Title");
            }
        }
    }

    IEnumerator WaitForButtonDownLose()
    {
        while (!Input.GetButtonDown("Submit"))
            yield return null;
        if (Input.GetButtonDown("Submit"))
        {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene("Title");
        }
    }

    IEnumerator ResapwnDelay()
    {
        Racket_New.instance.playSound();
        yield return new WaitForSeconds(Racket_New.instance.player_Death.length);
        SetUpPaddle();
    }


    void SetUpPaddle()
    {
        paddle.transform.position = paddleStart.position;
    }

    public void SetUpBall()
    {
        Debug.Log("set up ball");
        ballPrefab.transform.position = ballStart.transform.position;
    }

    public void LoseALife()
    {
        lives--;
        
        livesText.text = "Lives: " + lives;

        CheckGameOver();
        StartCoroutine(ResapwnDelay());
        if(score > 0)
        {
            score -= 300;
        }
        

    }

    void CheckGameOver()
    {
        switch (BricksOrKills)
        {
            case (true):
                if (NumberOfAliens < 1)
                {
                    youWon.SetActive(true);
                    StartCoroutine(WaitForButtonDownWin());
                    ball.StopBall();
                    Racket_New.instance.canMove = false;
                }
                else if (lives < 1)
                {
                    gameOver.SetActive(true);
                    StopCoroutine(WaitForButtonDownLose());
                    ball.StopBall();
                    Racket_New.instance.canMove = false;
                }
                break;
            case (false):
                if (bricks <= 0)
                {
                    youWon.SetActive(true);
                    StartCoroutine(WaitForButtonDownWin());
                    ball.StopBall();
                    Racket_New.instance.canMove = false;
                }
                else if (lives < 1)
                {
                    gameOver.SetActive(true);
                    StartCoroutine(WaitForButtonDownLose());
                    ball.StopBall();
                    Racket_New.instance.canMove = false;
                }
                break;
                
        }

    }
}
