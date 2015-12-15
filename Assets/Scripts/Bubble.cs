using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {
    public float speed = 0.2f;
    public LayerMask whatWillBreakMe;

    float mySpeed;
    Collider2D col;


    void Start ()
    {
        mySpeed = speed * (0.5f + Random.value);
        col = GetComponent<Collider2D>();
    }

	void Update () {
        transform.position += new Vector3(0f, mySpeed, 0f);
        if (transform.position.y >= -27.18f) //if (col.IsTouchingLayers(whatWillBreakMe)) //WHY doesn't this work
        {
            Destroy(gameObject);
        }
	}
}
