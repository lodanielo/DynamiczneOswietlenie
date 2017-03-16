using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class PlayerRotate : NetworkBehaviour
{


    public bool rot = false;
   void Start()
    {
        
    }
    void LateUpdate () {
        if (rot == true)
        {
            
            
            Vector3 mouseP = Input.mousePosition;
             Vector3 playerP = Camera.main.WorldToScreenPoint(transform.position);
             float mouseX = mouseP.x - playerP.x;
             float mouseY = mouseP.y - playerP.y;
             float angle = Mathf.Atan2(mouseX, mouseY) * Mathf.Rad2Deg;
             transform.rotation = Quaternion.Euler(0, 0,-angle);
        }
       

        
        
    }
}
