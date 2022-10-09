using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Player
{
	public class ThrustAttackDealDamage : MonoBehaviour
	{
		// Components
		private BladePlayerWeapon bladeWeaponComponent;

		private void Awake()
		{
			// Get components
			bladeWeaponComponent = GetComponentInParent<BladePlayerWeapon>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			// Player must be during thrust attack
			if (!bladeWeaponComponent.IsPlayerDuringThurstAttack) return;

			// Object must be in mask from main weapon component
			if (bladeWeaponComponent.AttackLayerMask != (bladeWeaponComponent.AttackLayerMask | (1 << collision.gameObject.layer))) return;

			// Deal damage
			var hitInterface = collision.GetComponent<IHit>();
			Assert.IsNotNull(hitInterface);
			hitInterface.Hit(gameObject, bladeWeaponComponent.ThrustAttackDamage);
		}
	}
}
