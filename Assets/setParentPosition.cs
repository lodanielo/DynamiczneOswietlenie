using UnityEngine;
using System.Collections;

public class setParentPosition : MonoBehaviour {

    // Use this for initialization
    Quaternion rot;
	void Awake()
    {
        rot = transform.rotation;
    }
	// Update is called once per frame
	void Update () {
        transform.localPosition = -transform.parent.transform.position;
        transform.rotation = rot;
        transform.localRotation = rot;
    }
}
