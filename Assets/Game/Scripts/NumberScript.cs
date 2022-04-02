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
		var targetSize = Mathf.Log(Count + 1);
		// Size = Mathf.Lerp(Size, targetSize, Time.deltaTime * 0.2f);
	}

	public void Split(Vector2 cutVector)
	{
		var offset = Vector2.Perpendicular(cutVector) * Size * 0.1f;
		var newCount = Count / 2;

		{
			var number = Instantiate(GameManagerScript.Instance.NumberPrefab, transform.parent);
			number.transform.localPosition = transform.localPosition + (Vector3)offset;
			number.Count = newCount;
			number.Size = Size * 0.7f;
		}

		{
			var number = Instantiate(GameManagerScript.Instance.NumberPrefab, transform.parent);
			number.transform.localPosition = transform.localPosition - (Vector3)offset;
			number.Count = newCount;
			number.Size = Size * 0.7f;
		}

		GameManagerScript.Instance.UnregisterNumber(this);
		Destroy(gameObject);
	}
}