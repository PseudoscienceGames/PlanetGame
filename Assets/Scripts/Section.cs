using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Section : MonoBehaviour
{
	public List<Section> neighbors = new List<Section>();
	public List<int> resources = new List<int>();
	public List<Section> inputs = new List<Section>();
	public List<Section> outputs = new List<Section>();
	public bool ready;

	void Start()
	{
		for (int i = 0; i < (int)Resource.Count; i++)
		{
			resources.Add(0);
		}
	}

	//void OnTriggerEnter(Collider neighbor)
	//{
	//	if (neighbor.GetComponent<Section>() != null)
	//		neighbors.Add(neighbor.transform);
	//	else Debug.Log(neighbor.transform.name);
	//}
	//void OnTriggerExit(Collider neighbor)
	//{
	//	if (neighbor.GetComponent<Section>() != null)
	//		neighbors.Remove(neighbor.transform);
	//}

	public void MoveInstant(Vector3 pos)
	{
		transform.position = pos;
		transform.LookAt(Vector3.zero);
	}

	public IEnumerator Move(Vector3 loc)
	{
		float time = 0;
		Vector3 oldLoc = transform.position;
		while (transform.position != loc)
		{
			time += Time.deltaTime * 10f;
			transform.position = Vector3.Lerp(oldLoc, loc, time);
			transform.LookAt(Vector3.zero);
			yield return null;
		}
		yield return null;
    }

	void Update()
	{
		if (GameObject.Find("Asteroid").GetComponent<Asteroid>().flo)
		{
			float radius = GameObject.Find("Asteroid").GetComponent<Asteroid>().radius;
            Ray ray = new Ray(transform.position, -transform.position);
			RaycastHit hit;
			int mask = 1 << 8;
			if (Physics.Raycast(ray, out hit, 100, mask))
			{
				transform.GetChild(0).position = transform.position.normalized * (hit.point.magnitude + 0.5f);
			}
		}
	}

	public void SetRadius(float rad)
	{
		GetComponent<SphereCollider>().radius = rad;
	}

	public void Send(Resource rec, Section to)
	{
		if(to.ready)
			to.Recieve(rec);
		else
		{
			Debug.Log(to.name + " not ready");
		}
	}

	public virtual void Recieve(Resource rec)
	{

	}

	public void CheckNeighbors()
	{
		Collider[] cols = Physics.OverlapSphere(transform.position, 1);
		neighbors.Clear();
		foreach(Collider col in cols)
		{
			if (col.GetComponent<Section>() != null && col.GetComponent<Section>() != this)
				neighbors.Add(col.GetComponent<Section>());
		}
	}
}
