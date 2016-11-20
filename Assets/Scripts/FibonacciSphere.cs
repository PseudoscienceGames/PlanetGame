using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FibonacciSphere : MonoBehaviour
{
	public GameObject test;
	public float radius;
	public List<Section> sections = new List<Section>();
	//public float inc;
	public bool add;
	public bool go;
	public bool update;
	public int at;
	public float lastRadius;
	public int sphereSize;
	public bool touch;

	void Update()
	{
		if(go)
		{
			go = false;
			AddSection();
		}
		if(update)
		{
			update = false;
			StartCoroutine(FindSphereSize());
//			UpdateSections();
		}
	}

	public void AddSection()
	{
		GameObject currentTest = Instantiate(test) as GameObject;
		sections.Add(currentTest.GetComponent<Section>());
		UpdateSections();
	}

	public void RemoveSection(int i)
	{
		Section section = sections[i];
		sections.RemoveAt(i);
		Destroy(section);
		UpdateSections();
	}

	public void UpdateSections()
	{

		//StopAllCoroutines();
		radius = GameObject.Find("Asteroid").GetComponent<Asteroid>().radius + 0.5f;
		float offset = 2 / (float)sphereSize;
		float increment = Mathf.PI * (3 - Mathf.Sqrt(5));
		for (int i = 0; i <= sections.Count - 1; i++)
		{
			float y = ((i * offset) - 1) + (offset / 2);
			float r = Mathf.Sqrt(1 - (y * y));
			float phi = (i % sphereSize - 1) * increment;
			float x = Mathf.Cos(phi) * r;
			float z = Mathf.Sin(phi) * r;
			Vector3 loc = new Vector3(x, y, z);
			//sections[i].transform.position = loc * radius;
            sections[i].MoveInstant(loc * radius);
		}
	}

	public IEnumerator FindSphereSize()
	{
		touch = false;
		int secCount = sections.Count;

		while(!touch)
		{
			sphereSize = secCount;
			UpdateSections();
			yield return null;
			foreach(Section section in sections)
			{
				
				Collider[] cols = Physics.OverlapSphere(section.transform.position, 0.5f);
				foreach (Collider col in cols)
				{
					if (col.GetComponent<Section>() != null && col.GetComponent<Section>() != section)
						touch = true;
				}
			}
			if (!touch)
				secCount++;
			else secCount--;
				yield return null;
		}

		foreach(Section section in sections)
		{
			section.CheckNeighbors();
		}
	}
}
