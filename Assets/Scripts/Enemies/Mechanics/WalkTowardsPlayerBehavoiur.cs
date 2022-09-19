using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemies
{
	public class WalkTowardsPlayerBehavoiur : StateMachineBehaviour
	{
		[SerializeField] private float walkSpeed;

		private Rigidbody2D rigidbody;
		private Enemy manager;

		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			rigidbody = animator.GetComponent<Rigidbody2D>();
			manager = animator.GetComponent<Enemy>();
		}

		override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			manager.SpriteRenderer.flipX = manager.Player.transform.position.x > animator.transform.position.x;

			var direction = (Vector2)manager.Player.transform.position - rigidbody.position;
			rigidbody.velocity = direction.normalized * walkSpeed * manager.Data.speedMultiplier;
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			rigidbody.velocity = Vector2.zero;
		}
	}
}
