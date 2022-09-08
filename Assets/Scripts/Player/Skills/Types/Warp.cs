using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
	public class Warp : Skill
	{
		private CircleCollider2D range;
		[SerializeField] private LayerMask mask;

		[SerializeField] private float radiusGrowSpeed;
		[SerializeField] private int maxEnemies;
		[SerializeField] private int damage;
		[SerializeField] private float jumpDelay;

		private bool charging;
		private bool inWarp;
		private List<GameObject> enemies = new List<GameObject>();

		private void Start()
		{
			range = GetComponent<CircleCollider2D>();

			range.radius = 0;
			charging = false;
		}

		private void Update()
		{
			if (!charging) return;
			range.radius += radiusGrowSpeed * Time.deltaTime;
		}

		public override void StartUsingSkill()
		{
			charging = true;
		}

		public override void StopUsingSkill()
		{
			charging = false;
			inWarp = true;
			StartCoroutine(WarpToEnemies(enemies));
		}

		private IEnumerator WarpToEnemies(List<GameObject> enemies)
		{
			Collider2D collider = transform.root.gameObject.GetComponent<Collider2D>();
			Vector2 startPos = transform.root.position;
			collider.enabled = false;
			foreach (GameObject enemy in enemies) {
				if (enemy != null) {
					enemy.GetComponent<IHit>().Hit(damage);
					transform.root.position = enemy.transform.position;
					yield return new WaitForSeconds(jumpDelay);
				}
			}
			transform.root.position = startPos;
			enemies.Clear();
			collider.enabled = true;
			inWarp = false;
			range.radius = 0;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (inWarp) return;
			if (mask != (mask | (1 << collision.gameObject.layer))) return;
			if (enemies.Count >= maxEnemies) return;

			enemies.Add(collision.gameObject);
		}

	}
}