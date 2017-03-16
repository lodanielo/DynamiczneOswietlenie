using UnityEngine;
using System.Collections;

public class DestroyOdciskiStop : MonoBehaviour {

    float clock;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        clock +=Time.deltaTime;
        if (clock >= 4)
            GetComponent<Animator>().SetBool("StartDestroy", true);
        if (clock >= 6)
             Destroy(gameObject);
	
	}
}
