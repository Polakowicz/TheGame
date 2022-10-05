using Scripts.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Enemies
{
	public class BossSpawningEnemies : MonoBehaviour
	{
		private readonly string spawnedEnemiesAnimatorName = "SpawnedEnemies";

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


		private SpawnerManager spawnerManager;
		private Animator animator;

		// List of waves
		[SerializeField] private Wave[] allEnemyWaves;
		private int waveNumber = 0;

		// Number of enemies spawned in wave that are still alive;
		private int spawnedEnemies;

		public int WavesLeft { get => allEnemyWaves.Length - waveNumber; }

		private void Awake()
		{
			animator = GetComponent<Animator>();
			spawnerManager = GetComponent<SpawnerManager>();
		}
		private void Start()
		{
			GameEventSystem.Instance.OnEnemyKilled += DecreaseEnemiesLeft;
		}
		private void OnDestroy()
		{
			GameEventSystem.Instance.OnEnemyKilled -= DecreaseEnemiesLeft;
		}

		public void SpawnNextWave()
		{
			// Make sure that not out of waves range
			Assert.IsTrue(waveNumber < allEnemyWaves.Length);

			spawnerManager.ResetHealth();

			// Spawn enemies
			spawnedEnemies = 0;
			var wave = allEnemyWaves[waveNumber++];
			foreach(var enemy in wave.enemiesInWave)
			{
				Instantiate(enemy.emenyPrefab, enemy.position.position, Quaternion.identity);
				spawnedEnemies++;
			}
			animator.SetInteger(spawnedEnemiesAnimatorName, spawnedEnemies);
		}

		private void DecreaseEnemiesLeft()
		{
			spawnedEnemies--;
			animator.SetInteger(spawnedEnemiesAnimatorName, spawnedEnemies);
		}
	}
}