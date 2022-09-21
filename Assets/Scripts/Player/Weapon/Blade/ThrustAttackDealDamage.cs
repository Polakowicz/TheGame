using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Player
{
	public class ThrustAttackDealDamage : MonoBehaviour
	{
		// Main weapon component
		private BladePlayerWeapon maleeWeapon;

		private void Awake()
		{
			// Get components
			maleeWeapon = GetComponentInParent<BladePlayerWeapon>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			// Player must be during thrust attack
			if (!maleeWeapon.IsPlayerDuringThurstAttack) return;

			// Object must be in mask from main weapon component
			if (maleeWeapon.AttackLayerMask != (maleeWeapon.AttackLayerMask | (1 << collision.gameObject.layer))) return;

			// Deal damage
			var hitInterface = collision.GetComponent<IHit>();
			Assert.IsNotNull(hitInterface);
			hitInterface.Hit(gameObject, maleeWeapon.ThrustAttackDamage);
		}
	}
}
