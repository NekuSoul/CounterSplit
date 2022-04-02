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
		if (Input.GetMouseButtonDown(0))
		{
			// Start
			var mousePosition = GameManagerScript.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
			var fixedPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
			StartPoint.position = fixedPosition;
			EndPoint.position = fixedPosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			// End
			ApplyCut();
			HideCutter();
		}
		else if (Input.GetMouseButton(0))
		{
			// Drag
			var mousePosition = GameManagerScript.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
			var fixedPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
			EndPoint.position = fixedPosition;
		}

		UpdateLine();
	}

	private void ApplyCut()
	{
		var start = StartPoint.position;
		var end = EndPoint.position;
		var hits = Physics2D.RaycastAll(start, end - start, Vector2.Distance(start, end));

		foreach (var hit in hits)
		{
			var number = hit.collider.GetComponent<NumberScript>();

			if (number == null)
				continue;

			if (Vector2.Distance(hit.transform.position, start) < number.Size / 2)
				continue;
			
			if (Vector2.Distance(hit.transform.position, end) < number.Size / 2)
				continue;

			number.Split((end - start).normalized);
		}
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