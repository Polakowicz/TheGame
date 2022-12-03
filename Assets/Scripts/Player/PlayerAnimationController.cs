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
		private SpriteRenderer gunSprite;
        [SerializeField] private GameObject gunObject;
        private Animator animator;

		private float gunPosition;

		private void Awake()
		{
            gunSprite = gunObject.GetComponent<SpriteRenderer>();
            player = GetComponentInParent<PlayerManager>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();

            gunPosition = gunObject.transform.localPosition.x;
        }

		private void Update()
		{
			// Set sprite direction towars crosshair
			if (player.AimDirection.x > 0) {
				spriteRenderer.flipX = true;
                gunSprite.flipY = true;
                gunObject.transform.localPosition = new Vector3(-gunPosition, gunObject.transform.localPosition.y, gunObject.transform.localPosition.z);
            } else if (player.AimDirection.x < 0) {
                gunObject.transform.localPosition = new Vector3(gunPosition, gunObject.transform.localPosition.y, gunObject.transform.localPosition.z);
                spriteRenderer.flipX = false;
                gunSprite.flipY = false;
            }

			animator.SetFloat(SpeedAnimationParameterName, player.MoveDirection.magnitude);
		}

		public void ChangeGun(PlayerWeapon.WeaponType type)
		{
			gunObject.SetActive(type == PlayerWeapon.WeaponType.Blaster);
			animator.SetBool(WeaponChangeAnimationParameterName, type == PlayerWeapon.WeaponType.Blaster);
		}
		public void BladeAttack() => animator.SetTrigger(BladeAttackAnimationTriggerName);
		public void Die() => animator.SetTrigger(DieAnimationTriggerName);
		public void Dash() => animator.SetTrigger(DashAnimationTriggerName);
		public void Interact() => animator.SetTrigger(CheckpointAnimationTriggerName);

	}
}