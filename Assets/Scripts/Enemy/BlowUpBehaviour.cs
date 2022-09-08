using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemies
{
    public class BlowUpBehaviour : StateMachineBehaviour
    {
		private CircleCollider2D blowRange;
		[SerializeField] private int damage;

		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			blowRange = animator.GetComponent<CircleCollider2D>();

			var range = blowRange.radius;
			List<Collider2D> hits = new List<Collider2D>();
			blowRange.OverlapCollider(new ContactFilter2D().NoFilter(), hits);
			foreach (Collider2D hit in hits) {
				var distance = Vector2.Distance(animator.transform.position, hit.transform.position);
				var dmg = (range - distance) / range * damage;
				if(hit.TryGetComponent<IHit>(out IHit hitInt)) {
					hitInt.Hit(Mathf.FloorToInt(dmg), IHit.HitWeapon.OTHER);
				}
			}
		}

		override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			Destroy(animator.gameObject);
		}
	}
}
