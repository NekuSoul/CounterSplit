using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberScript : MonoBehaviour
{
	public TextMeshPro Text;
	public Rigidbody2D Rigidbody;
	public float Size = 1;

	private int _count;

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
		SetSize();
	}

	private void SetSize()
	{
		transform.localScale = new Vector3(Size, Size);
	}

	public void Split()
	{
		for (var i = 0; i < 2; i++)
		{
			var number = Instantiate(GameManagerScript.Instance.NumberPrefab, transform.parent);
			number.transform.localPosition = transform.localPosition;
			number.Size = Size * 0.8f;
			number.SetSize();
		}

		GameManagerScript.Instance.UnregisterNumber(this);
		Destroy(gameObject);
	}
}