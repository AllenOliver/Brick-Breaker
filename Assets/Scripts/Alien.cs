using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alien : MonoBehaviour {
    public int currentHealth;
    public int maxHealth;
  //  public Slider healthSlider;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public int ScoreValue;
    private Ball ball;
	// Use this for initialization
	void Start ()
    {
        ball = FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Killed();
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch(col.gameObject.tag)
        {
            case ("Ball"):
                TakeDamage(ball.Damage);
                break;
        }
    }


    void PlayHitSound()
    {
       AudioSource audio = GetComponent<AudioSource>();
        audio.clip = hitSound;
        audio.Play();
    }

    void PlayDeathSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = deathSound;
        audio.Play();
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        //healthSlider.value = currentHealth;
        PlayHitSound();
        GameManager.instance.score += this.ScoreValue;
        if (currentHealth < 1)
        {
            Debug.Log("Died");
            PlayDeathSound();
            StartCoroutine(Wait(deathSound.length-.25f));
            GameManager.instance.NumberOfAliens--;
        }
    }


    IEnumerator Wait(float seconds)
    {

        yield return new WaitForSeconds(seconds -.25f);
        Destroy(gameObject);
    }
}
