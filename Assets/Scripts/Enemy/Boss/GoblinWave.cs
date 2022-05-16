using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWave : MonoBehaviour
{
	private CircleCollider2D waveCollider;

	[SerializeField] GameObject rock;

	[SerializeField] private int waveMaxRange = 5;
	[SerializeField] private float waveGrowSpeed = 5;
	[SerializeField] private int waveDamage;
	[SerializeField] private int RocksToSpawn = 3;

	private Vector2 bottomLeftSpawn;
	private Vector2 topRightSpawn;
	private float rockYdistance;

	private void Start()
	{
		waveCollider = GetComponent<CircleCollider2D>();
		waveCollider.radius = 0;
		StartCoroutine(GrowWave());
		SpawnRocks();
	}

	private void SpawnRocks()
	{
		for (int i = 0; i < RocksToSpawn; i++) {
			var x = Random.Range(bottomLeftSpawn.x, topRightSpawn.x);
			var y = Random.Range(bottomLeftSpawn.y, topRightSpawn.y);
			var pos = new Vector2(x, y + rockYdistance);
			Instantiate(rock, pos, Quaternion.identity);
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
			collision.GetComponent<PlayerEventSystem>().GiveDamage(waveDamage);
		}
	}
}
