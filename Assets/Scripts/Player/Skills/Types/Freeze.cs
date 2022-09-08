using Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
	public class Freeze : Skill
	{
		private Collider2D range;
		private LayerMask mask;
		private ContactFilter2D filter;

		[SerializeField] private float freezTime;
		[SerializeField] private float freezStrength;

		private void Start()
		{
			mask = LayerMask.GetMask("Enemy");
			range = GetComponent<Collider2D>();
			filter = new ContactFilter2D {
				layerMask = mask,
				useLayerMask = true,
				useTriggers = true
			};
		}

		public override void UseSkill()
		{
			Debug.Log($"Used skill: {SkillsController.SkillType.FreezeTime}");
			List<Collider2D> colliders = new List<Collider2D>();
			range.OverlapCollider(filter, colliders);
			foreach (Collider2D collider in colliders) {
				collider.GetComponent<IHit>()?.Stun(freezTime, freezStrength);
			}
		}
	}
}
