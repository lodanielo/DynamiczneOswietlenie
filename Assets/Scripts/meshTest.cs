using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class meshTest : MonoBehaviour
{
    public float szerokość;
    public float wysokość;
    private Vector3 objectPosition;
    // List <Vector3> vertics=new List<Vector3>();
    //List<int> triangles = new List<int>();

    //private Vector3[] newVertices=new Vector3[3];
    // private int[] newTriangles=new int[3];
    public Vector3[] newVertices;
    public int[] newTriangles;
    //public Vector2[] newUV;
    public GameObject dir;
    void Start()
    {
      
       // objectPosition = transform.position;
       
       // newVertices[0] = objectPosition;
       // newVertices[1] = new Vector3((0 - szerokość / 2), (0 - wysokość / 2), 0);
       // newVertices[2] = new Vector3((0 + szerokość / 2), (0 + wysokość / 2), 0);

      //  newTriangles[0] = 0;
       // newTriangles[1] = 1;
       // newTriangles[2] = 2;



    }
   
    
    void Update()
    {


        // Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
       
        if (Physics.Raycast(transform.position, -Vector2.up, out hit))
        {
            newVertices[2] = hit.transform.position;
             print("Found an object - distance: " + hit.distance);
          //  Debug.DrawRay(transform.position, hit.distance, Color.yellow);
    
        }
            

        Mesh mesh = new Mesh();
       // mesh.Clear();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = newVertices;
       
        mesh.triangles = newTriangles;

        
            
        
      
       
    }
}
