using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
    public Transform parallaxee;

    float origP;
    float origC;
    float diff;

	void Awake () {
        origP = parallaxee.position.x;
        origC = transform.position.x;
    }
	
	void Update () {
        parallaxee.position = new Vector3(origP + 0.5f * (transform.position.x - origC), parallaxee.position.y, parallaxee.position.z);
	}
}
