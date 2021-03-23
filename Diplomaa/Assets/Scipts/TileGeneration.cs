using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{

	[SerializeField]
	NoiseMapGeneration noiseMapGeneration;

	[SerializeField]
	private MeshRenderer tileRenderer;

	[SerializeField]
	private MeshFilter meshFilter;

	[SerializeField]
	private MeshCollider meshCollider;

	[SerializeField]
	private float levelScale;

	[SerializeField]
	private TerrainType[] terrainTypes;

	[SerializeField]
	private float heightMultiplier;

	[SerializeField]
	private AnimationCurve heightCurve;

	[SerializeField]
	private Wave[] waves;

	void Start()
	{
		GenerateTile();
	}

	void GenerateTile()
	{
		//рассчитать глубину и ширину плитки на основе вершин сетки 
		Vector3[] meshVertices = this.meshFilter.mesh.vertices;
		int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
		int tileWidth = tileDepth;

		// рассчитать смещения на основе положения плитки
		float offsetX = -this.gameObject.transform.position.x;
		float offsetZ = -this.gameObject.transform.position.z;

		// сгенерировать heightMap с использованием шума
		float[,] heightMap = this.noiseMapGeneration.GenerateNoiseMap(tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, waves);

		// построить Texture2D из карты высот
		Texture2D tileTexture = BuildTexture(heightMap);
		this.tileRenderer.material.mainTexture = tileTexture;

		//	обновить вершины мозаичной сетки в соответствии с картой высот
		UpdateMeshVertices(heightMap);
	}

	private Texture2D BuildTexture(float[,] heightMap)
	{
		int tileDepth = heightMap.GetLength(0);
		int tileWidth = heightMap.GetLength(1);

		Color[] colorMap = new Color[tileDepth * tileWidth];
		for (int zIndex = 0; zIndex < tileDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < tileWidth; xIndex++)
			{
				// преобразовать индекс 2D-карты в индекс массива
				int colorIndex = zIndex * tileWidth + xIndex;
				float height = heightMap[zIndex, xIndex];
				// выберите тип местности в соответствии со значением высоты
				TerrainType terrainType = ChooseTerrainType(height);
				//назначить цвет в соответствии с типом местности
				colorMap[colorIndex] = terrainType.color;
			}
		}

		//создать новую текстуру и установить цвета ее пикселей
		Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
		tileTexture.wrapMode = TextureWrapMode.Clamp;
		tileTexture.SetPixels(colorMap);
		tileTexture.Apply();

		return tileTexture;
	}

	TerrainType ChooseTerrainType(float height)
	{
		// для каждого типа местности проверьте, не ниже ли высота, чем высота для данного типа местности
		foreach (TerrainType terrainType in terrainTypes)
		{
			// вернуть первый тип ландшафта, высота которого выше, чем сгенерированный
			if (height < terrainType.height)
			{
				return terrainType;
			}
		}
		return terrainTypes[0];
	}

	private void UpdateMeshVertices(float[,] heightMap)
	{
		int tileDepth = heightMap.GetLength(0);
		int tileWidth = heightMap.GetLength(1);

		Vector3[] meshVertices = this.meshFilter.mesh.vertices;

		//перебирать все координаты heightMap, обновляя индекс вершины
		int vertexIndex = 0;
		for (int zIndex = 0; zIndex < tileDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < tileWidth; xIndex++)
			{
				float height = heightMap[zIndex, xIndex];

				Vector3 vertex = meshVertices[vertexIndex];
				//изменить координату Y вершины пропорционально значению высоты. Значение высоты оценивается функцией heightCurve, чтобы исправить его.
				meshVertices[vertexIndex] = new Vector3(vertex.x, this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z);

				vertexIndex++;
			}
		}

		// обновить вершины в сетке и обновить ее свойства
		this.meshFilter.mesh.vertices = meshVertices;
		this.meshFilter.mesh.RecalculateBounds();
		this.meshFilter.mesh.RecalculateNormals();
		//обновить коллайдер сетки
		this.meshCollider.sharedMesh = this.meshFilter.mesh;
	}
}

[System.Serializable]
public class TerrainType
{
	public string name;
	public float height;
	public Color color;
}
