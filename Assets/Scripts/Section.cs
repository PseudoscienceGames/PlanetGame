using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Section : MonoBehaviour
{
	public List<Transform> neighbors = new List<Transform>();
	public float mag;
	public int index;

	void OnTriggerEnter(Collider neighbor)
	{
		neighbors.Add(neighbor.transform);
	}
	void OnTriggerExit(Collider neighbor)
	{
		neighbors.Remove(neighbor.transform);
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
	}
}
