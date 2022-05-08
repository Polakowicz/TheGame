using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class EnemyMoveTowordsPlayer : MonoBehaviour
{
	Enemy enemy;
	EnemySharedData data;
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;

	//Parameters
	[SerializeField] float defaultSpeed;
	[SerializeField] float maxDistance;
	[SerializeField] float minDistance;

	void Start()
	{
		enemy = GetComponent<Enemy>();
		data = enemy.SharedData;
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (data.Pulled || data.Stunned || data.Player == null) {
			return;
		}

		var distance = data.DistanceToPlayer;
		if(distance > maxDistance || distance < minDistance) {
			rb.velocity = Vector2.zero;
			return;
		}

		var direction = data.DirectionToPlayer;

		if (enemy.transform.position.x < data.Player.transform.position.x)
		{
			spriteRenderer.flipX = true;
			
		} else if (enemy.transform.position.x > data.Player.transform.position.x)
		{
			spriteRenderer.flipX = false;
		}

		rb.velocity = direction.normalized * defaultSpeed * data.SpeedMultiplier;
	}
}
