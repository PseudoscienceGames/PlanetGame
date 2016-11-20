using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour
{
	public Vector3 rot;
	public float radius;
	public bool flo;

	public List<int> resources = new List<int>();

	void Start()
	{
		for(int i = 0; i < (int)Resource.Count; i++)
		{
			resources.Add(Random.Range(0, 1000));
		}
	}


	void Update ()
	{
		transform.rotation *= Quaternion.AngleAxis(rot.magnitude * Time.deltaTime, rot);
	}

	public Resource Mine()
	{
		List<int> pos = new List<int>();
		for (int i = 0; i < resources.Count; i++)
		{
			if (resources[i] > 0)
				pos.Add(i);
		}
		Resource rec = (Resource)pos[Random.Range(0, pos.Count)];
		resources[(int)rec]--;
		return rec;

	}
}

public enum Resource { Al, Ni, Fe, Co, N, H, Pt, C, Count};
