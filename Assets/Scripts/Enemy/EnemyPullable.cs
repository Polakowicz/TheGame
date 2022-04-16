using System.Collections;
using UnityEngine;


public class EnemyPullable : MonoBehaviour
{
	EnemyEventSystem enemyEventSystem;
	Rigidbody2D rb;

	[SerializeField] float minDistance;

	Transform playerPositoin;
	float speed;
	bool active;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		enemyEventSystem = GetComponent<EnemyEventSystem>();
		enemyEventSystem.OnGetPulled += GetPulledToPosition;
		enemyEventSystem.OnGetPulledCanceled += CanelGetPulledToPosition;
	}

	void OnDestroy()
	{
		enemyEventSystem.OnGetPulled -= GetPulledToPosition;
		enemyEventSystem.OnGetPulledCanceled -= CanelGetPulledToPosition;
	}

	void Update()
	{
		if (!active) {
			return;
		}
		var direction = (Vector2)playerPositoin.position - rb.position;
		var s = direction.magnitude;

		if (s <= minDistance) {
			active = false;
			rb.velocity = Vector2.zero;
			enemyEventSystem.CancelPull();
			return;
		}

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
