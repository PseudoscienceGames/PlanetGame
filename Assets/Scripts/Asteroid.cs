using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour
{
	public Vector3 rot;

	public Dictionary<Resource, float> resources = new Dictionary<Resource, float>();

	void Start()
	{
		for(int i = 0; i < (int)Resource.Count; i++)
		{
			resources.Add((Resource)i, Random.Range(0f, 1000f));
		}
	}


	void Update ()
	{
		transform.rotation *= Quaternion.AngleAxis(rot.magnitude * Time.deltaTime, rot);
	}

	public Dictionary<Resource, float> Mine(float speed)
	{
		float total = 0;
		int bre = 0;
		Dictionary<Resource, float> minedResources = new Dictionary<Resource, float>();
		while (total < speed && bre < 10)
		{
			bre++;
			Resource randResource = (Resource)Random.Range(0, (int)Resource.Count);
			float amt;
			if (resources[randResource] > 0)
			{
				amt = Random.Range(0f, speed);
				if (amt > speed - total)
					amt = speed - total;
				if (amt > resources[randResource])
					amt = resources[randResource];
				if (minedResources.ContainsKey(randResource))
					minedResources[randResource] += amt;
				else
					minedResources.Add(randResource, amt);
				resources[randResource] -= amt;
			}
		}
		string resourceCount = "";

		foreach (Resource rec in resources.Keys)
			resourceCount += "(" + rec + ": " + resources[rec] + ")";
		Debug.Log(resourceCount);
		return minedResources;
	}
}

public enum Resource { Al, Ni, Fe, Co, N, H, Pt, C, Count};
