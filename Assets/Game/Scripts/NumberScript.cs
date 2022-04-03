using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberScript : MonoBehaviour
{
	public TextMeshPro Text;
	public Rigidbody2D Rigidbody;

	private int _count = 2;

	public int Count
	{
		get => _count;
		set
		{
			_count = value;
			Text.text = _count.ToString();

			if (value == 13)
			{
				GetComponent<SpriteRenderer>().color = Color.red;
				Text.color = Color.black;
			}
			else if (value > 9)
			{
				Text.color = Color.red;
			}
			else if (value > 6)
			{
				Text.color = new Color(1f, 0.4f, 0);
			}
			else if (value > 3)
			{
				Text.color = new Color(1f, 0.6f, 0);
			}
			else
			{
				Text.color = Color.black;
			}
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