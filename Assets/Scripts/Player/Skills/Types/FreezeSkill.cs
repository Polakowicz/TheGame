using Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
	public class FreezeSkill : Skill
	{
		private Collider2D range;
		private LayerMask mask;
		private ContactFilter2D filter;

		[SerializeField] private float freezTime;
		[SerializeField] private float freezStrength;

		private void Awake()
		{
			range = GetComponent<Collider2D>();

			mask = LayerMask.GetMask("Enemy");
			filter = new ContactFilter2D {
				layerMask = mask,
				useLayerMask = true,
				useTriggers = true
			};
		}

		public override void UseSkill()
		{
			// Stun enemies in range
			List<Collider2D> colliders = new List<Collider2D>();
			range.OverlapCollider(filter, colliders);
			foreach (Collider2D collider in colliders) {
				collider.GetComponent<IHit>()?.Stun(gameObject, freezTime, freezStrength);
			}
		}
	}
}
