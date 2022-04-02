using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Camera Camera;

	private float targetSize;
	private Vector2 targetFocus;

	private void Start()
	{
		StartCoroutine(FrameCoroutine());
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

			targetFocus = new Vector2(Mathf.Lerp(minWidth, maxWidth, 0.5f), Mathf.Lerp(minHeight, maxHeight, 0.5f));

			var maxAbsWidth = Mathf.Max(Mathf.Abs(minWidth), Mathf.Abs(maxWidth));
			var maxAbsHeight = Mathf.Max(Mathf.Abs(minHeight), Mathf.Abs(maxHeight));

			if (maxAbsHeight * Screen.width < maxAbsWidth * Screen.height)
			{
				targetSize = maxAbsHeight / 2f;
			}
			else
			{
				targetSize = maxAbsWidth / 2f;
			}

			Camera.orthographicSize = targetSize + 1;
			Camera.transform.localPosition = new Vector3(targetFocus.x, targetFocus.y, -10);
		}
	}
}