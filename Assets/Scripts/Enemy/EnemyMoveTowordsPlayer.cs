using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMoveTowordsPlayer : MonoBehaviour
{
	GameObject player;
	Rigidbody2D rb;

	//Parameters
	[SerializeField] float speed;
	[SerializeField] float maxSightRange = 5;
	[SerializeField] float minSightRange = 0.2f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		var distance = Vector2.Distance(transform.position, player.transform.position);
		if(distance > maxSightRange || distance < minSightRange) {
			rb.velocity = Vector2.zero;
			return;
		}

		var direction = player.transform.position - transform.position;
		rb.velocity = direction.normalized * speed;
	}
}
