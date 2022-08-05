using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace script.Player.Weapon
{
	[Serializable]
	public class MeleeWeapon : Weapon
	{
		//Components
		[SerializeField] PlayerManager eventSystem;
		[SerializeField] LayerMask melleWeaponLayerMask;
		[SerializeField] Collider2D defaultRange;
		[SerializeField] Collider2D powerupRange;
		[SerializeField] Collider2D blockRange;

		//Internal variables
		ContactFilter2D attackContactFilter;
		Collider2D range;

		//Parameters
		[SerializeField] int basicAttackDamage;

		[SerializeField] float thrustSpeed;
		[SerializeField] float thrustTime;
		[SerializeField] int thrustDmg;

		void Start()
		{
			Type = WeaponType.Blade;

			attackContactFilter = new ContactFilter2D {
				layerMask = melleWeaponLayerMask,
				useLayerMask = true,
				useTriggers = true
			};

			range = defaultRange;
			eventSystem.powerUpController.OnPowerUpChanged += ChangePowerUp;
		}

		public override void PerformBasicAttack()
		{
			FindObjectOfType<AudioManager>().Play("MeleeBasicAttack");
			List<Collider2D> hits = new List<Collider2D>();
			range.OverlapCollider(attackContactFilter, hits);
			foreach (Collider2D hit in hits) {
				Debug.Log(hit.gameObject.layer);
				if (hit.gameObject.layer == LayerMask.NameToLayer("Rock")) {
					hit.GetComponent<PushableRock>().Push(hit.transform.position - transform.position);
				} else {
					hit.GetComponent<Enemy>().Damage(basicAttackDamage);
				}

			}
			eventSystem.OnBladeAttack?.Invoke();
		}

		public void Interact()
		{
			List<Collider2D> hits = new List<Collider2D>();
			range.OverlapCollider(attackContactFilter, hits);
			foreach (Collider2D hit in hits) {
				if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
					hit.GetComponent<Enemy>().Finish();
					break;
				}

			}
		}

		public override void PerformStrongerAttack()
		{
			eventSystem.StartBladeThrust(thrustSpeed, thrustTime, thrustDmg);
			//eventSystem.OnBladeAttack?.Invoke();
		}

		public override void PerformAlternativeAttack()
		{
			eventSystem.StartBladeBlock();
		}
		public override void CancelAlternativeAttack()
		{
			eventSystem.EndBladeBlock();
		}

		private void ChangePowerUp(PowerUp.PowerType type, bool active)
		{
			if (type != PowerUp.PowerType.DoubleBlade) {
				return;
			}

			range = active ? powerupRange : defaultRange;
		}
	}
}
