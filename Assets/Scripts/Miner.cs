using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Miner : Section
{
	public Asteroid asteroid;
	public float speed;
	public GameObject output;

	void Start()
	{
		asteroid = GameObject.Find("Asteroid").GetComponent<Asteroid>();
		InvokeRepeating("Mine", 0, speed);
	}

	void Mine()
	{
		Resource rec = asteroid.Mine();
		GetComponent<Section>().resources[(int)rec]++;
	}
}
