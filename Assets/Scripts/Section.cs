﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Section : MonoBehaviour
{
	public List<Transform> neighbors = new List<Transform>();
	public float mag;
	public int index;
	public List<int> resources = new List<int>();

	void Start()
	{
		for (int i = 0; i < (int)Resource.Count; i++)
		{
			resources.Add(0);
		}
	}

	void OnTriggerEnter(Collider neighbor)
	{
		if (neighbor.GetComponent<Section>() != null)
			neighbors.Add(neighbor.transform);
	}
	void OnTriggerExit(Collider neighbor)
	{
		if (neighbor.GetComponent<Section>() != null)
			neighbors.Remove(neighbor.transform);
	}

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
		mag = transform.position.magnitude;
		if (GameObject.Find("Asteroid").GetComponent<Asteroid>().flo)
		{
			float radius = GameObject.Find("Asteroid").GetComponent<Asteroid>().radius;
            Ray ray = new Ray(transform.position, -transform.position);
			RaycastHit hit;
			int mask = 1 << 8;
			if (Physics.Raycast(ray, out hit, 100, mask))
			{
				transform.GetChild(0).position = transform.position.normalized * (radius + 0.5f + (hit.point.magnitude - radius));
			}
		}
	}

	public void SetRadius(float rad)
	{
		GetComponent<SphereCollider>().radius = rad;
	}
}