using UnityEngine;
using System.Collections;

public class odciskiStop : MonoBehaviour {

    public GameObject odciskStopyjPref;
    
    float clock=0;
    bool prawaStopa = false;
    public float howOfftenSpawnFoot = 0.2f;
	
	// Update is called once per frame
	void Update () {
        clock += Time.deltaTime;
        if (GetComponent<Rigidbody2D>().IsSleeping() == false&&clock>=howOfftenSpawnFoot)
        {
            GameObject clone;
            clone = Instantiate(odciskStopyjPref);
            clone.transform.position = transform.position;
            if (prawaStopa == false)
            {
                if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S) )
                    clone.transform.position+=new Vector3(-0.5f,0,0);
                else clone.transform.position += new Vector3(0,-0.5f, 0);
                prawaStopa = true;
            }
            else
            {

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                    clone.transform.position+= new Vector3(0.5f, 0, 0);
                else clone.transform.position += new Vector3( 0,0.5f, 0);
                prawaStopa = false;
            }
            
            
            if (Input.GetKey(KeyCode.W))
            {
                clone.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                clone.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            if (Input.GetKey(KeyCode.D))
            {
               clone.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            if (Input.GetKey(KeyCode.A))
            {
                clone.transform.rotation = Quaternion.Euler(0, 0, 90);
                
            }
           
            
            clock = 0;
        }
	}
}
