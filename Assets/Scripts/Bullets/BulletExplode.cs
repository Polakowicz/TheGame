using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Bullets
{
	public class BulletExplode : MonoBehaviour
	{
		// Range for explosion
		[SerializeField] private CircleCollider2D rangeCollider;
		[SerializeField] private LayerMask hitLayerMask;
		private ContactFilter2D filter;

		// Maximal damaga that can be dealt in other object is really close
		[SerializeField] private int damage;

		private void Awake()
		{
			filter = new ContactFilter2D {
				layerMask = hitLayerMask,
				useLayerMask = true,
				useTriggers = true
			};
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (hitLayerMask != (hitLayerMask | (1 << collision.gameObject.layer))) {
				return;
			}

			List<Collider2D> hits = new List<Collider2D>();
			rangeCollider.OverlapCollider(filter, hits);
			foreach (Collider2D hit in hits) {
				var distance = Vector2.Distance(transform.position, hit.transform.position);
				var dmg = (rangeCollider.radius - distance) / rangeCollider.radius * damage;
				hit.GetComponent<IHit>().Hit(gameObject, Mathf.FloorToInt(dmg));
			}
			Destroy(gameObject);
		}
	}
}