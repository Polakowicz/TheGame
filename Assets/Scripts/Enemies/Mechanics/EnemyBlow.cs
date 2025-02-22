﻿using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemies
{
	public class EnemyBlow : MonoBehaviour
	{
		[SerializeField] private CircleCollider2D range;
		[SerializeField] private LayerMask blowMask;
		private ContactFilter2D blowFilter;

		[SerializeField] private int maxDamage;

		private void Awake()
		{
			blowFilter = new ContactFilter2D {
				layerMask = blowMask,
				useLayerMask = true
			};
		}

		public void BlowUp()
		{
			List<Collider2D> hits = new List<Collider2D>();
			range.OverlapCollider(blowFilter, hits);
			foreach(Collider2D hit in hits) {
				var distance = Vector2.Distance(gameObject.transform.position, hit.transform.position);
				var dmg = (distance - range.radius) / range.radius;
				hit.GetComponent<IHit>()?.Hit(gameObject, (int)dmg);
			}
		}
	}
}