using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManagerScript : MonoBehaviour
{
	public static GameManagerScript Instance { get; private set; }

	public Camera Camera;
	public Transform Storage;

	public NumberScript NumberPrefab;

	public List<NumberScript> Numbers { get; } = new();
	private int nextPushStep = 10;

	public GameManagerScript()
	{
		Instance = this;
	}

	private void Start()
	{
		StartCoroutine(CountNumbersCoroutine());
	}

	private IEnumerator CountNumbersCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);

			foreach (var number in Numbers)
			{
				number.Count++;
			}
		}
	}

	private void Update()
	{
		nextPushStep--;

		if (nextPushStep > 0)
			return;

		nextPushStep = 10;

		for (var a = 0; a < Numbers.Count; a++)
		{
			var numberA = Numbers[a];
			for (var b = a + 1; b < Numbers.Count; b++)
			{
				var numberB = Numbers[b];

				var curDistance = Vector2.Distance(numberA.transform.position, numberB.transform.position);

				if (curDistance > 1f)
					continue;

				var forceVector = (numberA.transform.position - numberB.transform.position).normalized;

				if (forceVector == Vector3.zero)
				{
					forceVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
				}

				var strength = 1f - curDistance;

				forceVector *= 1000 * strength * Time.deltaTime;

				numberA.Rigidbody.AddForce(forceVector);
				numberB.Rigidbody.AddForce(-forceVector);
			}

			numberA.Rigidbody.AddForce(-numberA.transform.position * Time.deltaTime * 10);
		}
	}

	public void RegisterNumber(NumberScript number)
	{
		Numbers.Add(number);
	}

	public void UnregisterNumber(NumberScript number)
	{
		Numbers.Remove(number);
	}
}