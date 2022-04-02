using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberScript : MonoBehaviour
{
	public TextMeshPro Text;
	public Rigidbody2D Rigidbody;
	
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
	}

	public void Split(Vector2 cutVector)
	{
		var offset = Vector2.Perpendicular(cutVector) * 0.001f;
		var newCount = Count / 2;

		{
			var number = Instantiate(GameManagerScript.Instance.NumberPrefab, transform.parent);
			number.transform.localPosition = transform.localPosition + (Vector3)offset;
			number.Count = newCount;
		}

		{
			var number = Instantiate(GameManagerScript.Instance.NumberPrefab, transform.parent);
			number.transform.localPosition = transform.localPosition - (Vector3)offset;
			number.Count = newCount;
		}

		GameManagerScript.Instance.UnregisterNumber(this);
		Destroy(gameObject);
	}
}