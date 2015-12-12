using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform toFollow;
    Vector3 diff;
	
    void Awake () {
        diff = transform.position - toFollow.position;
    }

	void Update () {
        transform.position = toFollow.position + diff;
	}
}
