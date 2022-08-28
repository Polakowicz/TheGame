using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWave : MonoBehaviour
{
	public static readonly float rockYdistance = 15;

	private CircleCollider2D waveCollider;
	private Vector2 spawnPos;

	[SerializeField] GameObject rock;

	[SerializeField] private int waveMaxRange = 5;
	[SerializeField] private float waveGrowSpeed = 5;
	[SerializeField] private int waveDamage;
	private int RocksToSpawn = 1;

	private static readonly float spawnRange = 5;
	private Vector2 bottomLeftSpawn = new Vector2(-spawnRange, spawnRange);
	private Vector2 topRightSpawn = new Vector2(spawnRange, -spawnRange);


	private void Start()
	{
		spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		waveCollider = GetComponent<CircleCollider2D>();
		waveCollider.radius = 0;
		StartCoroutine(GrowWave());
		SpawnRocks();
	}

	private void SpawnRocks()
	{
		for (int i = 0; i < RocksToSpawn; i++) {
			var x = Random.Range(bottomLeftSpawn.x, topRightSpawn.x);
			x += transform.position.x;
			var y = Random.Range(bottomLeftSpawn.y, topRightSpawn.y);
			y += transform.position.y;
			spawnPos.y += rockYdistance;
			var pos = new Vector2(x, y + rockYdistance);
			Instantiate(rock, spawnPos, Quaternion.identity);
		}
	}

	private IEnumerator GrowWave()
	{
		while(waveCollider.radius < waveMaxRange) {
			waveCollider.radius += Time.deltaTime * waveGrowSpeed;
			yield return null;
		}
		waveCollider.radius = 0;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;

		if (collision.name.Equals("Player")) {
			collision.GetComponent<IHit>().Hit(waveDamage, IHit.HitWeapon.OTHER);
		}
	}
}
