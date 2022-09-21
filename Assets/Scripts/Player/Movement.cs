using System;
using System.Collections;
using Scripts.Interfaces;
using Scripts.Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class Movement : ExtendedMonoBehaviour
	{
		//Components
		private Rigidbody2D rb;
		private PlayerManager player;
		private PlayerInput input;

		//Input actions
		private InputAction moveAction;
		private InputAction dashAction;

		//Parameters
		[SerializeField] private float basicSpeed = 5f;
		[SerializeField] private float dashSpeed = 30f;
		[SerializeField] private float dashTime = 0.1f;
		private Vector2 direction;
		public float SpeedMultiplier { get; set; } = 1f;

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			player = GetComponentInParent<PlayerManager>();
			input = GetComponentInParent<PlayerInput>();

			moveAction = input.actions["Move"];
			dashAction = input.actions["Dash"];

			dashAction.performed += PerformDash;
		}
		private void OnDestroy()
		{
			dashAction.performed -= PerformDash;
		}

		private void Update()
		{
			if (player.State == PlayerManager.PlayerState.Dash) return;

			if(player.State == PlayerManager.PlayerState.Walk) {
				direction = moveAction.ReadValue<Vector2>();
				rb.velocity = basicSpeed * SpeedMultiplier * direction.normalized;
				player.MoveDirection = rb.velocity;
			} else {
				rb.velocity = Vector2.zero;
			}
		}

		//Dash
		private void PerformDash(InputAction.CallbackContext context)
		{
			if (player.State != PlayerManager.PlayerState.Walk) return;

			MoveInDirection(direction, dashSpeed, dashTime, null);
			player.AnimationController.Dash();
		}
		public void MoveInDirection(Vector2 direction, float speed, float time, Action func)
		{
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
			player.State = PlayerManager.PlayerState.Dash;
			rb.velocity = direction.normalized * speed;
			StartCoroutine(Dashing(time, func));
		}
		private IEnumerator Dashing(float time, Action after)
		{
			yield return new WaitForSeconds(time);
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
			player.State = PlayerManager.PlayerState.Walk;
			after?.Invoke();
		}
	}
}
