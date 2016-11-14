using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FibonacciSphere : MonoBehaviour
{
	public GameObject test;
	public int samples;
	public float radius;
	public List<GameObject> islands = new List<GameObject>();
	//public float inc;

	public bool go;
	public float lastRadius;

	//void Update()
	//{
	//	if (go)
	//	{
	//		s++;
	//		//radius += inc;
	//		FindChunkStartingTiles(s);
	//		go = false;
	//	}
	//	if (radius <= 0)
	//		radius = 0.01f;
	//	if (lastRadius != radius)
	//	{
	//		foreach (GameObject i in islands)
	//		{
	//			i.transform.position = i.transform.position.normalized * radius;
	//		}
	//		lastRadius = radius;
	//	}
	//}

	void Start()
	{
		InvokeRepeating("FindChunkStartingTiles", .1f, .11f);
	}

	public void FindChunkStartingTiles()
	{
		StopAllCoroutines();
		samples += 1;
		float a = .5f;
		float b = .5f;
		radius = a * Mathf.Sqrt(b * samples);
		float offset = 2 / (float)samples;
		float increment = Mathf.PI * (3 - Mathf.Sqrt(5));
		for (int i = 0; i <= samples - 1; i++)
		{
			float y = ((i * offset) - 1) + (offset / 2);
			float r = Mathf.Sqrt(1 - (y * y));
			float phi = (i % samples) * increment;
			float x = Mathf.Cos(phi) * r;
			float z = Mathf.Sin(phi) * r;
			Vector3 loc = new Vector3(x, y, z);
			if (i < islands.Count)
				StartCoroutine(islands[i].GetComponent<Island>().Move(loc * radius));
			else
			{
				GameObject currentTest = Instantiate(test) as GameObject;
				StartCoroutine(currentTest.GetComponent<Island>().Move(loc * radius));
				islands.Add(currentTest);
			}
			islands[i].transform.LookAt(Vector3.zero);
		}
	}
}
