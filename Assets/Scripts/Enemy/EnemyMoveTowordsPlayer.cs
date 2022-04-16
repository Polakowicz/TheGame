using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMoveTowordsPlayer : MonoBehaviour
{
	EnemyEventSystem enemyEventSystem;
	GameObject player;
	Rigidbody2D rb;

	//Parameters
	[SerializeField] float speed;
	[SerializeField] float maxSightRange;
	[SerializeField] float minSightRange;

	bool active;

	void Start()
	{
		enemyEventSystem = GetComponent<EnemyEventSystem>();
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");

		enemyEventSystem.OnGetCaught += StopMovement;
		enemyEventSystem.OnGetCaughtCanceled += StartMovement;

		active = true;
	}

	void OnDestroy()
	{
		enemyEventSystem.OnGetCaught -= StopMovement;
		enemyEventSystem.OnGetCaughtCanceled -= StartMovement;
	}

	void Update()
	{
		if (!active) {
			return;
		}

		var distance = Vector2.Distance(transform.position, player.transform.position);
		if(distance > maxSightRange || distance < minSightRange) {
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
