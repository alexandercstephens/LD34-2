using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	void Update () {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 1f);
	}
}
