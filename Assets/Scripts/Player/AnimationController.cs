using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	PlayerEventSystem eventSystem;
	SpriteRenderer spriteRenderer;
	Animator animator;
	bool gun;

	private void Start()
	{
		eventSystem = GetComponent<PlayerEventSystem>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();

		eventSystem.OnBladeAttack += TriggerBladeAttackAnimation;
		//eventSystem.OnGunFire += TriggerBlasterAttackAnimation;
		eventSystem.OnGunChanged += ChangeGun;
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
		animator.SetBool("Blaster_equiped", eventSystem.playerData.weapon == PlayerData.Weapon.Blaster ? true : false);
	}
}