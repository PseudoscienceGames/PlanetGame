using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour
{
	public GameObject sel1;
	public GameObject sel2;
	
	void Update()
	{
		if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if(hit.transform.GetComponent<Section>() != null)
				{
					if (sel1 == null)
					{
						sel1 = hit.transform.gameObject;
					}
					else if (sel1 != null && sel2 == null && sel1.GetComponent<Section>().neighbors.Contains(hit.transform))
					{
						sel2 = hit.transform.gameObject;
						Vector3 loc1 = sel1.transform.position;
						Vector3 loc2 = sel2.transform.position;
						StartCoroutine(sel2.GetComponent<Section>().Move(loc1));
						StartCoroutine(sel1.GetComponent<Section>().Move(loc2));
						sel1 = null;
						sel2 = null;

					}
					
				}
			}
		}

	}
}
