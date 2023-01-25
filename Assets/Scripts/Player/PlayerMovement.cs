using System;
using System.Collections;
using Scripts.Interfaces;
using Scripts.Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class PlayerMovement : ExtendedMonoBehaviour
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

		// Layer masks to deactivate collisions
		private LayerMask playerLayerMask;
		private LayerMask enemyLayerMask;

		// [whereistheguru]
		public static bool playerControlsEnabled = true;

		private void Awake()
		{
			playerControlsEnabled = true;

            rb = GetComponent<Rigidbody2D>();
			player = GetComponentInParent<PlayerManager>();
			input = GetComponentInParent<PlayerInput>();

			moveAction = input.actions["Move"];
			dashAction = input.actions["Dash"];

			playerLayerMask = LayerMask.NameToLayer("Player");
			enemyLayerMask = LayerMask.NameToLayer("Enemy");
		}
		private void Start()
		{
			dashAction.performed += PerformDash;
		}
		private void OnDestroy()
		{
			dashAction.performed -= PerformDash;
		}

		private void Update()
		{
			// [whereistheguru]
			if(playerControlsEnabled){
				if (player.State == PlayerManager.PlayerState.Dash) return;

				if(player.State == PlayerManager.PlayerState.Walk) {
					direction = moveAction.ReadValue<Vector2>();
					rb.velocity = basicSpeed * SpeedMultiplier * direction.normalized;
					player.MoveDirection = rb.velocity;
				} else {
					rb.velocity = Vector2.zero;
				}
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
			// Disable collisions with enemy
			Physics2D.IgnoreLayerCollision(playerLayerMask, enemyLayerMask);

			// Set player state to dash
			player.State = PlayerManager.PlayerState.Dash;

			// set dash
			rb.velocity = direction.normalized * speed;
			StartCoroutine(Dashing(time, func));
		}
		private IEnumerator Dashing(float time, Action after)
		{
			yield return new WaitForSeconds(time);

			// Active collisions
			Physics2D.IgnoreLayerCollision(playerLayerMask, enemyLayerMask, false);

			// Reset state
			player.State = PlayerManager.PlayerState.Walk;
			after?.Invoke();
		}
	}
}
