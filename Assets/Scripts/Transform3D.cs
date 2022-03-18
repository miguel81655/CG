using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform3D : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "Test";


        vertices = new Vector3[18];
        vertices[0] = new Vector3(-10, 0, -10);
        vertices[1] = new Vector3(10, 0, -10);
        vertices[2] = new Vector3(0, 10, 0);

        vertices[3] = new Vector3(-10, 0, 10);
        vertices[4] = new Vector3(10, 0, 10);
        vertices[5] = new Vector3(0, 10, 0);

        vertices[6] = new Vector3(-10, 0, 10);
        vertices[7] = new Vector3(-10, 0, -10);
        vertices[8] = new Vector3(0, 10, 0);

        vertices[9] = new Vector3(10, 0, 10);
        vertices[10] = new Vector3(10, 0, -10);
        vertices[11] = new Vector3(0, 10, 0);

        vertices[12] = new Vector3(-10, 0, -10);
        vertices[13] = new Vector3(10, 0, -10);
        vertices[14] = new Vector3(-10, 0, 10);

        vertices[15] = new Vector3(10, 0, 10);
        vertices[16] = new Vector3(10, 0, -10);
        vertices[17] = new Vector3(-10, 0, 10);

        mesh.vertices = vertices;
        int[] triangles = new int[18];
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 3;
        triangles[4] = 4;
        triangles[5] = 5;

        triangles[6] = 6;
        triangles[7] = 8;
        triangles[8] = 7;

        triangles[9] = 9;
        triangles[10] = 10;
        triangles[11] = 11;

        triangles[12] = 12;
        triangles[13] = 13;
        triangles[14] = 14;

        triangles[15] = 15;
        triangles[16] = 17;
        triangles[17] = 16;

        mesh.triangles = triangles;

        normals = new Vector3[18];
        normals[0] = Vector3.back + Vector3.up;
        normals[1] = Vector3.back + Vector3.up;
        normals[2] = Vector3.back + Vector3.up;

        normals[3] = Vector3.forward + Vector3.up;
        normals[4] = Vector3.forward + Vector3.up;
        normals[5] = Vector3.forward + Vector3.up;

        normals[6] = Vector3.left + Vector3.up;
        normals[7] = Vector3.left + Vector3.up;
        normals[8] = Vector3.left + Vector3.up;

        normals[9] = Vector3.right + Vector3.up;
        normals[10] = Vector3.right + Vector3.up;
        normals[11] = Vector3.right + Vector3.up;

        normals[12] = Vector3.down;
        normals[13] = Vector3.down;
        normals[14] = Vector3.down;

        mesh.normals = normals;

        RotateZ3D(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Translate3D(10 * Time.deltaTime, 0, 0);
        //Scale3D(1.001f,1.001f, 1.001f);
        RotateAroundPosition(new Vector3(5 * Time.deltaTime, 5 * Time.deltaTime,  0 * Time.deltaTime), new Vector3(0,10,0));
        
    }

    void Translate3D(float tx, float ty, float tz)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(1, 0, 0, tx));
        matrix.SetRow(1, new Vector4(0, 1, 0, ty));
        matrix.SetRow(2, new Vector4(0, 0, 1, tz));
        matrix.SetRow(3, new Vector4(0, 0, 0, 1));

        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = matrix.MultiplyPoint(vertices[i]);
            normals[i] = matrix.MultiplyPoint(normals[i]);
        }
        mesh.vertices = vertices;
    }

    void RotateX3D(float angle)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(1, 0, 0, 0));
        matrix.SetRow(1, new Vector4(0, Mathf.Cos(angle), -Mathf.Sin(angle), 0));
        matrix.SetRow(2, new Vector4(0, Mathf.Sin(angle), Mathf.Cos(angle), 0));
        matrix.SetRow(3, new Vector4(0, 0, 0, 1));

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = matrix.MultiplyPoint(vertices[i]);
            normals[i] = matrix.MultiplyPoint(normals[i]);
        }
        mesh.vertices = vertices;
    }
    void RotateY3D(float angle)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(Mathf.Cos(angle), 0, Mathf.Sin(angle), 0));
        matrix.SetRow(1, new Vector4(0, 1, 0, 0));
        matrix.SetRow(2, new Vector4(-Mathf.Sin(angle), 0, Mathf.Cos(angle), 0));
        matrix.SetRow(3, new Vector4(0, 0, 0, 1));

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = matrix.MultiplyPoint(vertices[i]);
            normals[i] = matrix.MultiplyPoint(normals[i]);
        }
        mesh.vertices = vertices;
    }

    void RotateZ3D(float angle)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(Mathf.Cos(angle), -Mathf.Sin(angle), 0 , 0));
        matrix.SetRow(1, new Vector4(Mathf.Sin(angle), Mathf.Cos(angle), 0, 0));
        matrix.SetRow(2, new Vector4(0, 0, 1, 0));
        matrix.SetRow(3, new Vector4(0, 0, 0, 1));

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = matrix.MultiplyPoint(vertices[i]);
            normals[i] = matrix.MultiplyPoint(normals[i]);
        }
        mesh.vertices = vertices;
    }

    void Scale3D(float tx, float ty, float tz)
    {
        
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(tx, 0, 0, 0));
        matrix.SetRow(1, new Vector4(0, ty, 0, 0));
        matrix.SetRow(2, new Vector4(0, 0, tz, 0));
        matrix.SetRow(3, new Vector4(0, 0, 0, 1));

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = matrix.MultiplyPoint(vertices[i]);
        }
        mesh.vertices = vertices;
    }

    void RotateAroundPosition(Vector3 angles, Vector3 point)
    {
        Vector3 t2 = transform.position - point;
        Vector3 t1 = point - transform.position;

        Vector3 angle = new Vector3(angles.x * Mathf.Deg2Rad, angles.y * Mathf.Deg2Rad, angles.z * Mathf.Deg2Rad);
        Matrix4x4 matrix1 = new Matrix4x4();

        matrix1.SetRow(0, new Vector4(1, 0, 0, t1.x));
        matrix1.SetRow(1, new Vector4(0, 1, 0, t1.y));
        matrix1.SetRow(2, new Vector4(0, 0, 1, t1.z));
        matrix1.SetRow(3, new Vector4(0, 0, 0, 1));

        Matrix4x4 matrixX = new Matrix4x4();

        matrixX.SetRow(0, new Vector4(1, 0, 0, 0));
        matrixX.SetRow(1, new Vector4(0, Mathf.Cos(angle.x), -Mathf.Sin(angle.x), 0));
        matrixX.SetRow(2, new Vector4(0, Mathf.Sin(angle.x), Mathf.Cos(angle.x), 0));
        matrixX.SetRow(3, new Vector4(0, 0, 0, 1));

        Matrix4x4 matrixY = new Matrix4x4();

        matrixY.SetRow(0, new Vector4(Mathf.Cos(angle.y), 0, Mathf.Sin(angle.y), 0));
        matrixY.SetRow(1, new Vector4(0, 1, 0, 0));
        matrixY.SetRow(2, new Vector4(-Mathf.Sin(angle.y), 0, Mathf.Cos(angle.y), 0));
        matrixY.SetRow(3, new Vector4(0, 0, 0, 1));

        Matrix4x4 matrixZ = new Matrix4x4();

        matrixZ.SetRow(0, new Vector4(Mathf.Cos(angle.z), -Mathf.Sin(angle.z), 0, 0));
        matrixZ.SetRow(1, new Vector4(Mathf.Sin(angle.z), Mathf.Cos(angle.z), 0, 0));
        matrixZ.SetRow(2, new Vector4(0, 0, 1, 0));
        matrixZ.SetRow(3, new Vector4(0, 0, 0, 1));





        Matrix4x4 matrix3 = new Matrix4x4();

        matrix3.SetRow(0, new Vector4(1, 0, 0, t2.x));
        matrix3.SetRow(1, new Vector4(0, 1, 0, t2.y));
        matrix3.SetRow(2, new Vector4(0, 0, 1, t2.z));
        matrix3.SetRow(3, new Vector4(0, 0, 0, 1));



        Matrix4x4 finalmatrix = new Matrix4x4();
        finalmatrix = matrix1 * matrixX * matrixY * matrixZ * matrix3;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = finalmatrix.MultiplyPoint(vertices[i]);
        }
        mesh.vertices = vertices;
    }


}
