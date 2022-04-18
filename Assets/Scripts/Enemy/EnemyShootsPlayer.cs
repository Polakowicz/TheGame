using System.Collections;
using UnityEngine;

public class EnemyShootsPlayer : MonoBehaviour
{
	EnemyEventSystem enemyEventSystem;
	GameObject player;

	//Parameters
	[SerializeField] GameObject bullet;
	[SerializeField] float maxRange;
	[SerializeField] float minRange;
	[SerializeField] float cooldown;
	//Internal variables

	float cooldownLeft;
	bool active;

	void Start()
	{
		enemyEventSystem = GetComponent<EnemyEventSystem>();
		player = GameObject.FindWithTag("Player");

		enemyEventSystem.OnDied += StopShooting;
		enemyEventSystem.OnGetPulled += StopShooting;
		enemyEventSystem.OnGetPulledCanceled += StartShooting;
		enemyEventSystem.OnGetStuned += StopShooting;
		enemyEventSystem.OnGetStunedEnded += StartShooting;

		active = true;
	}

	void OnDestroy()
	{
		enemyEventSystem.OnDied -= StopShooting;
		enemyEventSystem.OnGetPulled -= StopShooting;
		enemyEventSystem.OnGetPulledCanceled -= StartShooting;
		enemyEventSystem.OnGetStuned -= StopShooting;
		enemyEventSystem.OnGetStunedEnded -= StartShooting;
	}

	void Update()
	{
		if (cooldownLeft > 0) {
			cooldownLeft -= Time.deltaTime;
			return;
		}

		if (!active) {
			return;
		}

		var distance = Vector2.Distance(transform.position, player.transform.position);
		if (distance > maxRange || distance < minRange) {
			return;
		}

		var direction = player.transform.position - transform.position;
		var rotation = Vector2.SignedAngle(Vector2.up, direction);
		Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, rotation));
		cooldownLeft = cooldown;
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
