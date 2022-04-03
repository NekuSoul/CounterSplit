using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Camera Camera;

	private float targetSize;
	private Vector3 targetFocus;

	private void Start()
	{
		targetSize = Camera.orthographicSize;
		StartCoroutine(FrameCoroutine());
	}

	private void Update()
	{
		transform.position = Vector3.Lerp(transform.position, targetFocus, 10f * Time.deltaTime);
		Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, targetSize, 10f * Time.deltaTime);
	}

	private IEnumerator FrameCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);

			var minWidth = float.MaxValue;
			var maxWidth = float.MinValue;
			var minHeight = float.MaxValue;
			var maxHeight = float.MinValue;

			foreach (var number in GameManagerScript.Instance.Numbers)
			{
				var position = number.transform.position;

				minWidth = Mathf.Min(minWidth, position.x);
				maxWidth = Mathf.Max(maxWidth, position.x);
				minHeight = Mathf.Min(minHeight, position.y);
				maxHeight = Mathf.Max(maxHeight, position.y);
			}

			targetFocus = new Vector3(Mathf.Lerp(minWidth, maxWidth, 0.5f), Mathf.Lerp(minHeight, maxHeight, 0.5f), -10);

			var maxAbsWidth = Mathf.Max(Mathf.Abs(minWidth), Mathf.Abs(maxWidth));
			var maxAbsHeight = Mathf.Max(Mathf.Abs(minHeight), Mathf.Abs(maxHeight));

			var aspectRatio = Screen.width / (float)Screen.height;

			var adjustedMaxAbsWidth = maxAbsWidth / aspectRatio;

			targetSize = Mathf.Max(maxAbsHeight, adjustedMaxAbsWidth) / 2f;

			targetSize += 1;
			targetSize *= 1.5f;
		}
	}
}