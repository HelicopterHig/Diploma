using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class AllNoiseMapGeneration : MonoBehaviour
{
    public float seedall;
    public float frequencyall;
    public float amplitudeall;
    public float[,] AllGenerateNoiseMap(int mapDepth, int mapWidth, float scale, float offsetX, float offsetZ, Waveall[] waves)
    {

        float[,] allnoiseMap = new float[mapDepth, mapWidth];

        System.Random rnd1 = new System.Random();
        seedall = rnd1.Next(1111, 9999);

        System.Random rnd = new System.Random();
        amplitudeall = rnd.Next(1, 5);

        System.Random rnd2 = new System.Random();
        frequencyall = (float)rnd2.Next(1, 9) / 10f;

        for (int zIndex = 0; zIndex < mapDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < mapWidth; xIndex++)
            {

                float sampleX = (xIndex + offsetX) / scale;
                float sampleZ = (zIndex + offsetZ) / scale;


                float allnoise = 0f;
                float normalization = 0f;


                allnoise += amplitudeall * Mathf.PerlinNoise(sampleX * frequencyall + seedall, sampleZ * frequencyall + seedall);
                normalization += amplitudeall;
                Debug.Log(amplitudeall);

                allnoise /= normalization;


                allnoiseMap[zIndex, xIndex] = allnoise;
            }
        }

        return allnoiseMap;
    }

}
public class Waveall
{

}