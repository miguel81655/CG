using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGrid: MonoBehaviour
{
    private Mesh mesh;
    public int xSize = 5;
    public int ySize = 3;
    public float xScaleFactor = 0.3f;
    public float yScaleFactor = 0.3f;
    public float refinement = 0.01f;
    public float heightSclae = 0.04f;
    public float sampleOffset = 0;



    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "MyGrid";
        Vector3[] vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        int vetCount = 0;
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.PerlinNoise(x * refinement, y * refinement) * heightSclae;
                vertices[vetCount] = new Vector3(x * xScaleFactor,height, y * yScaleFactor);
                vetCount++;
            }
        }
        mesh.vertices = vertices;

        int[] triangles = new int[(xSize * ySize) * 6];
        int triCont = 0;
        int vertIndex = 0;
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[triCont] = vertIndex;
                triangles[triCont + 1] = vertIndex + xSize + 1;
                triangles[triCont + 2] = vertIndex + 1;
                triangles[triCont + 3] = vertIndex + 1;
                triangles[triCont + 4] = vertIndex + xSize + 1;
                triangles[triCont + 5] = vertIndex + xSize + 2;
                triCont += 6;
                vertIndex++;
            }
            vertIndex++;
        }
        mesh.triangles = triangles;

        Vector3[] normals = new Vector3[vertices.Length];

        for (int i = 0; i < vetCount; i++)
        {
            normals[i] = -Vector3.forward;
        }

        mesh.normals = normals;

        Vector2[] uv = new Vector2[vertices.Length];
        vetCount = 0;
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uv[vetCount] = new Vector2((float)x / xSize, (float)y / ySize);
                vetCount++;
            }
        }
        mesh.uv = uv;
    }
}

