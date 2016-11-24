using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour
{
	public Section sel1;
	public Section sel2;

	FibonacciSphere planet;

	void Start()
	{
		planet = GameObject.Find("Planet").GetComponent<FibonacciSphere>();
	}

	void Update()
	{
		if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit) && hit.transform.GetComponent<Section>() != null)
			{
				if (sel1 == null)
				{
					sel1 = hit.transform.GetComponent<Section>();
					transform.position = sel1.transform.position;
					//Camera.main.transform.root.GetComponent<CameraControlScript>().FocusCamera(transform.position);
				}
				else if (sel1 != null && sel2 == null && sel1.GetComponent<Section>().neighbors.Contains(hit.transform.GetComponent<Section>()))
				{
					sel2 = hit.transform.GetComponent<Section>();
					planet.Swap(sel1, sel2);
					Deselect();
				}
				
			}
			else
				Deselect();
		}

	}

	void Swap()
	{
		Vector3 loc1 = sel1.transform.position;
		Vector3 loc2 = sel2.transform.position;
		StartCoroutine(sel2.GetComponent<Section>().Move(loc1));
		StartCoroutine(sel1.GetComponent<Section>().Move(loc2));
		Deselect();
		GameObject.Find("Planet").GetComponent<FibonacciSphere>().UpdateSections();
	}

	void Deselect()
	{
		sel1 = null;
		sel2 = null;
		transform.position = Vector3.zero;
	}
}
