using System.Collections;
using UnityEngine;

public class EnemyShootsPlayer : MonoBehaviour
{
	Enemy enemy;
	EnemySharedData data;

	//Parameters
	[SerializeField] GameObject bullet;
	[SerializeField] float maxDistance;
	[SerializeField] float minDistance;
	[SerializeField] float cooldown;
	//Internal variables

	float cooldownLeft;

	void Start()
	{
		enemy = GetComponent<Enemy>();
		data = enemy.SharedData;
	}

	void Update()
	{
		if (cooldownLeft > 0) {
			cooldownLeft -= Time.deltaTime * data.SpeedMultiplier;
			return;
		}

		if (data.Stunned || data.Pulled || data.Player == null) {
			return;
		}

		var distance = data.DistanceToPlayer;
		if (distance > maxDistance || distance < minDistance) {
			return;
		}

		var direction = data.DirectionToPlayer;
		var rotation = Vector2.SignedAngle(Vector2.up, direction);
		Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, rotation));
		cooldownLeft = cooldown;
	}
}
