using UnityEngine;

namespace Scripts.Player
{
	public class AnimationController : MonoBehaviour
	{
		private Manager player;
		private SpriteRenderer spriteRenderer;
		private Animator animator;

		private void Start()
		{
			player = GetComponentInParent<Manager>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
		}

		private void Update()
		{
			if (player.AimDirection.x > 0) {
				spriteRenderer.flipX = true;
			} else if (player.AimDirection.x < 0) {
				spriteRenderer.flipX = false;
			}

			animator.SetFloat("Speed", player.MoveDirection.magnitude);
		}

		public void TriggerBladeAttackAnimation()
		{
			animator.SetTrigger("Sword_fighting");
		}

		public void Die()
		{
			animator.SetTrigger("Die");
		}

		public void ChangeGun(Weapon.WeaponType type)
		{
			animator.SetBool("Blaster_equiped", type == Weapon.WeaponType.Blaster);
		}

		public void Dash()
		{
			animator.SetTrigger("Dodge");
		}
	}
}