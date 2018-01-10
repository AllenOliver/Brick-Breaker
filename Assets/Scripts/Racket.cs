using UnityEngine;
using System.Collections;

public class Racket : MonoBehaviour
{
    // Movement Speed
    public float speed = 150;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = Vector2.right * h * speed;
    }
}