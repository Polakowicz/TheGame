using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Bullets
{
	public class ExplosiveBullet : Bullet
	{
		// To find enemies to hit
		[SerializeField] private CircleCollider2D rangeCollider;
		private ContactFilter2D filter;

		private void Awake()
		{
			filter = new ContactFilter2D {
				layerMask = hitLayerMask,
				useLayerMask = true,
				useTriggers = true
			};
		}

		protected override void Damage(Collider2D collision)
		{
			List<Collider2D> hits = new List<Collider2D>();
			rangeCollider.OverlapCollider(filter, hits);
			foreach (Collider2D hit in hits)
			{
				var distance = Vector2.Distance(transform.position, hit.transform.position);
				var dmg = (rangeCollider.radius - distance) / rangeCollider.radius * damage;
				hit.GetComponent<IHit>().Hit(gameObject, Mathf.FloorToInt(dmg));
			}
			bulletPool.DisposeObject(gameObject);
		}
	}
}