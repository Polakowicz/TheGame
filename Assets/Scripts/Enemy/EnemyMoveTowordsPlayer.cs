using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyEventSystem))]
public class EnemyMoveTowordsPlayer : MonoBehaviour
{
	EnemyEventSystem enemyEventSystem;
	GameObject player;
	Rigidbody2D rb;

	//Parameters
	[SerializeField] float speed;
	[SerializeField] float maxRange;
	[SerializeField] float minRange;

	bool active;

	void Start()
	{
		enemyEventSystem = GetComponent<EnemyEventSystem>();
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");

		enemyEventSystem.OnDied += StopMovement;
		enemyEventSystem.OnGetPulled += StopMovement;
		enemyEventSystem.OnGetPulledCanceled += StartMovement;

		active = true;
	}

	void OnDestroy()
	{
		enemyEventSystem.OnDied -= StopMovement;
		enemyEventSystem.OnGetPulled -= StopMovement;
		enemyEventSystem.OnGetPulledCanceled -= StartMovement;
	}

	void Update()
	{
		if (!active) {
			return;
		}

		var distance = Vector2.Distance(transform.position, player.transform.position);
		if(distance > maxRange || distance < minRange) {
			rb.velocity = Vector2.zero;
			return;
		}

		var direction = player.transform.position - transform.position;
		rb.velocity = direction.normalized * speed;
	}

	void StopMovement()
	{
		rb.velocity = Vector2.zero;
		active = false;
	}

	void StartMovement()
	{
		active = true;
	}
}
