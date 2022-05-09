using System.Collections;
using UnityEngine;

public class ESplitToSmaller : MonoBehaviour
{
	private Enemy main;

	[SerializeField] private GameObject minionPrefab;
	[SerializeField] private int numbersOfMinions;

	private void Start()
	{
		main = GetComponent<Enemy>();
		main.OnDied += CreateMinions;
	}

	private void OnDestroy()
	{
		main.OnDied -= CreateMinions;
	}

	private void CreateMinions()
	{
		for(int i = 0; i < numbersOfMinions; i++) {
			var position = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			position += (Vector2)transform.position;
			Instantiate(minionPrefab, position, Quaternion.identity);
		}
	}
}
