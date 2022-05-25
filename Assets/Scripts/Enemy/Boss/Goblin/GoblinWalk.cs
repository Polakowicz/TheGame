using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWalk : StateMachineBehaviour
{
	Goblin goblin;
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;

	private float defaultSpeed = 1;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		goblin = animator.GetComponent<Goblin>();
		rb = animator.GetComponent<Rigidbody2D>();
		spriteRenderer = animator.GetComponent<SpriteRenderer>();
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		var direction = goblin.Player.transform.position - animator.transform.position;
		rb.velocity = direction.normalized * defaultSpeed;

		if (direction.x < 0) {
			spriteRenderer.flipX = true;
		} else if (direction.x > 0) {
			spriteRenderer.flipX = false;
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		rb.velocity = Vector2.zero;
	}
}
