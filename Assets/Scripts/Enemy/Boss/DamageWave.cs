using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWave : MonoBehaviour
{
	private CircleCollider2D waveCollider;

	[SerializeField] private int waveMaxRange;
	[SerializeField] private float waveGrowSpeed;
	[SerializeField] private int waveDamage;

	private void Start()
	{
		waveCollider = GetComponent<CircleCollider2D>();
		waveCollider.radius = 0;
	}

	private void MakeWave()
	{
		StartCoroutine(GrowWave());
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

		collision.GetComponent<PlayerEventSystem>().GiveDamage(waveDamage);
	}
}
