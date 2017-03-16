using UnityEngine;
using System.Collections;

public class DestroySplash : MonoBehaviour {

    float clock;
    public float timeDestroy;
	void Update () {
        clock += Time.deltaTime;
        if (clock >= timeDestroy)
            Destroy(gameObject);

	}
}
