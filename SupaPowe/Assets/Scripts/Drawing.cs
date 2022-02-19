using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{

    [SerializeField] private Transform debug1;
    [SerializeField] private Transform debug2;


    private Mesh mesh;
    private Vector3 lastMousePos;
    public float lineThickness = 1f;

    public float minDistance;
    public float maxDistance;
    public float distanceTraveled;

    public int maxDrawing;
    public int drawnLine;

    public bool isDrawing;

    private void Awake()
    {
       

    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if(drawnLine < maxDrawing)
            {

            
           
            mesh = new Mesh();

            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];

            vertices[0] = mousePos();
            vertices[1] = mousePos();
            vertices[2] = mousePos();
            vertices[3] = mousePos();

            uv[0] = Vector2.zero;
            uv[1] = Vector2.zero;
            uv[2] = Vector2.zero;
            uv[3] = Vector2.zero;

            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;

            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.MarkDynamic();

            distanceTraveled = 0;
            GetComponent<MeshFilter>().mesh = mesh;
            }




            lastMousePos = mousePos();


        }
        if (Input.GetMouseButton(0))
        {
            

            if (Vector3.Distance(mousePos(), lastMousePos) > minDistance && distanceTraveled < maxDistance && drawnLine < maxDrawing) {
            
            isDrawing = true;


            Vector3[] vertices = new Vector3[mesh.vertices.Length +2];
            Vector2[] uv = new Vector2[mesh.uv.Length +2];
            int[] triangles = new int[mesh.triangles.Length +6];


            mesh.vertices.CopyTo(vertices,0);
            mesh.uv.CopyTo(uv, 0);
            mesh.triangles.CopyTo(triangles, 0);

            int vIndex = vertices.Length - 4;
            int vIndex0 = vIndex + 0;
            int vIndex1 = vIndex + 1;
            int vIndex2 = vIndex + 2;
            int vIndex3 = vIndex + 3;


            Vector3 mouseForwardVector = (mousePos() - lastMousePos).normalized;
            Vector3 normal2D = new Vector3(0, 0, -1);
            Vector3 newVertexUp = mousePos() + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness;
            Vector3 newVertexDown = mousePos() + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness;

            vertices[vIndex2] = newVertexUp;
            vertices[vIndex3] = newVertexDown;
            uv[vIndex2] = Vector2.zero;
            uv[vIndex3] = Vector2.zero;

            int tIndex = triangles.Length - 6; 

            triangles[tIndex + 0] = vIndex0;
            triangles[tIndex + 1] = vIndex2;
            triangles[tIndex + 2] = vIndex1;

            triangles[tIndex + 3] = vIndex1;
            triangles[tIndex + 4] = vIndex2;
            triangles[tIndex + 5] = vIndex3;

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;

            
            distanceTraveled += Vector3.Distance(mousePos(), lastMousePos);
            

                lastMousePos = mousePos();

            //Debug UpDown
            debug1.position = newVertexUp;
            debug2.position = newVertexDown;

            

            }

        }



        if (Input.GetMouseButtonUp(0))
        {

            if(isDrawing == true)
            {
                drawnLine++; //Burda bug çýkabilir
                isDrawing = false;
                distanceTraveled = 0f;

            }


        }
    }


    public static Vector3 mousePos()
    {
        Vector3 worldPos =  new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0); 
        return worldPos;
    }
}
