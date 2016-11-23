using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FibonacciSphere : MonoBehaviour
{
	public float radius;
	public List<Section> sections = new List<Section>();
	//public float inc;
	public int at;
	public float lastRadius;
	public int sphereSize;
	public bool touch;
	public List<GameObject> startingSections = new List<GameObject>();

	void Start()
	{
		foreach(GameObject prefab in startingSections)
		{
			AddSection(prefab);
		}
		StartCoroutine(FindSphereSize());
	}

	public void AddSection(GameObject prefab)
	{
		GameObject currentTest = Instantiate(prefab) as GameObject;
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
		if (sphereSize < sections.Count)
			sphereSize = sections.Count;
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
		foreach (Section section in sections)
		{
			section.CheckNeighbors();
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
