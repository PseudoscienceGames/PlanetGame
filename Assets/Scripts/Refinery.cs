using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Refinery : Section
{
	public List<Resource> sepTypes = new List<Resource>();

	public override void Recieve(Resource rec)
	{
		if(outputs.Count == sepTypes.Count + 1)
		{
			bool sent = false;
			for(int i = 0; i < sepTypes.Count; i++)
			{
				if (rec == sepTypes[i])
				{
					Send(rec, outputs[i]);
					sent = true;
				}
			}
			if (!sent)
				Send(rec, outputs[sepTypes.Count]);
		}
	}
}
