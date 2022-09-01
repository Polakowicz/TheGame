using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
	public class WhackAMole : Skill
	{
		private CircleCollider2D range;
		private ContactFilter2D filter;
		[SerializeField] private LayerMask mask;

		[SerializeField] private float radiusGrow;
		[SerializeField] private int damage;
		[SerializeField] private float stunTime;

		private bool charging;

		private void Start()
		{
			range = GetComponent<CircleCollider2D>();
			filter = new ContactFilter2D {
				layerMask = mask,
				useLayerMask = true
			};

			range.radius = 0;
			charging = false;
		}

		private void Update()
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
			List<Collider2D> colliders = new List<Collider2D>();
			range.OverlapCollider(filter, colliders);
			foreach (Collider2D collider in colliders) {
				collider.GetComponent<IHit>()?.StunHit(damage, stunTime, IHit.HitWeapon.WhackAMole);
			}
			charging = false;
			range.radius = 0;
		}
	}
}