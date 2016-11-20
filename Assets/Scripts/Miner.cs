using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Miner : Section
{
	public Asteroid asteroid;
	public float speed;

	void Start()
	{
		asteroid = GameObject.Find("Asteroid").GetComponent<Asteroid>();
		InvokeRepeating("Mine", 0, speed);
	}

	void Mine()
	{
		if (outputs.Count > 0)
		{
			Resource rec = asteroid.Mine();
			Send(rec, outputs[0]);
		}
	}
}
