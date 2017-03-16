using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class WeapanScript : NetworkBehaviour
{

	
    public GameObject bulletPref;
    public float atackSpeed;
    float clock;
	
	void Update () {
        clock += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
            clock = 0;          
           
            
        }
	}
    [Command]
    void CmdFire()
    {
        Debug.Log("efef");
        GameObject clone = Instantiate(bulletPref);
        clone.transform.position = transform.GetChild(1).transform.position;
        clone.transform.rotation = transform.rotation;
        clone.GetComponent<bulletScript>().setDir(transform.GetChild(0).transform.position);
        NetworkServer.Spawn(clone);
    }
}
