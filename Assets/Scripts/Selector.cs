using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour
{
	public GameObject sel1;
	public GameObject sel2;

	public Material selected;
	public Material neighbor;
	public Material def;
	
	void Update()
	{
		if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if(hit.transform.GetComponent<Island>() != null)
				{
					if (sel1 == null)
					{
						sel1 = hit.transform.gameObject;
						sel1.transform.GetChild(0).GetComponent<Renderer>().material = selected;
						foreach(Transform neigh in sel1.GetComponent<Island>().neighbors)
						{
							neigh.transform.GetChild(0).GetComponent<Renderer>().material = neighbor;
						}
					}
					else if (sel1 != null && sel2 == null && sel1.GetComponent<Island>().neighbors.Contains(hit.transform))
					{
						sel1.transform.GetChild(0).GetComponent<Renderer>().material = def;
						foreach (Transform neigh in sel1.GetComponent<Island>().neighbors)
						{
							neigh.transform.GetChild(0).GetComponent<Renderer>().material = def;
						}
						sel2 = hit.transform.gameObject;
						Vector3 loc1 = sel1.transform.position;
						Vector3 loc2 = sel2.transform.position;
						StartCoroutine(sel2.GetComponent<Island>().Move(loc1));
						StartCoroutine(sel1.GetComponent<Island>().Move(loc2));
						sel1 = null;
						sel2 = null;

					}
					
				}
			}
		}

	}
}
