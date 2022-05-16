using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWave : MonoBehaviour
{
	private CircleCollider2D waveCollider;

	[SerializeField] private int waveMaxRange = 5;
	[SerializeField] private float waveGrowSpeed = 5;
	[SerializeField] private int waveDamage;

	private void Start()
	{
		waveCollider = GetComponent<CircleCollider2D>();
		waveCollider.radius = 0;
		StartCoroutine(GrowWave());
		SpawnRocks();
	}

	private void SpawnRocks()
	{
		Debug.Log("Spawn Rocks");
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
