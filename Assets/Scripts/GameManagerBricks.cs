using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerBricks : MonoBehaviour {


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
    public Transform paddleStart;
    public Transform ballStart;
    public Ball ball;
    // public GameObject deathParticles;
    public static GameManagerBricks instance = null;


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

        Setup();

    }

    void Update()
    {
        scoreText.text = score.ToString();

        CheckGameOver();
    }

    public void Setup()
    {
        StartCoroutine(setUpBallAndPaddle());
        Instantiate(bricksPrefab, transform.position, Quaternion.identity);
        //Instantiate(alienPrefab, transform.position, Quaternion.identity);
        livesText.text = "Lives: " + lives;
        Racket_New.instance.canMove = true;
        score = 0;
    }


    public void DestroyBrick()
    {
        bricks--;
        //Instantiate(brickParticle, bricksPrefab.transform.position, Quaternion.identity);

    }


    void Reset()
    {
        int c = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(c, LoadSceneMode.Single);
    }

    IEnumerator setUpBallAndPaddle()
    {
        Debug.Log("enum");
        SetUpBall();
        yield return new WaitForSeconds(2f);
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
        SetUpPaddle();


    }

    void CheckGameOver()
    {
        if (bricks <= 0)
        {
            youWon.SetActive(true);
            ball.StopBall();
            Racket_New.instance.canMove = false;
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCount > nextSceneIndex)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene("Finale");
            }
        }


        else if (lives < 1)
        {
            gameOver.SetActive(true);
            ball.StopBall();
            Racket_New.instance.canMove = false;
        }



    }
}
