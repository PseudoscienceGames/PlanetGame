using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Miner : MonoBehaviour
{
	public Asteroid asteroid;
	public float speed;

	public Dictionary<Resource, float> resources = new Dictionary<Resource, float>();

	void Start()
	{
		asteroid = GameObject.Find("Asteroid").GetComponent<Asteroid>();
		InvokeRepeating("Mine", 0, 1);
	}

	void Mine()
	{
		Dictionary<Resource, float> minedResources = asteroid.Mine(speed);
		foreach(Resource rec in minedResources.Keys)
		{
			if (resources.ContainsKey(rec))
				resources[rec] += minedResources[rec];
			else
				resources.Add(rec, minedResources[rec]);
		}
	}
}
