using Scripts.Player.Weapon;
using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	[SerializeField] PlayerManager eventSystem;
	SpriteRenderer spriteRenderer;
	Animator animator;
	bool gun;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();

		eventSystem.OnBladeAttack += TriggerBladeAttackAnimation;
		//eventSystem.OnGunFire += TriggerBlasterAttackAnimation;
		eventSystem.OnGunChanged += ChangeGun;
		eventSystem.OnDodge += Dodge;
	}

	private void Update()
	{
		if (eventSystem.playerData.aimDirection.x > 0) {
			spriteRenderer.flipX = true;
		} else if (eventSystem.playerData.aimDirection.x < 0) {
			spriteRenderer.flipX = false;
		}

		animator.SetFloat("Speed", eventSystem.playerData.moveDireciton.magnitude);
	}

	void TriggerBladeAttackAnimation()
	{
		animator.SetTrigger("Sword_fighting");
	}

	//void TriggerBlasterAttackAnimation()
	//{
	//	animator.SetTrigger("Gun_fighting");
	//}

	void ChangeGun()
	{
		animator.SetBool("Blaster_equiped", eventSystem.playerData.weapon == Weapon.WeaponType.Blaster ? true : false);
	}

	void Dodge()
	{
		animator.SetTrigger("Dodge");
	}

	void Die()
	{
		//animator.SetInteger("Health", eventSystem.playerData.HP);
	}
}