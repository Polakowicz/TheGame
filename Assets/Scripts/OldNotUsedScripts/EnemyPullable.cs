using System.Collections;
using UnityEngine;


public class EnemyPullable : MonoBehaviour
{
	OldEnemy enemy;
	Rigidbody2D rb;

	[SerializeField] float targetDistance = 1;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		enemy = GetComponent<OldEnemy>();
	}

	void Update()
	{
		if (!enemy.Data.Pulled) {
			return;
		}

		var distance = enemy.Data.DistanceToPlayer;

		if (distance <= targetDistance) {
			enemy.Data.Pulled = false;
			rb.velocity = Vector2.zero;
			enemy.EndPull();
			return;
		}

		var direction = enemy.Data.DirectionToPlayer;
		rb.velocity = direction.normalized * enemy.Data.PullSpeed;
	}
}
