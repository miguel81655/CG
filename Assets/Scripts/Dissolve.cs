using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private Renderer renderer;
    private float thershold;
    
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        thershold = 0;
    }
    private void Update()
    {
        if (thershold < 1)
        {
            thershold += Time.deltaTime / 10;
            renderer.material.SetFloat("_DissolveThershold", thershold);
        }
    }
    
}
