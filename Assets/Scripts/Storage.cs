using UnityEngine;
using System.Collections;

public class Storage : Section
{
	public override void Recieve(Resource rec)
	{
		resources[(int)rec]++;
	}
}
