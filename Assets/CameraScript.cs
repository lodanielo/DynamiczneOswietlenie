using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform target;
	
	void LateUpdate () {
        transform.position = target.position;
	}
}
