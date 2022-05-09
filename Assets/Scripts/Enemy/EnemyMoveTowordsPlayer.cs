using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class EnemyMoveTowordsPlayer : MonoBehaviour
{
	Enemy enemy;
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;

	//Parameters
	[SerializeField] float defaultSpeed;
	[SerializeField] float maxDistance;
	[SerializeField] float minDistance;


	void Start()
	{
		enemy = GetComponent<Enemy>();
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (enemy.Data.Pulled || enemy.Data.Stunned || enemy.Data.Player == null) {
			return;
		}
		Debug.Log("Here");

		var distance = enemy.Data.DistanceToPlayer;
		if(distance > maxDistance || distance < minDistance) {
			rb.velocity = Vector2.zero;
			return;
		}
		Debug.Log(distance);
		var direction = enemy.Data.DirectionToPlayer;
		Debug.Log(direction);

		if (enemy.transform.position.x < enemy.Data.Player.transform.position.x)
		{
			spriteRenderer.flipX = true;
			
		} else if (enemy.transform.position.x > enemy.Data.Player.transform.position.x)
		{
			spriteRenderer.flipX = false;
		}

		rb.velocity = direction.normalized * defaultSpeed * enemy.Data.SpeedMultiplier;
	}
}
