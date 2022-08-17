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
		private PlayerMovement movement;

		[Header("Weapon Colliders")]
		[SerializeField] private Collider2D defaultRange;
		[SerializeField] private Collider2D powerupRange;
		[SerializeField] private Collider2D blockRange;
		[SerializeField] private Collider2D thrustRange;
		[SerializeField] private LayerMask melleWeaponLayerMask;
		private ContactFilter2D attackContactFilter;
		private ContactFilter2D interactContactFilter;
		private Collider2D range;
		public LayerMask AttackLayerMask { get => melleWeaponLayerMask; }
		[Space(20)]

		[Header("Thrust")]
		[SerializeField] private float thrustSpeed;
		[SerializeField] private float thrustTime;
		[SerializeField] private int thrustDmg;
		public int ThrustDmg { get => thrustDmg; }
		public bool ThrustActive { get; private set; }
		[Space(20)]

		[SerializeField] private int basicAttackDamage = 20;

		private void Start()
		{
			player = GetComponentInParent<PlayerManager>();
			movement = GetComponentInParent<PlayerMovement>();
			Type = WeaponType.Blade;

			attackContactFilter = new ContactFilter2D {
				layerMask = melleWeaponLayerMask,
				useLayerMask = true,
				useTriggers = true
			};
			interactContactFilter = new ContactFilter2D().NoFilter();

			range = defaultRange;
			player.powerUpController.OnPowerUpChanged += ChangePowerUp;
		}
		private void OnDestroy()
		{
			player.powerUpController.OnPowerUpChanged -= ChangePowerUp;
		}

		public override void PerformBasicAttack()
		{
			player.AnimationController.TriggerBladeAttackAnimation();
			player.AudioManager.Play("MeleeBasicAttack");
			List<Collider2D> hits = new List<Collider2D>();
			range.OverlapCollider(attackContactFilter, hits);
			foreach (Collider2D hit in hits) {
				hit.GetComponent<IHit>()?.Hit(basicAttackDamage, IHit.HitWeapon.Sword);
			}
		}

		public void Interact()
		{
			List<Collider2D> hits = new List<Collider2D>();
			range.OverlapCollider(interactContactFilter, hits);
			foreach (Collider2D hit in hits) {
				hit.GetComponent<IInteract>()?.Interact();
			}
		}

		public override void PerformStrongerAttack()
		{
			if (ThrustActive) return;

			List<Collider2D> hits = new List<Collider2D>();
			thrustRange.OverlapCollider(attackContactFilter, hits);
			foreach (Collider2D hit in hits) {
				hit.GetComponent<IHit>()?.Hit(thrustDmg, IHit.HitWeapon.Sword);
			}
			ThrustActive = true;
			movement.Dash(player.playerData.aimDirection, thrustSpeed, thrustTime,
				() => {
					ThrustActive = false;
				});
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
			if (type != PowerUp.PowerType.DoubleBlade) return;

			range = active ? powerupRange : defaultRange;
		}
	}
}
