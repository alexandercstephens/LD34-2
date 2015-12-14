using UnityEngine;
using System.Collections;

public class BirdAI : MonoBehaviour
{

    public Vector2 Direction;
    private Rigidbody2D rigidBody;
    public Vector2 LiftingForce;
    private float startY;
    public GameObject Poo;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Direction;

        startY = transform.position.y;
    }

    public void DoLiftingForce()
    {
        rigidBody.velocity.Set(rigidBody.velocity.x, 0);

        var diffY = startY - transform.position.y;

        LiftingForce.y = 15 + diffY * 20;
        rigidBody.AddForce(LiftingForce);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Poo due to " + col.gameObject.name);
        // Start pooping
        var poo =(GameObject) Instantiate(Poo, transform.position, Quaternion.identity);
        var pooRigidBody = poo.GetComponent<Rigidbody2D>();
        pooRigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        var audioSource = rigidBody.GetComponent<AudioSource>();
        audioSource.Play();
    }

}
