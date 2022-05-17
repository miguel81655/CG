using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralUnityTerrain : MonoBehaviour
{

    public float refinement = 0.01f;
    public float heightSclae = 0.04f;
    public float sampleOffset = 0;
    public int size = 100;
    public int resolution = 512;
    // Start is called before the first frame update
    void Start()
    {
        float[,] terrainHeights = new float[resolution,resolution];
        for(int i = 0; i < resolution; i++)
        {
            for(int j = 0; j < resolution; j++)
            {
                terrainHeights[i, j] = Mathf.PerlinNoise(sampleOffset + i * refinement, sampleOffset + j * refinement) * heightSclae;
            }
        }
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = new TerrainData();
        terrain.terrainData.size = new Vector3(size, size, size);
        terrain.terrainData.heightmapResolution = resolution;
        terrain.terrainData.SetHeights(0, 0, terrainHeights);

        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
        terrainCollider.terrainData = terrain.terrainData;
        
        
    }

    // Update is called once per frame
    
}
