using UnityEngine;
using System.Collections;

public class StarGenerator : MonoBehaviour {
    public float secondsPerGeneration;
    public GameObject[] stars;

    Bounds bounds;

    void Awake()
    {
        bounds = GetComponent<Collider2D>().bounds;
    }

    void Start()
    {
        Invoke("Generate", 0f);
    }

    void Generate()
    {
        var x = bounds.min.x + Random.value * (bounds.max.x - bounds.min.x);
        var y = bounds.min.y + Random.value * (bounds.max.y - bounds.min.y);
        var i = Mathf.FloorToInt(Random.value * stars.Length);
        Instantiate(stars[i], new Vector3(x, y, -0.03f), Quaternion.Euler(new Vector3(0f, 0f, Random.value * 360f)));
        Invoke("Generate", secondsPerGeneration * (0.5f + Random.value));
    }
}
