using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class EnemyMoveTowordsPlayer : MonoBehaviour
{
	Enemy enemy;
	EnemySharedData data;
	Rigidbody2D rb;

	//Parameters
	[SerializeField] float defaultSpeed;
	[SerializeField] float maxDistance;
	[SerializeField] float minDistance;

	void Start()
	{
		enemy = GetComponent<Enemy>();
		data = enemy.SharedData;
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (data.Pulled || data.Stunned) {
			return;
		}

		var distance = data.DistanceToPlayer;
		if(distance > maxDistance || distance < minDistance) {
			rb.velocity = Vector2.zero;
			return;
		}

		var direction = data.DirectionToPlayer;
		rb.velocity = direction.normalized * defaultSpeed;
	}
}
