using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterScript : MonoBehaviour
{
	public Transform StartPoint;
	public Transform EndPoint;
	public LineRenderer Line;

	private void Start()
	{
		HideCutter();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			// Start
			var mousePosition = GameManagerScript.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
			var fixedPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
			StartPoint.position = fixedPosition;
			EndPoint.position = fixedPosition;
		}
		else if (Input.GetMouseButtonUp(1))
		{
			// End
			HideCutter();
		}
		else if(Input.GetMouseButton(1))
		{
			// Drag
			var mousePosition = GameManagerScript.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
			var fixedPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
			EndPoint.position = fixedPosition;
		}
		
		UpdateLine();
	}

	private void UpdateLine()
	{
		Line.SetPosition(0, StartPoint.position);
		Line.SetPosition(1, EndPoint.position);
	}

	private void HideCutter()
	{
		StartPoint.position = GameManagerScript.Instance.Storage.position;
		EndPoint.position = GameManagerScript.Instance.Storage.position;
	}
}