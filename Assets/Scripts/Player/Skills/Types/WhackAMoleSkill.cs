using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
	public class WhackAMoleSkill : Skill
	{
		private Manager player;

		private CircleCollider2D range;
		private ContactFilter2D filter;
		private LayerMask mask;

		// How fast radius grow during charging
		[SerializeField] private float radiusGrow;

		// How long enemy should be stuned after hit
		[SerializeField] private float stunTime;

		[SerializeField] private int damage;
		
		private bool isCharging;

		private void Awake()
		{
			// Get components
			player = GetComponentInParent<Manager>();
			range = GetComponent<CircleCollider2D>();

			// Set variables
			mask = LayerMask.GetMask("Enemy");
			filter = new ContactFilter2D {
				layerMask = mask,
				useLayerMask = true
			};
			range.radius = 0;
			isCharging = false;
		}

		private void Update()
		{
			// Grow collider radius when charging skill
			if (!isCharging) return;
			range.radius += radiusGrow * Time.deltaTime;
		}

		public override void StartUsingSkill()
		{
			isCharging = true;
			player.State = Manager.PlayerState.Charging;
		}
		public override void StopUsingSkill()
		{
			// Hit all enemies in range with skill
			List<Collider2D> colliders = new List<Collider2D>();
			range.OverlapCollider(filter, colliders);
			foreach (Collider2D collider in colliders) {
				collider.GetComponent<IHit>()?.StunHit(gameObject, damage, stunTime);
			}
			isCharging = false;
			range.radius = 0;
			player.State = Manager.PlayerState.Walk;
		}
	}
}