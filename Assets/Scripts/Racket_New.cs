using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket_New : MonoBehaviour {

    public float paddleSpeed = 1f;
    public bool canMove;
    public AudioClip player_Death;
    public static Racket_New instance = null;
    private Vector2 playerPos = new Vector2(0, -94.3f);


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

    }



    public void playSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = player_Death;
        audio.Play();
    }

    void Update()
    {
        if (canMove)
        {
            float xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
            playerPos = new Vector2(Mathf.Clamp(xPos, -87f, 87f), -94.3f);
            transform.position = playerPos;
        }
    }
}
