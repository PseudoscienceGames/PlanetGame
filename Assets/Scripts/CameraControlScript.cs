//1
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControlScript : MonoBehaviour
{
    public float zoom;
    public int oZoomMin;
    public int oZoomMax;
    public float oZoomSpeed;
	public int pZoomMin;
	public int pZoomMax;
	public float pZoomSpeed;
	public float cameraRotSpeed;

    public GameObject cameraPivot;

	public Vector2 touchPoint1;
	public bool touchMoved;
	public float touchGap;

    void Update()
    {
		if (Input.touchCount == 1)
		{
			Touch t = Input.touches[0];
			if (t.phase == TouchPhase.Began)
			{
				touchPoint1 = t.position;
				touchMoved = false;
			}
			if (t.phase == TouchPhase.Moved)
			{
				touchMoved = true;
				cameraPivot.transform.Rotate(Vector3.up, (touchPoint1.x - Input.touches[0].position.x) * -cameraRotSpeed * Time.deltaTime);
				cameraPivot.transform.Rotate(Vector3.right, (touchPoint1.y - Input.touches[0].position.y) * cameraRotSpeed * Time.deltaTime);
				touchPoint1 = Input.touches[0].position;
			}
			//if(t.phase == TouchPhase.Ended)
			//{
			//	if(!touchMoved)
			//	{
			//		Ray ray = Camera.main.ScreenPointToRay(t.position);
			//		RaycastHit hit = new RaycastHit();
			//		if (Physics.Raycast(ray, out hit, 200))
			//		{
			//			if (hit.transform.GetComponent<TileScript>() != null)
			//			{
			//				hit.transform.GetComponent<TileScript>().TurnTile();
			//			}
			//		}
			//	}
			//}
		}
		if (Input.touchCount == 2)
		{
			touchMoved = true;
			if (Input.touches[1].phase == TouchPhase.Began)
				touchGap = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
			//Zoom camera
			if (Camera.main.orthographic)
			{
				zoom = Camera.main.orthographicSize;
				if (zoom >= oZoomMin && zoom <= oZoomMax)
				{
					float deltaGap = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
					Camera.main.orthographicSize += (touchGap - deltaGap) * oZoomSpeed;
				}
				if (zoom < oZoomMin)
					Camera.main.orthographicSize = oZoomMin;
				if (zoom > oZoomMax)
					Camera.main.orthographicSize = oZoomMax;
			}
			else
			{
				zoom = Camera.main.fieldOfView;
				if (zoom >= pZoomMin && zoom <= pZoomMax)
				{
					float deltaGap = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
					Camera.main.fieldOfView += (touchGap - deltaGap) * pZoomSpeed;
				}
				if (zoom < pZoomMin)
					Camera.main.fieldOfView = pZoomMin;
				if (zoom > pZoomMax)
					Camera.main.fieldOfView = pZoomMax;
			}
			touchGap = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
		}
	}
}