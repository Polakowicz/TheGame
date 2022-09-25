using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
	public class BossSpawningEnemies : MonoBehaviour
	{
		[Serializable]
		public class Spawn
		{
			public GameObject emenyPrefab;
			public Transform position;
		}

		// List of all enemies to spawn in wave
		[SerializeField] private Spawn[] enemiesInWave;

		public void SpawnWave()
		{
			foreach(var spawn in enemiesInWave)
			{
				Instantiate(spawn.emenyPrefab, spawn.position);
			}
		}
	}
}