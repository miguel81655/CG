using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform2D : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    void Start()
    {
        
        vertices = new Vector3[3];
        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "test";
        vertices[0] = new Vector3(2, 0, 1);
        vertices[1] = new Vector3(-2, 0, 1);
        vertices[2] = new Vector3(0, 4, 1);
        mesh.vertices = vertices;
        int[] triangles = new int[3];
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        mesh.triangles = triangles;
        RotateScale(45 * Mathf.Deg2Rad, 2, 1);

    }
    void Translate2D(float tx, float ty)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices[i].x + tx, vertices[i].y + ty, vertices[i].z);
        }
        mesh.vertices = vertices;
    }
    void Scale2D(float tx, float ty)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices[i].x * tx, vertices[i].y * ty, vertices[i].z);
        }
        mesh.vertices = vertices;
    }
    void Rotate2D(float angle)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices[i].x * Mathf.Cos(angle)- vertices[i].y * Mathf.Sin(angle), vertices[i].x * Mathf.Sin(angle) + vertices[i].y * Mathf.Cos(angle), vertices[i].z);
        }
        mesh.vertices = vertices;
    }
    Vector3 multiply(float[,] matrix, Vector3 vertex)
    {
        Vector3 result = new Vector3();
        for(int r = 0; r < matrix.GetLength(0); r++)
        {
            for(int c = 0; c < matrix.GetLength(1); c++)
            {
                result[r] += matrix[r, c] * vertex[c];
            }
        }
        return result;
    }
    float[,] multiply(float[,] matrix, float[,] matrix2)
    {
        float[,] result = new float[matrix.GetLength(0), matrix2.GetLength(1)];
        
        for (int r = 0; r < matrix.GetLength(0); r++)
        {
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                for (int c1 = 0; c1 < matrix.GetLength(1); c1++)
                {
                    result[r, c] += matrix[r, c1] * matrix2[c1,c];
                }
            }
        }
        return result;
    }
    void RotateScale(float angle, float tx, float ty) 
    {
        float[,] matrix = new float[3, 3];
        matrix[0, 0] = Mathf.Cos(angle); matrix[0, 1] = -Mathf.Sin(angle); matrix[0, 2] = 0;
        matrix[1, 0] = Mathf.Sin(angle); matrix[1, 1] = Mathf.Cos(angle); matrix[1, 2] = 0;
        matrix[2, 0] = 0; matrix[2, 1] = 0; matrix[2, 2] = 1;


        float[,] matrix2 = new float[3, 3];
        matrix2[0, 0] = tx; matrix2[0, 1] = 0; matrix2[0, 2] = 0;
        matrix2[1, 0] = 0; matrix2[1, 1] = ty; matrix2[1, 2] = 0;
        matrix2[2, 0] = 0; matrix2[2, 1] = 0; matrix2[2, 2] = 1;

        float[,] finalmatrix = multiply(matrix, matrix2);
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = multiply(finalmatrix, vertices[i]);
        }
        mesh.vertices = vertices;
    }
    void Scale2DMatrix(float sx, float sy)
    {
        float[,] matrix = new float[2, 2];
        matrix[0, 0] = sx; matrix[0, 1] = 0;
        matrix[1, 0] = 0; matrix[1, 1] = sy;
        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = multiply(matrix, vertices[i]);
        }
        mesh.vertices = vertices;
    }

    void Rotate2DMatrix(float angle)
    {
        float[,] matrix = new float[2, 2];
        matrix[0, 0] = Mathf.Cos(angle); matrix[0, 1] = - Mathf.Sin(angle);
        matrix[1, 0] = Mathf.Sin(angle); matrix[1, 1] = Mathf.Cos(angle);
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = multiply(matrix, vertices[i]);
        }
        mesh.vertices = vertices;
    }

    void Translate2DMatrix(float tx, float ty)
    {
        float[,] matrix = new float[3, 3];
        matrix[0, 0] = 1; matrix[0, 1] = 0; matrix[0, 2] = tx;
        matrix[1, 0] = 0; matrix[1, 1] = 1; matrix[1, 2] = ty;
        matrix[2, 0] = 0; matrix[2, 1] = 0; matrix[2, 2] = 1;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = multiply(matrix, vertices[i]);
        }
        mesh.vertices = vertices;
    }
    void Scale2DMatrixHM(float tx, float ty)
    {
        float[,] matrix = new float[3, 3];
        matrix[0, 0] = tx; matrix[0, 1] = 0; matrix[0, 2] = 0;
        matrix[1, 0] = 0; matrix[1, 1] = ty; matrix[1, 2] = 0;
        matrix[2, 0] = 0; matrix[2, 1] = 0; matrix[2, 2] = 1;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = multiply(matrix, vertices[i]);
        }
        mesh.vertices = vertices;
    }
    void Rotate2DMatrixHM(float angle)
    {
        float[,] matrix = new float[3, 3];
        matrix[0, 0] = Mathf.Cos(angle); matrix[0, 1] = -Mathf.Sin(angle); matrix[0, 2] = 0;
        matrix[1, 0] = Mathf.Sin(angle); matrix[1, 1] = Mathf.Cos(angle); matrix[1, 2] = 0;
        matrix[2, 0] = 0; matrix[2, 1] = 0; matrix[2, 2] = 1;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = multiply(matrix, vertices[i]);
        }
        mesh.vertices = vertices;
    }

    void RotateAroundPosition(float angle, float tx, float ty)
    {
        float[,] matrix = new float[3, 3];
        matrix[0, 0] = 1; matrix[0, 1] = 0; matrix[0, 2] = tx;
        matrix[1, 0] = 0; matrix[1, 1] = 1; matrix[1, 2] = ty;
        matrix[2, 0] = 0; matrix[2, 1] = 0; matrix[2, 2] = 1;

        float[,] matrix2 = new float[3, 3];
        matrix2[0, 0] = Mathf.Cos(angle); matrix2[0, 1] = -Mathf.Sin(angle); matrix2[0, 2] = 0;
        matrix2[1, 0] = Mathf.Sin(angle); matrix2[1, 1] = Mathf.Cos(angle); matrix2[1, 2] = 0;
        matrix2[2, 0] = 0; matrix2[2, 1] = 0; matrix2[2, 2] = 1;

        float[,] matrix3 = new float[3, 3];
        matrix3[0, 0] = 1; matrix3[0, 1] = 0; matrix3[0, 2] = tx;
        matrix3[1, 0] = 0; matrix3[1, 1] = 1; matrix3[1, 2] = ty;
        matrix3[2, 0] = 0; matrix3[2, 1] = 0; matrix3[2, 2] = 1;


        float[,] finalmatrix = multiply(multiply(matrix3, matrix2), matrix);
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = multiply(finalmatrix, vertices[i]);
        }
        mesh.vertices = vertices;
    }
    // Update is called once per frame
    void Update()
    {
        //Scale2D(1.0001f, 1);
        //Rotate2DMatrix(20 * Mathf.Deg2Rad * Time.deltaTime);
        //Translate2DMatrix(2 * Time.deltaTime, 0);
        //Rotate2DMatrix(20 * Time.deltaTime * Mathf.Deg2Rad);
        //Scale2DMatrixHM (1.001f, 1.001f);
        //Rotate2DMatrixHM(20 * Time.deltaTime * Mathf.Deg2Rad);
        RotateAroundPosition(20 * Mathf.Deg2Rad * Time.deltaTime, 0 , 0);
        
    }
}
