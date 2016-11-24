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
		//StartCoroutine(FindSphereSize());
	}

	public void AddSection(GameObject prefab)
	{
		Debug.Log("ERTE");
		GameObject currentTest = Instantiate(prefab) as GameObject;
		sections.Add(currentTest.GetComponent<Section>());
		currentTest.transform.parent = transform;
		currentTest.GetComponent<Section>().MoveInstant(FindWorldLoc(sections.Count - 1, sphereSize));
		//UpdateSections();
	}

	public void RemoveSection(int i)
	{
		Section section = sections[i];
		sections.RemoveAt(i);
		Destroy(section);
		UpdateSections();
	}

	public void Swap(Section sec1, Section sec2)
	{
		int index1 = sections.IndexOf(sec1);
		int index2 = sections.IndexOf(sec2);
		sections.RemoveAt(index1);
		sections.Insert(index1, sec2);
		sections.RemoveAt(index2);
		sections.Insert(index2, sec1);
		Vector3 loc1 = sec1.transform.position;
		Vector3 loc2 = sec2.transform.position;
		StartCoroutine(sec2.GetComponent<Section>().Move(loc1));
		StartCoroutine(sec1.GetComponent<Section>().Move(loc2));
		//UpdateSections();
	}

	public void UpdateSections()
	{
		for (int i = 0; i <= sections.Count - 1; i++)
		{
			sections[i].CheckNeighbors();
		}
	}

	public Vector3 FindWorldLoc(int index, int sectionCount)
	{
		radius = GameObject.Find("Asteroid").GetComponent<Asteroid>().radius + 0.5f;
		float offset = 2 / (float)sectionCount;
		float increment = Mathf.PI * (3 - Mathf.Sqrt(5));
		float y = ((index * offset) - 1) + (offset / 2);
		float r = Mathf.Sqrt(1 - (y * y));
		float phi = (index % sectionCount - 1) * increment;
		float x = Mathf.Cos(phi) * r;
		float z = Mathf.Sin(phi) * r;
		Vector3 loc = new Vector3(x, y, z);
		//sections[i].transform.position = loc * radius;
		return loc * radius;
		
	}

	//public IEnumerator FindSphereSize()
	//{
	//	touch = false;
	//	int secCount = sections.Count;

	//	while(!touch)
	//	{
	//		sphereSize = secCount;
	//		UpdateSections();
	//		yield return null;
	//		foreach(Section section in sections)
	//		{
				
	//			Collider[] cols = Physics.OverlapSphere(section.transform.position, 0.5f);
	//			foreach (Collider col in cols)
	//			{
	//				if (col.GetComponent<Section>() != null && col.GetComponent<Section>() != section)
	//					touch = true;
	//			}
	//		}
	//		if (!touch)
	//			secCount++;
	//		else secCount--;

	//		yield return null;
	//	}
	//}
}
