using UnityEngine;

namespace Scripts.Player
{
	public class PlayerAnimationController : MonoBehaviour
	{
		// Animator parameter names
		private static readonly string SpeedAnimationParameterName = "Speed";
		private static readonly string WeaponChangeAnimationParameterName = "Blaster_equiped";
		private static readonly string BladeAttackAnimationTriggerName = "Sword_fighting";
		private static readonly string DieAnimationTriggerName = "Die";
		private static readonly string DashAnimationTriggerName = "Dodge";
		private static readonly string CheckpointAnimationTriggerName = "Checkpoint";

		private PlayerManager player;
		private SpriteRenderer spriteRenderer;
		private Animator animator;

		private void Awake()
		{
			player = GetComponentInParent<PlayerManager>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
		}

		private void Update()
		{
			// Set sprite direction towars crosshair
			if (player.AimDirection.x > 0) {
				spriteRenderer.flipX = true;
			} else if (player.AimDirection.x < 0) {
				spriteRenderer.flipX = false;
			}

			animator.SetFloat(SpeedAnimationParameterName, player.MoveDirection.magnitude);
		}

		public void ChangeGun(PlayerWeapon.WeaponType type) => animator.SetBool(WeaponChangeAnimationParameterName, type == PlayerWeapon.WeaponType.Blaster);
		public void BladeAttack() => animator.SetTrigger(BladeAttackAnimationTriggerName);
		public void Die() => animator.SetTrigger(DieAnimationTriggerName);
		public void Dash() => animator.SetTrigger(DashAnimationTriggerName);
		public void Interact() => animator.SetTrigger(CheckpointAnimationTriggerName);

	}
}