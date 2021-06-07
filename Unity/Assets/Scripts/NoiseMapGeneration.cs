using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class NoiseMapGeneration : MonoBehaviour
{
    public float seed;
    public float frequency;
    public float amplitude;
    public float[,] GenerateNoiseMap(int mapDepth, int mapWidth, float scale, float offsetX, float offsetZ, Wave[] waves)
    {

        float[,] noiseMap = new float[mapDepth, mapWidth];

        System.Random rnd1 = new System.Random();
        seed = rnd1.Next(1111, 9999);

        System.Random rnd = new System.Random();
        amplitude = rnd.Next(1, 5);

        System.Random rnd2 = new System.Random();
        frequency = (float)rnd2.Next(1, 9) / 10f;

        for (int zIndex = 0; zIndex < mapDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < mapWidth; xIndex++)
            {

                float sampleX = (xIndex + offsetX) / scale;
                float sampleZ = (zIndex + offsetZ) / scale;


                float noise = 0f;
                float normalization = 0f;


                noise += amplitude * Mathf.PerlinNoise(sampleX * frequency + seed, sampleZ * frequency + seed);
                normalization += amplitude;
                Debug.Log(amplitude);

                noise /= normalization;


                noiseMap[zIndex, xIndex] = noise;
            }
        }

        return noiseMap;
    }

}
public class Wave
{

}