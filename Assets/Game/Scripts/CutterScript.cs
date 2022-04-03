using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterScript : MonoBehaviour
{
	public Transform StartPoint;
	public Transform EndPoint;
	public LineRenderer Line;

	public AudioSource StartCutAudio;
	public AudioSource DragCutAudio;
	public AudioSource EndCutAudio;

	private void Start()
	{
		HideCutter();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			StartCut();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			ApplyCut();
			HideCutter();
		}
		else if (Input.GetMouseButton(0))
		{
			ContinueCut();
		}

		Debug.LogWarning(Input.GetMouseButton(0));

		UpdateLine();
	}

	private void StartCut()
	{
		var mousePosition = GameManagerScript.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
		var fixedPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
		StartPoint.position = fixedPosition;
		EndPoint.position = fixedPosition;

		StartCutAudio.pitch = Random.Range(0.8f, 1.2f);
		StartCutAudio.volume = Random.Range(0.25f, 0.35f);
		EndCutAudio.pitch = Random.Range(0.8f, 1.2f);
		EndCutAudio.volume = Random.Range(0.8f, 1.2f);
		StartCutAudio.Play();
		//DragCutAudio.Play();
	}

	private void ContinueCut()
	{
		var mousePosition = GameManagerScript.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
		var fixedPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
		EndPoint.position = fixedPosition;
	}

	private void ApplyCut()
	{
		DragCutAudio.Stop();
		EndCutAudio.Play();
		
		var start = StartPoint.position;
		var end = EndPoint.position;
		var hits = Physics2D.RaycastAll(start, end - start, Vector2.Distance(start, end));

		foreach (var hit in hits)
		{
			var number = hit.collider.GetComponent<NumberScript>();

			if (number == null)
				continue;

			if (Vector2.Distance(hit.transform.position, start) < 0.5f)
				continue;

			if (Vector2.Distance(hit.transform.position, end) < 0.5f)
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