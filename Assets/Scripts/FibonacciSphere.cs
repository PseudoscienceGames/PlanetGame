using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FibonacciSphere : MonoBehaviour
{
	public GameObject test;
	public float radius;
	public List<GameObject> sections = new List<GameObject>();
	//public float inc;
	public bool add;
	public bool go;
	public int at;
	public float lastRadius;

	void Update()
	{
		if(go)
		{
			go = false;
			if(add)
			{
				AddSection();
			}
			else
			{
				RemoveSection(at);
			}
		}
	}

	public void AddSection()
	{
		GameObject currentTest = Instantiate(test) as GameObject;
		sections.Add(currentTest);
		UpdateSections();
	}

	public void RemoveSection(int i)
	{
		GameObject section = sections[i];
		sections.RemoveAt(i);
		Destroy(section);
		UpdateSections();
	}

	public void UpdateSections()
	{
		StopAllCoroutines();
		float a = .5f;
		float b = .5f;
		radius = a * Mathf.Sqrt(b * sections.Count);
		float offset = 2 / (float)sections.Count;
		float increment = Mathf.PI * (3 - Mathf.Sqrt(5));
		for (int i = 0; i <= sections.Count - 1; i++)
		{
			float y = ((i * offset) - 1) + (offset / 2);
			float r = Mathf.Sqrt(1 - (y * y));
			float phi = (i % sections.Count - 1) * increment;
			float x = Mathf.Cos(phi) * r;
			float z = Mathf.Sin(phi) * r;
			Vector3 loc = new Vector3(x, y, z);
			StartCoroutine(sections[i].GetComponent<Island>().Move(loc * radius));
		}
	}
}
