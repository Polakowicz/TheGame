using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemies
{
	public class WalkTowardsPlayerBehavoiur : StateMachineBehaviour
	{
		[SerializeField] private float walkSpeed;

		private Rigidbody2D rigidbody;
		private Enemy enemy;

		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			rigidbody = animator.GetComponent<Rigidbody2D>();
			enemy = animator.GetComponent<Enemy>();
		}

		override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			var direction = (Vector2)enemy.Player.transform.position - rigidbody.position;
			rigidbody.velocity = direction.normalized * walkSpeed;
		}
	}
}
