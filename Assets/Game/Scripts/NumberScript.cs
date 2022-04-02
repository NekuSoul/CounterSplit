using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberScript : MonoBehaviour
{
	public TextMeshPro Text;
	public Rigidbody2D Rigidbody;

	private float _size = 5;

	public float Size
	{
		get => _size;
		set
		{
			_size = value;
			ApplySize();
		}
	}

	public float TargetSize { get; set; } = 5;

	private void ApplySize()
	{
		transform.localScale = new Vector3(Size, Size);
	}

	private int _count = 0;

	public int Count
	{
		get => _count;
		set
		{
			_count = value;
			Text.text = _count.ToString();
		}
	}

	// Start is called before the first frame update
	private void Start()
	{
		GameManagerScript.Instance.RegisterNumber(this);
		ApplySize();
	}

	private void Update()
	{
		Size = Mathf.Lerp(Size, TargetSize, Time.deltaTime);
	}

	public void Split(Vector2 cutVector)
	{
		var offset = Vector2.Perpendicular(cutVector) * Size * 0.001f;
		var newCount = Count / 2;

		{
			var number = Instantiate(GameManagerScript.Instance.NumberPrefab, transform.parent);
			number.transform.localPosition = transform.localPosition + (Vector3)offset;
			number.Count = newCount;
			number.Size = Size;
			number.TargetSize = Size * 0.6f;
		}

		{
			var number = Instantiate(GameManagerScript.Instance.NumberPrefab, transform.parent);
			number.transform.localPosition = transform.localPosition - (Vector3)offset;
			number.Count = newCount;
			number.Size = Size;
			number.TargetSize = Size * 0.6f;
		}

		GameManagerScript.Instance.UnregisterNumber(this);
		Destroy(gameObject);
	}
}