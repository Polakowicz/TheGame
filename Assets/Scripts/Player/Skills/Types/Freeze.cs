using Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
	public class Freeze : Skill
	{
		private Collider2D range;
		private ContactFilter2D filter;
		[SerializeField] private LayerMask mask;

		[SerializeField] private float freezTime;
		[SerializeField] private float freezStrength;

		private void Start()
		{
			range = GetComponent<Collider2D>();
			filter = new ContactFilter2D {
				layerMask = mask,
				useLayerMask = true,
				useTriggers = true
			};
		}

		public override void UseSkill()
		{
			List<Collider2D> colliders = new List<Collider2D>();
			range.OverlapCollider(filter, colliders);
			foreach (Collider2D collider in colliders) {
				collider.GetComponent<IHit>().Stun(IHit.HitWeapon.SkillFreeze, freezTime, freezStrength);
			}
		}
	}
}
