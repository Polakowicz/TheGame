using Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player.Weapon
{
	[Serializable]
	public class MeleeWeapon : Weapon
	{
		private PlayerManager player;


		[SerializeField] private LayerMask melleWeaponLayerMask;


		[SerializeField] private Collider2D defaultRange;
		[SerializeField] private Collider2D powerupRange;
		[SerializeField] private Collider2D blockRange;

		//Internal variables
		ContactFilter2D attackContactFilter;
		Collider2D range;

		//Parameters
		[SerializeField] int basicAttackDamage;

		//
		[SerializeField] float thrustSpeed;
		[SerializeField] float thrustTime;
		[SerializeField] int thrustDmg;

		void Start()
		{
			player = GetComponentInParent<PlayerManager>();
			Type = WeaponType.Blade;

			attackContactFilter = new ContactFilter2D {
				layerMask = melleWeaponLayerMask,
				useLayerMask = true,
				useTriggers = true
			};

			range = defaultRange;
			player.powerUpController.OnPowerUpChanged += ChangePowerUp;
		}

		public override void PerformBasicAttack()
		{
			player.AudioManager.Play("MeleeBasicAttack");
			List<Collider2D> hits = new List<Collider2D>();
			range.OverlapCollider(attackContactFilter, hits);
			foreach (Collider2D hit in hits) {
				hit.GetComponent<IHit>()?.Hit(basicAttackDamage, IHit.HitWeapon.Sword);
				//if (hit.gameObject.layer == LayerMask.NameToLayer("Rock")) {
				//	hit.GetComponent<PushableRock>().Push(hit.transform.position - transform.position);
				//} else {
				//	hit.GetComponent<Enemy>().Damage(basicAttackDamage);
				//}

			}
			player.OnBladeAttack?.Invoke();
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
			player.StartBladeThrust(thrustSpeed, thrustTime, thrustDmg);
			//eventSystem.OnBladeAttack?.Invoke();
		}

		public override void PerformAlternativeAttack()
		{
			player.StartBladeBlock();
		}
		public override void CancelAlternativeAttack()
		{
			player.EndBladeBlock();
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
