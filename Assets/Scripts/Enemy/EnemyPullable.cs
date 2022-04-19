using System.Collections;
using UnityEngine;


public class EnemyPullable : MonoBehaviour
{
	Enemy enemy;
	EnemySharedData data;
	Rigidbody2D rb;

	[SerializeField] float targetDistance;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		enemy = GetComponent<Enemy>();
		data = enemy.SharedData;
	}

	void Update()
	{
		if (!data.Pulled) {
			return;
		}

		var distance = data.DistanceToPlayer;

		if (distance <= targetDistance) {
			data.Pulled = false;
			rb.velocity = Vector2.zero;
			enemy.EndPull();
			return;
		}

		var direction = data.DirectionToPlayer;
		rb.velocity = direction.normalized * data.PullSpeed;
	}
}
