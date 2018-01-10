using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
   //public GameManager gm;
    public AudioClip brick_Break;
    public GameObject particles;
    public int scoreValue = 100;

    void Start()
    {
        particles = GetComponent<GameObject>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        StartCoroutine(Bricks());

    }

    IEnumerator Bricks()
    {
        playSound();

        //Instantiate(particles, this.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(brick_Break.length -.5f);
        GameManager.instance.bricks--;
        GameManager.instance.score += this.scoreValue;
        Destroy(gameObject);

    }
    private void playSound()
    {
        Debug.Log("Brick Broke");
        AudioSource audio = GetComponent<AudioSource>();
        //  //  audio.Play();

        audio.clip = brick_Break;
        audio.Play();
    }
}
