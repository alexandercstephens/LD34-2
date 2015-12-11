using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public float speed = 1.0f;
    public float gravity = 5.0f;
    public Transform gravitySource;

    Rigidbody2D rb;
    Vector2 movementForce;
    Vector2 gravityForce;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        Vector2 distanceFromGravity = gravitySource.position - transform.position;
        Vector2 gravityDirection = distanceFromGravity.normalized;
        gravityForce = gravity * gravityDirection / distanceFromGravity.sqrMagnitude;

        movementForce = speed * Input.GetAxis("Horizontal") * new Vector2(-gravityDirection.y, gravityDirection.x);

        rb.AddForce(movementForce + gravityForce);
	}

    void OnDrawGizmos () {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, movementForce);
        Gizmos.DrawRay(transform.position, gravityForce);
        if (rb != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position, rb.velocity);
        }
    }
}
