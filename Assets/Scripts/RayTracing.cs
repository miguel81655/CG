using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracing : MonoBehaviour
{
    private int l = -1;
    private int r = 1;
    private int b = -1;
    private int t = 1;

    public Light light;
    private Texture2D renderBuffer;
    private bool optimize = false;
    void Start()
    {
        renderBuffer = new Texture2D(Screen.width, Screen.height);
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++)
            {
                Color color = Color.black;
                /*int n =Random.Range(0, 2);
              
                if (n > 0) color = Color.white;*/
                renderBuffer.SetPixel(x, y, color);
            }
        }
        renderBuffer.Apply();

    }
    private Color TraceRay(Ray ray)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Material mat = hit.transform.gameObject.GetComponent<Renderer>().material;
            Vector3 l = Vector3.Normalize(light.transform.position - hit.point);
            float r = mat.color.r * light.intensity * Mathf.Max(0, Vector3.Dot(hit.normal, l));
            float g = mat.color.g * light.intensity * Mathf.Max(0, Vector3.Dot(hit.normal, l));
            float b = mat.color.b * light.intensity * Mathf.Max(0, Vector3.Dot(hit.normal, l));
            return new Color(r,g,b);
        }
        return Color.black;
    }
    private void RayTracer()
    {
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++)
            {
                //float u = l + (r - l) * (x + 0.5f) / Screen.width;
                //float v = b + (t - b) * (y + 0.5f) / Screen.height;
                Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y, 0));
                //Ray ray = new Ray(new Vector3(u, v, 0), transform.forward);
                renderBuffer.SetPixel(x, y, TraceRay(ray));
            }
        }
        renderBuffer.Apply();
    }
    private void Update()
    {
        if (!optimize)
            RayTracer();
        if (Input.GetKeyDown(KeyCode.K)) if (optimize) optimize = false; else optimize = true;
    }
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), renderBuffer);
    }


}
