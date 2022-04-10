using System.Collections;
using UnityEngine;

public class EnemyShootsPlayer : MonoBehaviour
{
	GameObject player;

	//Parameters
	[SerializeField] GameObject bullet;
	[SerializeField] float maxSightRange = 5f;
	[SerializeField] float minSightRange = 1f;
	[SerializeField] float shootCooldown = 1f;

	//Internal variables
	float cooldown;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
	}

	void Update()
	{
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
}
