using UnityEngine;

namespace Scripts.Player
{
	public class AnimationController : MonoBehaviour
	{
		private PlayerManager player;
		private SpriteRenderer spriteRenderer;
		private Animator animator;

		private void Start()
		{
			player = GetComponentInParent<PlayerManager>();
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

		//void TriggerBlasterAttackAnimation()
		//{
		//	animator.SetTrigger("Gun_fighting");
		//}




		void Die()
		{
			//animator.SetInteger("Health", eventSystem.playerData.HP);
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