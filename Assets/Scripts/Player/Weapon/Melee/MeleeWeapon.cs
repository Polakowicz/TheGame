using Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
	[Serializable]
	public class MeleeWeapon : Weapon
	{
		private Manager player;
		private Movement movement;

		[Header("Attack")]
		[SerializeField] private Collider2D defaultRange;
		[SerializeField] private Collider2D powerupRange;
		[SerializeField] private LayerMask attackLayerMask;
		[SerializeField] private int basicAttackDamage = 20;
		private ContactFilter2D attackContactFilter;
		private Collider2D range;
		public LayerMask AttackLayerMask { get => attackLayerMask; }
		[Space(20)]

		[Header("Thrust")]
		[SerializeField] private Collider2D thrustRange;
		[SerializeField] private float thrustSpeed;
		[SerializeField] private float thrustTime;
		[SerializeField] private int thrustDmg;
		public int ThrustDmg { get => thrustDmg; }
		public bool ThrustActive { get; private set; }
		[Space(20)]

		[Header("Block")]
		[SerializeField] private Collider2D blockRange;
		[SerializeField] private LayerMask blockLayerMask;
		private ContactFilter2D blockContactFilter;
		public LayerMask BlockLayerMask { get => blockLayerMask; }
		public bool BlockActive { get; private set; }
	

		private void Start()
		{
			player = GetComponentInParent<Manager>();
			movement = GetComponentInParent<Movement>();
			Type = WeaponType.Blade;

			attackContactFilter = new ContactFilter2D {
				layerMask = attackLayerMask,
				useLayerMask = true,
				useTriggers = true
			};
			blockContactFilter = new ContactFilter2D {
				layerMask = blockLayerMask,
				useLayerMask = true
			};

			range = defaultRange;
			player.PowerUpController.OnPowerUpChanged += ChangePowerUp;
		}
		private void OnDestroy()
		{
			player.PowerUpController.OnPowerUpChanged -= ChangePowerUp;
		}

		public override void PerformBasicAttack()
		{
			player.AnimationController.BlaseAttack();
			player.AudioManager.Play("MeleeBasicAttack");
			List<Collider2D> hits = new List<Collider2D>();
			range.OverlapCollider(attackContactFilter, hits);
			foreach (Collider2D hit in hits) {
				hit.GetComponent<IHit>()?.Hit(basicAttackDamage);
			}
		}

		public override void PerformStrongerAttack()
		{
			if (ThrustActive) return;

			List<Collider2D> hits = new List<Collider2D>();
			thrustRange.OverlapCollider(attackContactFilter, hits);
			foreach (Collider2D hit in hits) {
				hit.GetComponent<IHit>()?.Hit(thrustDmg);
			}
			ThrustActive = true;
			movement.MoveInDirection(player.AimDirection, thrustSpeed, thrustTime,
				() => {
					ThrustActive = false;
				});
		}

		public override void PerformAlternativeAttack()
		{
			List<Collider2D> blocks = new List<Collider2D>();
			blockRange.OverlapCollider(blockContactFilter, blocks);
			foreach (Collider2D block in blocks) {
				block.GetComponent<IRiposte>()?.Riposte();
			}
			BlockActive = true;
		}
		public override void CancelAlternativeAttack()
		{
			BlockActive = false;
		}

		private void ChangePowerUp(PowerUp.PowerType type, bool active)
		{
			if (type != PowerUp.PowerType.DoubleBlade) return;

			range = active ? powerupRange : defaultRange;
		}
	}
}
