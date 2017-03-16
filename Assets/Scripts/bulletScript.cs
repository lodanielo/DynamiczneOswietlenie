using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

    public float moveSpeed=1;
    public GameObject splash;
    private Vector3 dir;
	void Start () {
	
	}
    void cloneSplash()
    {
        GameObject clone = Instantiate(splash);
        clone.transform.position = transform.position;
        clone.transform.rotation = transform.rotation;
    }
	public void setDir(Vector3 direction)
    {
        
        dir = direction;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       
       if(other.tag!="bullet")
         Destroy(gameObject);
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(10);
        }

    }
    void LateUpdate()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, dir, step);
        if (transform.position == dir)
        {
           // cloneSplash();
           

        }
    }
            
}
