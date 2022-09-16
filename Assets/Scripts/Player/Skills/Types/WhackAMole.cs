using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
	public class WhackAMole : Skill
	{
		private Manager player;
		private CircleCollider2D range;
		private ContactFilter2D filter;
		private LayerMask mask;

		[SerializeField] private float radiusGrow;
		[SerializeField] private int damage;
		[SerializeField] private float stunTime;

		private bool charging;

		private void Start()
		{
			player = GetComponentInParent<Manager>();
			range = GetComponent<CircleCollider2D>();
			mask = LayerMask.GetMask("Enemy");
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
			Debug.Log("Whack-a-mole start charging");
			charging = true;
			player.State = Manager.PlayerState.Charging;
		}
		public override void StopUsingSkill()
		{
			Debug.Log("Whack-a-mole attck");
			List<Collider2D> colliders = new List<Collider2D>();
			range.OverlapCollider(filter, colliders);
			foreach (Collider2D collider in colliders) {
				collider.GetComponent<IHit>()?.StunHit(gameObject, damage, stunTime);
			}
			charging = false;
			range.radius = 0;
			player.State = Manager.PlayerState.Walk;
		}
	}
}