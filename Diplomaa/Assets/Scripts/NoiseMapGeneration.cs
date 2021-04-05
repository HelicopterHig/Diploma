using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEngine.Random;

public class NoiseMapGeneration : MonoBehaviour
{
	public float seed;
	public float frequency;
	public float amplitude;



	public float[,] GenerateNoiseMap(int mapDepth, int mapWidth, float scale, float offsetX, float offsetZ, Wave[] waves)
	{
		//создать пустую карту шума с координатами mapDepth и mapWidth
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
				// рассчитывать индексы выборки на основе координат, масштаба и смещения
				float sampleX = (xIndex + offsetX) / scale;
				float sampleZ = (zIndex + offsetZ) / scale;
				//float amplitude;

				float noise = 0f;
				float normalization = 0f;

				foreach (Wave wave in waves)
				{
					// генерировать значение шума с помощью PerlinNoise для данной волны

					//int single = rnd.Next(1, 10);
					//int[] bog = { 1,2,3,4,5 };
					//amplitude = rnd.Next(1, 5);

					//seed = rnd1.Next(1111, 9999);
					//int frequency = (int)frequencyFloat;
					noise += amplitude * Mathf.PerlinNoise(sampleX * frequency + seed, sampleZ * frequency + seed);
					normalization += amplitude;
					Debug.Log(amplitude);
				}
				// нормализовать значение шума так, чтобы оно находилось в пределах от 0 до 1
				noise /= normalization;


				noiseMap[zIndex, xIndex] = noise;
			}
		}

		return noiseMap;
	}
}

[System.Serializable]
public class Wave
{
	//public float seed;
	//public float frequency;
	//public float amplitude;


}

/*static void Main(string[] args)
{
	int[] bog = { 9, 2, 3, 1, 1, 2, 1, 1, 1, 1 };
	Random rnd = new Random();
	for (int i = 0; i < bog.Length; i++)
	{
		Console.WriteLine(rnd.Next(bog[i]));
		Console.ReadKey();

	}
	*/