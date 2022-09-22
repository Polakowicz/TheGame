using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
	public class EnemySplit : MonoBehaviour
	{
		[SerializeField] private GameObject minionPrefab;
		[SerializeField] private int numberOfMinions;

		public void SpawnMinions()
		{
			for (int i = 0; i < numberOfMinions; i++) {
				var position = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
				position += (Vector2)transform.position;
				Instantiate(minionPrefab, position, Quaternion.identity);
			}
		}
	}
}