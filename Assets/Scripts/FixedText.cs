using UnityEngine;
using System.Collections;

public class FixedText : MonoBehaviour {
    public Transform target;

    Vector3 diff;
    
	void Awake () {
        diff = transform.position - target.position;
	}
	
	void Update () {
        transform.position = target.position + diff;
	}
}
