using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour
{
    public World world;
    private Texture2D frameBuffer;
    private float leftplane = 5;
    private float rightplane = -5;
    private float topplane = 5;
    private float bottonplane = -5;
    private float nearplane = -1;
    private float farplane = -11;

    // Start is called before the first frame update
    public Vector3 eye = new Vector3(0,0,0);
    public Vector3 gaze = new Vector3(0,0,-1);
    public Vector3 up = new Vector3(0,1,0);
    void Start()
    {
        frameBuffer = new Texture2D(Screen.width, Screen.height);
        ClearBuffer();
    }

    private void ClearBuffer()
    {
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++)
            {
                frameBuffer.SetPixel(x, y, Color.black);
            }
        }
        frameBuffer.Apply();
    }

    Vector4 MultiplyPoint(Matrix4x4 matrix, Vector4 point)
    {
        Vector4 result = new Vector4();

        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                result[r] += matrix[r, c] * point[c];
            }
        }
        return result;
    }

    void DrawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color col)
    {
        int dy = (int)(y1 - y0);
        int dx = (int)(x1 - x0);
        int stepx, stepy;

        if (dy < 0) { dy = -dy; stepy = -1; }
        else { stepy = 1; }
        if (dx < 0) { dx = -dx; stepx = -1; }
        else { stepx = 1; }
        dy <<= 1;
        dx <<= 1;

        float fraction = 0;

        tex.SetPixel(x0, y0, col);
        if (dx > dy)
        {
            fraction = dy - (dx >> 1);
            while (Mathf.Abs(x0 - x1) > 1)
            {
                if (fraction >= 0)
                {
                    y0 += stepy;
                    fraction -= dx;
                }
                x0 += stepx;
                fraction += dy;
                tex.SetPixel(x0, y0, col);
            }
        }
        else
        {
            fraction = dx - (dy >> 1);
            while (Mathf.Abs(y0 - y1) > 1)
            {
                if (fraction >= 0)
                {
                    x0 += stepx;
                    fraction -= dy;
                }
                y0 += stepy;
                fraction += dx;
                tex.SetPixel(x0, y0, col);
            }
        }
    }

    void UpdateViewVolume(Vector3 e)
    {
        nearplane = e.z - 3;
        farplane = e.z - 13;
        rightplane = e.x - 5;
        leftplane = e.x + 5;
        topplane = e.y + 5;
        bottonplane = e.y - 5;
    }

    private void OnGUI()
    {
        UpdateViewVolume(eye);
        ClearBuffer();

        Matrix4x4 mvp = new Matrix4x4(); 
        mvp.SetRow(0, new Vector4(Screen.width / 2, 0,0, (Screen.width - 1) / 2)); 
        mvp.SetRow(1, new Vector4(0,Screen.height / 2, 0, (Screen.height - 1) / 2)); 
        mvp.SetRow(2, new Vector4(0,0,1, 0)); 
        mvp.SetRow(3, new Vector4(0,0,0, 1)); 
        
        Matrix4x4 morth = new Matrix4x4(); 
        morth.SetRow(0, new Vector4(2/(rightplane - leftplane), 0,0,-((rightplane + leftplane) / (rightplane - leftplane)))); 
        morth.SetRow(1, new Vector4(0,2/(topplane - bottonplane),0,-((topplane + bottonplane) / (topplane - bottonplane)))); 
        morth.SetRow(2, new Vector4(0,0,2/(nearplane - farplane),-((nearplane + farplane) / (nearplane - farplane)))); 
        morth.SetRow(3, new Vector4(0,0,0,1));

        Vector3 w = -gaze.normalized;
        Vector3 u = Vector3.Cross(up, w).normalized;
        Vector3 v = Vector3.Cross(w,u);

        Matrix4x4 mcam = new Matrix4x4();
        mcam.SetRow(0, new Vector4(u.x,u.y,u.z,-((u.x * eye.x) + (u.y*eye.y) + (u.z * eye.z))));
        mcam.SetRow(1, new Vector4(v.x,v.y,v.z,-((v.x * eye.x) + (v.y*eye.y) + (v.z * eye.z))));
        mcam.SetRow(2, new Vector4(w.x, w.y, w.z, -((w.x * eye.x) + (w.y * eye.y) + (w.z * eye.z))));
        mcam.SetRow(3,new Vector4(0, 0, 0, 1));

        Matrix4x4 mp = new Matrix4x4();
        mp.SetRow(0, new Vector4(nearplane,0,0,0));
        mp.SetRow(1, new Vector4(0, nearplane, 0, 0));
        mp.SetRow(2, new Vector4(0, 0, nearplane + farplane, -(farplane*nearplane)));
        mp.SetRow(3, new Vector4(0, 0, 1, 0));

        Matrix4x4 mper = new Matrix4x4();
        //mper.SetRow(0, Vector4());


        //(morth * mp) é o MorthP no assignment
        Matrix4x4 final = mvp * ((morth * mp) * mcam);




        //Matrix4x4 final = mvp * (morth * mcam);

        for (int i = 0; i < world.lines.Length; i += 2)
        {
            Vector4 p1 = MultiplyPoint(final, new Vector4(world.vertices[world.lines[i]].x, world.vertices[world.lines[i]].y, world.vertices[world.lines[i]].z, 1));
            Vector4 p2 = MultiplyPoint(final, new Vector4(world.vertices[world.lines[i + 1]].x, world.vertices[world.lines[i + 1]].y, world.vertices[world.lines[i + 1]].z, 1));
            DrawLine(frameBuffer, (int)(p1.x / p1.w), (int)(p1.y / p1.w), (int)(p2.x / p2.w), (int)(p2.y / p2.w), Color.white);
        }
        frameBuffer.Apply();

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), frameBuffer);
    }
}
