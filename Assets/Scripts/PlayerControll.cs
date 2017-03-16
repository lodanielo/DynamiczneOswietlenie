using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerControll : NetworkBehaviour
{

    public float speed=3;
    public GameObject bulletPref;
    public Transform shotDir;
    public bool move = false;
	void Start () {
        if (!isLocalPlayer)
        {
            return;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).GetComponent<PlayerRotate>().rot = false;
        if (!isLocalPlayer)
        {
            return;
        }
        transform.GetChild(0).GetComponent<PlayerRotate>().rot = true;
        move = false;

    
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(x, y, 0);
        if (Input.GetMouseButton(0))
        {
            CmdFire();
        }

    }
    [Command]
    void CmdFire()
    {

        Vector2 mouseP=Camera.main.WorldToScreenPoint(Input.mousePosition);
        GameObject clone = Instantiate(bulletPref);
        clone.transform.rotation = transform.GetChild(0).rotation;
        clone.transform.position =transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.position;
        Vector2 shotVelocity =new Vector2(Random.Range(-2, 2),30);
       
        shotDir.localPosition= shotVelocity;
        clone.GetComponent<Rigidbody2D>().velocity=  shotDir.position-transform.position;
        NetworkServer.Spawn(clone);
        Destroy(clone, 0.2f);

    }
    public override void OnStartLocalPlayer()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>().target = transform;
    }



}
