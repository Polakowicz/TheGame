﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplode : MonoBehaviour
{
	[SerializeField] LayerMask hitLayerMask;
	[SerializeField] int damage;

	[SerializeField] CircleCollider2D rangeCollider;
	ContactFilter2D filter;
	float range;

	void Start()
	{
		range = rangeCollider.radius;

		filter = new ContactFilter2D {
			layerMask = LayerMask.GetMask("Enemy"),
			useLayerMask = true,
			useTriggers = true
		};
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (hitLayerMask != (hitLayerMask | (1 << collision.gameObject.layer))) {
			return;
		}

		List<Collider2D> hits = new List<Collider2D>();
		rangeCollider.OverlapCollider(filter, hits);
		foreach (Collider2D hit in hits) {
			var distance = Vector2.Distance(transform.position, hit.transform.position);
			var dmg = (range - distance) / range * damage;
			hit.GetComponent<Enemy>().Hit(Mathf.FloorToInt(dmg));
		}
		Destroy(gameObject);
	}
}