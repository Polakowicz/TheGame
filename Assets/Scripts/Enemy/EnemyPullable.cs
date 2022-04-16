using System.Collections;
using UnityEngine;


public class EnemyPullable : MonoBehaviour
{
	EnemyEventSystem enemyEventSystem;
	Rigidbody2D rb;

	[SerializeField] float targetDistance;

	Transform playerPositoin;
	float speed;
	bool active;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		enemyEventSystem = GetComponent<EnemyEventSystem>();

		enemyEventSystem.OnGetPulledValues += GetPulledToPosition;
		enemyEventSystem.OnGetPulledCanceled += CanelGetPulledToPosition;
	}

	void OnDestroy()
	{
		enemyEventSystem.OnGetPulledValues -= GetPulledToPosition;
		enemyEventSystem.OnGetPulledCanceled -= CanelGetPulledToPosition;
	}

	void Update()
	{
		if (!active) {
			return;
		}

		var distance = Vector2.Distance(playerPositoin.position, rb.position);

		if (distance <= targetDistance) {
			active = false;
			rb.velocity = Vector2.zero;
			enemyEventSystem.CancelPulling();
			return;
		}

		var direction = (Vector2)playerPositoin.position - rb.position;
		rb.velocity = direction.normalized * speed;
	}

	void GetPulledToPosition(Transform position, float speed)
	{
		playerPositoin = position;
		this.speed = speed;
		active = true;
	}

	void CanelGetPulledToPosition()
	{
		rb.velocity = Vector2.zero;
		active = false;
	}
}
