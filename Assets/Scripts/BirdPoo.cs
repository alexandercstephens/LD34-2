using UnityEngine;
using System.Collections;

public class BirdPoo : MonoBehaviour {


    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Hit");
        var rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = 0;

        Destroy(gameObject, 2f);
    }

}
