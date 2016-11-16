using UnityEngine;
using System.Collections;

public class Island : MonoBehaviour
{
	public float mag;

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
