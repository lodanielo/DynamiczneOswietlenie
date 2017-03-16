/*using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;
/*class shadowPoint
{
    public Vector3 position;
    public int angel;
    public Transform parentTransform;
    
    

    public shadowPoint()
    {

    }
    public shadowPoint(Vector3 v, int a,Transform parentTransform)
    {
        this.parentTransform = parentTransform;
        position = v;
        angel = a;
    }

    

    public void setAngel(int angel)
    {
        this.angel = angel;
    }
}
public class raycastTest : MonoBehaviour
{

    

   

    public GameObject[] points;
    Vector3[] vertices;
    public int[] triangles; 
    List<shadowPoint> shadowP = new List<shadowPoint>();
    List<Vector3> rendererPoints = new List<Vector3>();
    bool sort = false;
    bool meshes = false;
    public Vector3 center;
    public GameObject pointPref;
   
  
    
    void findPoints()
    {
        //points = GameObject.FindGameObjectsWithTag("ShadowPoint");

        Vector3[] v = new Vector3[(points.Length-1)*3+3];
        vertices = v;
      
        int[] t = new int[(points.Length - 1) * 3+3];
        triangles = t;
        shadowP.Clear();
        if(shadowP.Count<points.Length)
            for(int i = 0; i < points.Length; i++)
            {
                shadowP.Add(new shadowPoint(points[i].transform.position,AngleBetweenVector2(center,points[i].transform.position),points[i].transform.parent));
            }
                 
        
        
            
    }
    void calculatePoints()
    {
        GameObject[] point = GameObject.FindGameObjectsWithTag("ShadowPoint");
        List<GameObject> newPoint = new List<GameObject>();
        
        for(int i = 0; i < point.Length; i++)
        {
            //Debug.Log(point.Length);
            Vector2 cen = center;
            Vector2 dir = point[i].transform.position;
            RaycastHit2D c = Physics2D.Raycast(cen, dir*100);
            bool add=true;
            
            
            if (c)
            {
                
                {


                    Debug.Log(Vector2.Distance(c.point, cen) + "   " + Vector2.Distance(dir, cen));
                    if (Vector2.Distance(c.point, cen) <= Vector2.Distance(dir, cen))
                    {
                        float dist = Vector2.Distance(c.point, cen) - Vector2.Distance(dir, cen);
                        if (Mathf.Abs(dist) > 0.3)
                            add = false;
                       
                           
                    }
                    else if (Vector2.Distance(c.point, cen) - 0.5 >= Vector2.Distance(dir, cen))
                    {
                        Vector3 hitPosition = c.point;
                        GameObject clone = Instantiate(pointPref);
                        clone.transform.position = hitPosition;
                        clone.name = ".";
                        
                        newPoint.Add(clone);
                        Destroy(clone);
                        
                    }
                   
                   

                }
            }
           if(add==true)
            newPoint.Add(point[i]);
        }
       
       
        GameObject[] p = new GameObject[newPoint.Count];
        points = p;
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = newPoint[i];
        }
    }
    private int AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return(int)( (Vector2.Angle(Vector2.right, diference) * sign)+720)%360;
    }
    void SortByDistance(List <shadowPoint> s)
    {

       
        for(int i = 0; i < s.Count-1; i++)
        {
            if (s[i].angel == s[i + 1].angel)
            {

                Vector2 cen = center;
                if (Vector2.Distance(s[i + 1].position, cen) > Vector2.Distance(s[i].position, cen))//wiekszy index i+1
                {
                    //Debug.Log("A");
                   // Debug.Log(AngleBetweenVector2(s[i].parentTransform.position,center));
                    if(s[i].parentTransform!=null)
                    if (AngleBetweenVector2(s[i].parentTransform.position,cen) > AngleBetweenVector2( s[i].position,cen))//rodzic wiekszy kąt-czyli dłuższy ma być pierwszy
                    {
                        Debug.Log("B");
                        shadowPoint podstaw = s[i];
                        s[i] = s[i + 1];
                        s[i + 1] = podstaw;

                    }
                  


                }
                if(Vector2.Distance(s[i + 1].position, cen) < Vector2.Distance(s[i].position, cen))//wiekszy index i              
                {
                    if (s[i+1].parentTransform != null)
                    if (AngleBetweenVector2( s[i+1].parentTransform.position,cen) < AngleBetweenVector2( s[i+1].position,cen))    
                    { Debug.Log("D");
                        shadowPoint podstaw = s[i];
                        s[i] = s[i + 1];
                        s[i + 1] = podstaw;
                        
                    }
                }
            }
               
        }
    }
    void Start()
    {
        calculatePoints();
        findPoints();
       
    }
    
    void LateUpdate()
    {
        center = pointPref.transform.position;
        calculatePoints();
        findPoints();
        

        //Debug.Log("SP= "+shadowP.Count);
        sort = false;
        /*if (sort == false)
        {
            for(int i = 0; i < points.Length; i++)
            {
                Vector2 vecA = center;
                shadowP[i].angel=AngleBetweenVector2(points[i].transform.position, vecA);
           

            }
        }

      
        if (sort == false)
        {
          
            shadowP.Sort((a, b) => a.angel.CompareTo(b.angel));
            SortByDistance(shadowP);
            sort = true;
           /* for(int i = 0; i < shadowP.Count;i++)
            {
                Debug.Log(shadowP[i].angel + "  " + shadowP[i].position);
            }
        }
       
     
        int tri = 0;
       
        if(shadowP.Count>0)
       
        for (int i = 0; i < shadowP.Count; i++)
        {

                if (i + 1 < shadowP.Count)
                {
                    vertices[tri] = shadowP[i].position;
                    triangles[tri] = tri;
                    tri += 1;/////
                    vertices[tri] = center;
                    triangles[tri] = tri;
                    tri += 1;/////
                    vertices[tri] = shadowP[i + 1].position;
                    triangles[tri] = tri;
                    tri += 1;
                }
                else
                {
                    vertices[tri] = shadowP[i].position;
                    triangles[tri] = tri;
                    tri += 1;/////
                    vertices[tri] =center; 
                    triangles[tri] = tri;
                    tri += 1;/////
                    vertices[tri] = shadowP[0].position;
                    triangles[tri] = tri;
                }
               
               /* if (i + 2 >= shadowP.Count)
                {
                  
                }



            }
         if (shadowP.Count > 0)//segregowanie ze wzgledu na kąt
         {

             

         }
       

        
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
       
        
       
        //Debug.Log( "FPS :"  + 1.0f / Time.fixedDeltaTime);





        
        



    }
}
*/