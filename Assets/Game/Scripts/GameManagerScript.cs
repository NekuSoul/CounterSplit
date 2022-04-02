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

	private readonly List<NumberScript> Numbers = new();

	public GameManagerScript()
	{
		Instance = this;
	}

	private void Start()
	{
		StartCoroutine(CountNumbersCoroutine());
		StartCoroutine(SplitCoroutine());
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

	private IEnumerator SplitCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(3);

			foreach (var number in Numbers.ToArray())
			{
				number.Split();
			}
		}
	}

	private void FixedUpdate()
	{
		foreach (var numberA in Numbers)
		{
			// ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
			foreach (var numberB in Numbers)
			{
				if (numberA == numberB)
					continue;

				var minDistance = (numberA.Size + numberB.Size) / 2;
				var curDistance = Vector2.Distance(numberA.transform.transform.position, numberB.transform.position);

				if (curDistance > minDistance)
					continue;

				var forceVector = (numberA.transform.position - numberB.transform.position).normalized;

				if (forceVector == Vector3.zero)
				{
					forceVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
				}

				forceVector *= 3;

				numberA.Rigidbody.AddForce(forceVector);
			}
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