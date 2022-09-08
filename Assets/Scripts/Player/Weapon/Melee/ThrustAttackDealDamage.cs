using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Player
{
	public class ThrustAttackDealDamage : MonoBehaviour
	{
		private MeleeWeapon weapon;

		private void Start()
		{
			weapon = GetComponentInParent<MeleeWeapon>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!weapon.ThrustActive) return;

			if (!(weapon.AttackLayerMask !=
				(weapon.AttackLayerMask | (1 << collision.gameObject.layer))))
				return;

			collision.GetComponent<IHit>()?.Hit(weapon.ThrustDmg);
		}
	}
}
