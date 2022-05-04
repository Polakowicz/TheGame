using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : Skill
{
	CircleCollider2D range;
	LayerMask mask;
	ContactFilter2D filter;

	[SerializeField] float radiusGrow;
	[SerializeField] int maxEnemies;

	bool charging;
	bool inWarp;
	List<Enemy> enemies = new List<Enemy>();

	void Start()
	{
		range = GetComponent<CircleCollider2D>();
		mask = LayerMask.GetMask("Enemy");
		filter = new ContactFilter2D {
			layerMask = mask,
			useLayerMask = true,
			useTriggers = true
		};

		range.radius = 0;
		charging = false;
	}

	void Update()
	{
		if (!charging) return;

		range.radius += radiusGrow * Time.deltaTime;
	}

	public override void StartUsingSkill()
	{
		charging = true;
	}

	public override void StopUsingSkill()
	{
		charging = false;
		range.radius = 0;
		inWarp = true;
		StartCoroutine(WarpToEnemies(enemies));
	}

	private IEnumerator WarpToEnemies(List<Enemy> enemies)
	{
		foreach (Enemy enemy in enemies) {
			if (enemy != null) {
				transform.root.position = enemy.transform.position;
				yield return new WaitForSeconds(1);
			}
		}
		enemies.Clear();
		inWarp = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (inWarp) return;
		if (mask != (mask | (1 << collision.gameObject.layer))) return;
		if (enemies.Count >= maxEnemies) return;

		enemies.Add(collision.GetComponent<Enemy>());
	}

}