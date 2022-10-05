using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Enemies
{
	public class BossSpawningEnemies : MonoBehaviour
	{
		// Class nesting to display 2d array in Unity inspector

		// Class represents single wave of enemies
		[Serializable]
		private class Wave
		{
			// All enemies in single wave
			[SerializeField] public EnemySpawn[] enemiesInWave;

			[Serializable]
			public class EnemySpawn
			{
				public GameObject emenyPrefab;
				public Transform position;
			}
		}

		// List of waves
		[SerializeField] private Wave[] allEnemyWaves;
		private int waveNumber = 0;

		public void SpawnNextWave()
		{
			// Make sure that not out of waves range
			Assert.IsTrue(waveNumber < allEnemyWaves.Length);

			// Spawn enemies
			var wave = allEnemyWaves[waveNumber++];
			foreach(var enemy in wave.enemiesInWave)
			{
				Instantiate(enemy.emenyPrefab, enemy.position.position, Quaternion.identity);
			}
		}
	}
}