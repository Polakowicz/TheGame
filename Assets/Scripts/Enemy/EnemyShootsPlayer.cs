using System.Collections;
using UnityEngine;

public class EnemyShootsPlayer : MonoBehaviour
{
	EnemyEventSystem enemyEventSystem;
	GameObject player;

	//Parameters
	[SerializeField] GameObject bullet;
	[SerializeField] float maxSightRange;
	[SerializeField] float minSightRange;
	[SerializeField] float shootCooldown;
	//Internal variables
	float cooldown;
	bool active;

	void Start()
	{
		enemyEventSystem = GetComponent<EnemyEventSystem>();
		player = GameObject.FindWithTag("Player");

		enemyEventSystem.OnGetCaught += StopShooting;
		enemyEventSystem.OnGetCaughtCanceled += StartShooting;

		active = true;
	}

	void OnDestroy()
	{
		enemyEventSystem.OnGetCaught -= StopShooting;
		enemyEventSystem.OnGetCaughtCanceled -= StartShooting;
	}

	void Update()
	{
		if (!active) {
			return;
		}

		if(cooldown > 0) {
			cooldown -= Time.deltaTime;
			return;
		}

		var distance = Vector2.Distance(transform.position, player.transform.position);
		if (distance > maxSightRange || distance < minSightRange) {
			return;
		}

		var direction = player.transform.position - transform.position;
		var rotation = Vector2.SignedAngle(Vector2.up, direction);
		Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, rotation));
		cooldown = shootCooldown;
	}

	void StopShooting()
	{
		active = false;
	}

	void StartShooting()
	{
		active = true;
	}
}
