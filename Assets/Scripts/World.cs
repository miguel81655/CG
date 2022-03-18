using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private Mesh mesh;
    public Vector3[] vertices;
    public int[] lines;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "test";

        Vector3 p0 = new Vector3(-1f, -1f, -1f);
        Vector3 p1 = new Vector3(1f, -1f, -1f);
        Vector3 p2 = new Vector3(1f, -1f, -3f);
        Vector3 p3 = new Vector3(-1f, -1f, -3f);
        Vector3 p4 = new Vector3(-1f, 1f, -1f);
        Vector3 p5 = new Vector3(1f, 1f, -1f);
        Vector3 p6 = new Vector3(1f, 1f, -3f);
        Vector3 p7 = new Vector3(-1f, 1f, -3f);

        vertices = new Vector3[]
        {
            // Bottom
            p0, p1, p2, p3,
             // Left
            p7, p4, p0, p3,
             // Front
            p4, p5, p1, p0,
             // Back
            p6, p7, p3, p2,
             // Right
            p5, p6, p2, p1,
             // Top
            p7, p6, p5, p4
        };
        mesh.vertices = vertices;

        int[] triangles = new int[]
        {
            3, 1, 0, // Bottom
            3, 2, 1,
            7, 5, 4, // Left
            7, 6, 5,
            11, 9, 8, // Front
            11, 10, 9,
            15, 13, 12, // Back
            15, 14, 13,
            19, 17, 16, // Right
            19, 18, 17,
            23, 21, 20, // Top
            23, 22, 21,
        };
        mesh.triangles = triangles;

        lines = new int[]
                {
            0, 1,
            0, 3,
            0, 5,
            1, 2,
            1, 9,
            2, 3,
            2, 12,
            3, 4,
            5, 9,
            5, 4,
            9, 12,
            12, 4
                };

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    void RotationX3D(float angle)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(1, 0, 0, 0));
        matrix.SetRow(1, new Vector4(0, Mathf.Cos(angle), -Mathf.Sin(angle), 0));
        matrix.SetRow(2, new Vector4(0, Mathf.Sin(angle), Mathf.Cos(angle), 0));
        matrix.SetRow(3, new Vector4(0, 0, 0, 1));

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = matrix.MultiplyPoint(vertices[i]);
        }
        mesh.vertices = vertices;
    }

    void RotationY3D(float angle)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(Mathf.Cos(angle), 0, Mathf.Sin(angle), 0));
        matrix.SetRow(1, new Vector4(0, 1, 0, 0));
        matrix.SetRow(2, new Vector4(-Mathf.Sin(angle), 0, Mathf.Cos(angle), 0));
        matrix.SetRow(3, new Vector4(0, 0, 0, 1));

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = matrix.MultiplyPoint(vertices[i]);
        }
        mesh.vertices = vertices;
    }

    void RotationZ3D(float angle)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(Mathf.Cos(angle), -Mathf.Sin(angle), 0, 0));
        matrix.SetRow(1, new Vector4(Mathf.Sin(angle), Mathf.Cos(angle), 0, 0));
        matrix.SetRow(2, new Vector4(0, 0, 1, 0));
        matrix.SetRow(3, new Vector4(0, 0, 0, 1));

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = matrix.MultiplyPoint(vertices[i]);
        }
        mesh.vertices = vertices;
    }

    private void Update()
    {
       /* RotationZ3D(5* Mathf.Deg2Rad *Time.deltaTime);
        RotationX3D(5*Mathf.Deg2Rad *Time.deltaTime);
        RotationY3D(5* Mathf.Deg2Rad *Time.deltaTime);*/
        
    }
}
