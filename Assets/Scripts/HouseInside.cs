using UnityEngine;
using System.Collections;

public class HouseInside : MonoBehaviour {
    public Collider2D houseCollider;
    public Transform player;
    
    Collider2D trigger;
    
    void Awake ()
    {
        trigger = GetComponent<Collider2D>();
    }
    
	void Update ()
    {
        if (trigger.OverlapPoint(player.position))
        {
            houseCollider.enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.01f);
        } else
        {
            houseCollider.enabled = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.01f);
        }
	}
}
