using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public float speed = 1f;
    public float airSpeed = 5f;

    Rigidbody2D rb;
    SpriteRenderer sr;

    bool isGrounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
	}

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        float move = Input.GetAxis("Horizontal");
        if (isGrounded) {
            rb.velocity = new Vector2(speed * move, rb.velocity.y);
        } else {
            rb.AddForce(new Vector2(airSpeed * move, airSpeed * Input.GetAxis("Vertical")));
        }
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) >= 0.01) {
            sr.flipX = rb.velocity.x < 0;
        }
    }
}
