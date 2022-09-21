using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Player
{
	public class WarpSkill : Skill
	{
		private Manager playerManagerComponent;

		private CircleCollider2D range;
		private LayerMask mask;

		// Speed of collider radius grow during charging
		[SerializeField] private float radiusGrowSpeed;

		// Maximal number of enemies that can be hit by single attack
		[SerializeField] private int maxEnemies;

		// Delay between jumps in attack
		[SerializeField] private float jumpDelay;

		[SerializeField] private int damage;

		private bool isCharging;
		private bool isInWarp;

		private readonly List<GameObject> hitEnemies = new List<GameObject>();

		private void Awake()
		{
			// Get components
			playerManagerComponent = GetComponentInParent<Manager>();
			range = GetComponent<CircleCollider2D>();

			// Set variables
			mask = LayerMask.GetMask("Enemy");
			range.radius = 0;
			isCharging = false;
		}
		private void Update()
		{
			// Grow radius during skill charging
			if (!isCharging) return;
			range.radius += radiusGrowSpeed * Time.deltaTime;
		}

		public override void StartUsingSkill()
		{
			playerManagerComponent.State = Manager.PlayerState.Charging;
			isCharging = true;
		}
		public override void StopUsingSkill()
		{
			isCharging = false;
			isInWarp = true;
			StartCoroutine(WarpToEnemies(hitEnemies));
		}
		private IEnumerator WarpToEnemies(List<GameObject> enemies)
		{
			// Disable collider to not collide with enemies when jumping to them
			Collider2D collider = transform.root.gameObject.GetComponent<Collider2D>();
			collider.enabled = false;


			Vector2 startPos = transform.root.position;
			foreach (GameObject enemy in enemies) {
				// Checki if enemie did not die in before attack
				if (enemy != null) {
					
					var hitInterface = enemy.GetComponent<IHit>();
					Assert.IsNotNull(hitInterface);
					hitInterface.Hit(gameObject, damage);

					transform.root.position = enemy.transform.position;

					yield return new WaitForSeconds(jumpDelay);
				}
			}

			transform.root.position = startPos;

			enemies.Clear();
			collider.enabled = true;
			isInWarp = false;
			range.radius = 0;
			playerManagerComponent.State = Manager.PlayerState.Walk;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			// Add enemies entering trigger during charging to list
			if (isInWarp) return;
			if (mask != (mask | (1 << collision.gameObject.layer))) return;
			if (hitEnemies.Count >= maxEnemies) return;

			hitEnemies.Add(collision.gameObject);
		}
	}
}