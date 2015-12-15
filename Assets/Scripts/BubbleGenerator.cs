using UnityEngine;
using System.Collections;

public class BubbleGenerator : MonoBehaviour {
    public float secondsPerGeneration;
    public GameObject bubble;

    Bounds bounds;

    void Awake()
    {
        bounds = GetComponent<Collider2D>().bounds;
    }
    
	void Start () {
        Invoke("Generate", 0f);
	}

    void Generate ()
    {
        var x = bounds.min.x + Random.value * (bounds.max.x - bounds.min.x);
        var y = bounds.min.y + Random.value * (bounds.max.y - bounds.min.y);
        Instantiate(bubble, new Vector3(x, y, -0.03f), Quaternion.Euler(new Vector3(0f, 0f, Random.value * 360f)));
        Invoke("Generate", secondsPerGeneration * (0.5f + Random.value));
    }
}
