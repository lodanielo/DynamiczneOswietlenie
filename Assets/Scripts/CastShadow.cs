using UnityEngine;
using System.Collections;
using System.Collections.Generic;
class shadowPoint
{
    public Vector3 position;
    public int angel;
    public shadowPoint()
    {

    }
    public shadowPoint(Vector3 v, int a)
    {
       
        position = v;
        angel = a;
    }

}
class angelId
{
    public int angel;
    public int id;
    public angelId(int id)
    {
        this.id = id;
    }
}
public class CastShadow : MonoBehaviour {

    public GameObject lightPoint;
    public Transform[] shadowPoint;

    
    List<shadowPoint> points = new List<shadowPoint>();
    public Vector3[] vertics;
    public int[] triangles;
    private int AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return (int)((Vector2.Angle(Vector2.right, diference) * sign) + 720) % 360;
    }
    void SortByDistance(List<shadowPoint> s)
    {


        for (int i = 0; i < s.Count - 1; i++)
        {
            if (s[i].angel == s[i + 1].angel)
            {

                Vector2 cen = lightPoint.transform.position;
              

                if (Vector2.Distance(s[i + 1].position, cen) < Vector2.Distance(s[i].position, cen))//wiekszy index i              
                {
                      shadowPoint podstaw = s[i];
                      s[i] = s[i + 1];
                      s[i + 1] = podstaw;           
                 }



                
              
            }

        }
    }
    void calculatePoints()
    {

       
        
        points.Clear();
        
        List<angelId> idAngel = new List<angelId>();
        idAngel.Clear();
        float min1= Vector2.Distance(lightPoint.transform.position, shadowPoint[0].position);
        float min2 = 1000;
        float min3 = 1000;
        float[] dist = new float[4];
        
        int idPoint1=10;
        int idPoint2=10;
        int idPoint3 =10;

        for (int i = 0; i < shadowPoint.Length; i++)
        {

            //RaycastHit2D c = Physics2D.Raycast(lightPoint.transform.position, shadowPoint[i].position-lightPoint.transform.position);//100, 1 << LayerMask.NameToLayer("shadowDistance"));
            dist[i] = Vector2.Distance(lightPoint.transform.position, shadowPoint[i].position);                
        }
        min1 = dist[0];
        min2 = dist[1];
        min3 = dist[2];
        float max = dist[0];
        int idMAX = 0;
        
        for(int i=0;i< shadowPoint.Length; i++)
        {
            if (dist[i] >= max)
            {
                max = dist[i];
                idMAX = i;
            }                                       
        }
        for(int i = 0; i < 4; i++)
        {
            if (idPoint1 == 10 && idMAX != i)
            {
                idPoint1 = i;
                idAngel.Add(new angelId(i));
            }
                
            else if (idPoint2 == 10 && idMAX != i)
            {
                idPoint2 = i;
                idAngel.Add(new angelId(i));
            }
                
            else if(idPoint3 == 10 && idMAX != i)
            {
                idPoint3 = i;
                idAngel.Add(new angelId(i));
            }
               
            

        }
        for(int i = 0; i < idAngel.Count;i++)
        {
            idAngel[i].angel = AngleBetweenVector2(shadowPoint[idAngel[i].id].position, lightPoint.transform.position);
        }
        int maxA = idAngel[0].angel;
        int idMaxA = 0;
        for (int i = 0; i < idAngel.Count; i++)
        {
            if (idAngel[i].angel >= maxA)
            {
                maxA = idAngel[i].angel;
                idMaxA = idAngel[i].id;

            }
                
        }
        int minA = idAngel[0].angel;
        int idMinA = 0;
        for (int i = 0; i < idAngel.Count; i++)
        {
            if (idAngel[i].angel <= minA)
            {
                minA = idAngel[i].angel;
                idMinA = idAngel[i].id;

            }

        }
        RaycastHit2D d = Physics2D.Raycast(lightPoint.transform.position, shadowPoint[idMaxA].position - lightPoint.transform.position,100, 1 << LayerMask.NameToLayer("shadowDistance"));
        points.Add(new shadowPoint(d.point, AngleBetweenVector2(d.point, lightPoint.transform.position)));
        points.Add(new shadowPoint(shadowPoint[idMaxA].position, AngleBetweenVector2(shadowPoint[idMaxA].position, lightPoint.transform.position)));

        d = Physics2D.Raycast(lightPoint.transform.position, shadowPoint[idMinA].position - lightPoint.transform.position, 100, 1 << LayerMask.NameToLayer("shadowDistance"));
        points.Add(new shadowPoint(d.point, AngleBetweenVector2(d.point, lightPoint.transform.position)));
        points.Add(new shadowPoint(shadowPoint[idMinA].position, AngleBetweenVector2(shadowPoint[idMinA].position, lightPoint.transform.position)));

       // Debug.Log(shadowPoint[idMinA].name+"            "+ shadowPoint[idMaxA].name);
        //Debug.Log(idPoint1+ "            " + idPoint2);


        points.Sort((a, b) => a.angel.CompareTo(b.angel));
        SortByDistance(points);
       
    }
    void findPoints()
    {
       
       
            Vector3[] vec = new Vector3[(points.Count ) * 3];
            vertics = vec;
            int[] tri = new int[(points.Count ) * 3];
            triangles = tri;




            vertics[0] = points[0].position;
            triangles[0] = 2;
            vertics[1] = points[1].position;
            triangles[1] =1;
            vertics[2] = points[2].position;
            triangles[2] = 0;
            //////////////////////////////////////////
            vertics[3] = points[2].position;
            triangles[3] = 5;
            vertics[4] = points[1].position;
            triangles[4] = 4;
            vertics[5] = points[3].position;
            triangles[5] = 3;
        ////////////////////////////////////////////
        vertics[6] = points[0].position;
        triangles[6] = 0;
        vertics[7] = points[1].position;
        triangles[7] = 1;
        vertics[8] = points[2].position;
        triangles[8] = 2;
        //////////////////////////////////////////
        vertics[9] = points[2].position;
        triangles[9] = 3;
        vertics[10] = points[1].position;
        triangles[10] = 4;
        vertics[11] = points[3].position;
        triangles[11] = 5;


    }
    Quaternion rota;
    Vector3 pos;
    void Start()
    {

        rota = transform.rotation;
        //pos = transform.position;
        shadowPoint = new Transform[/*transform.parent.childCount-2*/4];      
        for (int i = 0; i < 4; i++)
        {
            shadowPoint[i] = transform.parent.transform.GetChild(i).transform;          
        }
       calculatePoints();
       findPoints();

    }
    
    void Update () {
        
        transform.rotation = rota;
        transform.localPosition = -transform.parent.position;
       
        

        calculatePoints();
        findPoints();
        
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        mesh.vertices = vertics;
        mesh.triangles =triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
        
	}
}
