using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public float speed = 1f;
    public float airSpeed = 5f;
    public float jumpForce = 5f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    NoseScript nose;

    bool isGrounded = false;
    public Transform groundCheck;
    float groundRadius = 0.1f;
    public LayerMask whatIsGround;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        nose = GetComponentInChildren<NoseScript>();
	}

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        float move = Input.GetAxis("Horizontal");
        if (isGrounded) {
            rb.velocity = new Vector2(speed * move, rb.velocity.y);
        } else if (nose.isNosing) {
            //rb.AddForce(new Vector2(airSpeed * move, airSpeed * Input.GetAxis("Vertical")));
        }
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) >= 0.01) {
            sr.flipX = rb.velocity.x < 0;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
}
