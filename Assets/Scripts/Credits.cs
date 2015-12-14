using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
    public float[] stops;
    int nextStopIndex;

    bool isScrolling;

    void Start () {
        nextStopIndex = 0 ;
        isScrolling = true;
    }

    void Update () {
        if (isScrolling) {
            transform.position += new Vector3(0f, 1f, 0f) * Time.deltaTime;
            if (transform.position.y >= stops[nextStopIndex])  {
                transform.position = new Vector3(transform.position.x, stops[nextStopIndex], transform.position.z);
                isScrolling = false;
                nextStopIndex += 1;
                if (nextStopIndex < stops.Length) {
                    Invoke("ResumeScroll", 1f);
                }
            }
        }
	}

    void ResumeScroll() {
        isScrolling = true;
    }
}
