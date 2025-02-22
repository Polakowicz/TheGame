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
			spawnPos.y += rockYdistance;
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
			collision.GetComponent<IHit>().Hit(gameObject, waveDamage, IHit.HitWeapon.OTHER);
		}
	}
}
