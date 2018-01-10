using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioClip paddle_Hit;
    public float speed = 100.0f;
    public Rigidbody2D rb;
    public int Damage;


	// Use this for initialization
	void Start ()
    {
        AudioSource audio = GetComponent<AudioSource>();
        //  audio.Play();

        audio.clip = paddle_Hit;

        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    private void playSound()
    {
         AudioSource audio = GetComponent<AudioSource>();
        //  //  audio.Play();

         audio.clip = paddle_Hit;
         audio.Play();
    }
    public void StopBall()
    {
        var rigidbody = this.GetComponent<Rigidbody2D>();
        if (rigidbody)
        {

            rigidbody.isKinematic = false;

            // Reset the velocity
            rigidbody.velocity = Vector2.zero;
            //rigidbody.angularVelocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.name)
        {
            case ("racket_new"):
                float x = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.x);
                Vector2 direction = new Vector2(x, 1).normalized;
                GetComponent<Rigidbody2D>().velocity = direction * speed;
                playSound();
                break;
            default:
                break;
        }

    }

     private void OnTriggerEnter2D(Collider2D col)
    {

            if(col.tag == "Kill")
            {
            GameManager.instance.LoseALife();
                var rigidbody = this.GetComponent<Rigidbody2D>();
                if (rigidbody)
                {
                    // Setting isKinematic to False will ensure that this object
                    // will not be affected by any force from the Update() function
                    // In case the update function runs after this one xD
                    rigidbody.isKinematic = false;

                    // Reset the velocity
                    rigidbody.velocity = Vector2.zero;
                    //rigidbody.angularVelocity = Vector2.zero;
                   
                }
                //move object to start position
                transform.position = GameManager.instance.ballStart.transform.position;


                // I want to stop the object here, after it was moved to start position.   Because my ball was moving when it hit Trap object, so when it was moved to start position, it keeps rolling.

        }
        
    }
    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }

}
