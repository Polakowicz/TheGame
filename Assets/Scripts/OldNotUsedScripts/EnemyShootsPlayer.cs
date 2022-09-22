using System.Collections;
using UnityEngine;

public class EnemyShootsPlayer : MonoBehaviour
{
	OldEnemy enemy;

	//Parameters
	[SerializeField] GameObject bullet;
	[SerializeField] float maxDistance;
	[SerializeField] float minDistance;
	[SerializeField] float cooldown;
	//Internal variables

	float cooldownLeft;

	void Start()
	{
		enemy = GetComponent<OldEnemy>();
	}

	void Update()
	{
		if (cooldownLeft > 0) {
			cooldownLeft -= Time.deltaTime * enemy.Data.SpeedMultiplier;
			return;
		}

		if (enemy.Data.Stunned || enemy.Data.Pulled || enemy.Data.Player == null) {
			return;
		}

		var distance = enemy.Data.DistanceToPlayer;
		if (distance > maxDistance || distance < minDistance) {
			return;
		}

		var direction = enemy.Data.DirectionToPlayer;
		var rotation = Vector2.SignedAngle(Vector2.up, direction);
		Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, rotation));
		cooldownLeft = cooldown;
	}
}
