using Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Player
{
	public class MeleeWeapon : Weapon
	{
		// Used components
		private Manager playerManagerComponent;
		private Movement playerMovementComponent;

		[Header("Basic Attack")]
		// Triggers collider to find enemies in range of basic attack
		[SerializeField] private Collider2D basicAttackDefaultRange;
		[SerializeField] private Collider2D basicAttackPowerUpRange;

		// Currently picked range for attack
		private Collider2D currentAttackRange;

		// Layer mask for attack colliders
		[SerializeField] private LayerMask attackLayerMask;
		public LayerMask AttackLayerMask { get => attackLayerMask; }
		private ContactFilter2D attackContactFilter;

		// Damage dealt by basic attack
		[SerializeField] private int basicAttackDamage = 20;
		[Space(20)]


		[Header("Thrust Attack")]
		// Tigger collider to find enemies during thrust attack
		[SerializeField] private Collider2D thrustAttackRange;

		// Thrust settings
		// How fast does player move during thrust attack
		[SerializeField] private float thrustAttackSpeed;

		// How long does player move faster
		[SerializeField] private float thrustAttackTime;

		// Damage dealt by thurst attack
		[SerializeField] private int thrustAttackDamage;
		public int ThrustAttackDamage { get => thrustAttackDamage; }

		// Flag indicating if player is during thrust attack
		public bool IsPlayerDuringThurstAttack { get; private set; }
		[Space(20)]


		[Header("Block")]
		// Trigger collider to find bullets that should be riposeted by block
		[SerializeField] private Collider2D blockRange;

		// LayerMask for blocking objects
		[SerializeField] private LayerMask blockLayerMask;
		public LayerMask BlockLayerMask { get => blockLayerMask; }
		private ContactFilter2D blockContactFilter;

		// Flag indicating if block is active
		public bool IsBlockActive { get; private set; }

		// Flag indicating if player can riposte attacks
		public bool IsRiposteActive { get; private set; }

		// For how long does riposte last after initiate block
		[SerializeField] private float riposetTime;
		
		
		private void Awake()
		{
			// Get needed components
			playerManagerComponent = GetComponentInParent<Manager>();
			playerMovementComponent = GetComponentInParent<Movement>();

			// Set inherited weaopn type variable to blade
			Type = WeaponType.Blade;

			// Create contact filters for colliders
			attackContactFilter = new ContactFilter2D {
				layerMask = attackLayerMask,
				useLayerMask = true,
				useTriggers = true
			};
			blockContactFilter = new ContactFilter2D {
				layerMask = blockLayerMask,
				useLayerMask = true,
				useTriggers = true
			};

			// Set current attack range to basic (no power up active)
			currentAttackRange = basicAttackDefaultRange;
		}
		private void Start()
		{
			// Subscribe to get information when powerup was picked up or ended
			playerManagerComponent.PowerUpController.OnPowerUpChanged += ChangePowerUp;
		}
		private void OnDestroy()
		{
			// Unsubscribe for powerup changed info
			playerManagerComponent.PowerUpController.OnPowerUpChanged -= ChangePowerUp;
		}

		// Perform basic blade attack
		public override void PerformBasicAttack()
		{
			// Trigger blade attack animation
			playerManagerComponent.AnimationController.BladeAttack();

			// Play sword attack sound
			playerManagerComponent.AudioManager.Play("MeleeBasicAttack");

			// Find all enemies in range of attack
			List<Collider2D> hits = new List<Collider2D>();
			currentAttackRange.OverlapCollider(attackContactFilter, hits);

			// Deal damage to all enemies in range
			foreach (Collider2D hit in hits)
			{
				var hitInterface = hit.GetComponent<IHit>();
				Assert.IsNotNull(hit);
				hitInterface.Hit(gameObject, basicAttackDamage);
			}
		}

		// Perform thrust attack
		public override void PerformStrongerAttack()
		{
			// Cant do thrust attack when previous is still in progress
			if (IsPlayerDuringThurstAttack) return;

			// Find all enemies that are already in overlapping with thrust collider and deal damage to them
			List<Collider2D> hits = new List<Collider2D>();
			thrustAttackRange.OverlapCollider(attackContactFilter, hits);
			foreach (Collider2D hit in hits)
			{
				var hitInterface = hit.GetComponent<IHit>();
				Assert.IsNotNull(hit);
				hitInterface.Hit(gameObject, thrustAttackDamage);
			}

			IsPlayerDuringThurstAttack = true;

			// Perform fast movement (dash) in aiming direction and disable attack after
			playerMovementComponent.MoveInDirection(playerManagerComponent.AimDirection, thrustAttackSpeed, thrustAttackTime,
				() => {
					IsPlayerDuringThurstAttack = false;
				});
		}

		// Active block
		public override void PerformAlternativeAttack()
		{
			// Riposte bullets in range
			List<Collider2D> blocks = new List<Collider2D>();
			blockRange.OverlapCollider(blockContactFilter, blocks);
			foreach (Collider2D block in blocks)
			{
				var riposteInterface = block.GetComponent<IRiposte>();
				Assert.IsNotNull(riposteInterface);
				riposteInterface.Riposte(gameObject);
			}

			// Start Blocking
			IsBlockActive = true;
			IsRiposteActive = true;

			// Disable riposte after time even if block still active
			StartCoroutine(WaitAndDo(riposetTime, () => IsRiposteActive = false));
		}

		// Disble block
		public override void CancelAlternativeAttack()
		{
			IsBlockActive = false;
			IsRiposteActive = false;
		}

		private void ChangePowerUp(PowerUp.PowerType type, bool active)
		{
			// Only double blade powerup is relevant to this weapon
			if (type != PowerUp.PowerType.DoubleBlade) return;

			// If powerup active set current range to powerup range, if not to default range
			currentAttackRange = active ? basicAttackPowerUpRange : basicAttackDefaultRange;
		}
	}
}
