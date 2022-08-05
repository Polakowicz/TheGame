using Interfaces;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public class PlayerMovement : MonoBehaviour, IKick
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

		//Internal variables
		private Vector2 direction;
		private bool isInDash;
		private float speed;

		void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			player = GetComponentInParent<PlayerManager>();
			input = GetComponentInParent<PlayerInput>();

			moveAction = input.actions["Move"];
			dashAction = input.actions["Dash"];

			dashAction.performed += PerformDash;
			player.OnBladeThrustStarted += PerformThrustDash;
			player.OnBeamPullTowardsEnemyStarted += PerformBeamPull;
			player.OnKicked += PerformKicked;

			speed = basicSpeed;
		}

		void OnDestroy()
		{
			dashAction.performed -= PerformDash;
			player.OnBladeThrustStarted -= PerformThrustDash;
			player.OnBeamPullTowardsEnemyStarted -= PerformBeamPull;
			player.OnKicked -= PerformKicked;
		}

		void Update()
		{
			if (disableImput > 0) return;

			if (!isInDash) {
				direction = moveAction.ReadValue<Vector2>();
			}
		}

		void FixedUpdate()
		{
			if (disableImput > 0) return;

			if (isInDash) {
				rb.velocity = direction.normalized * speed;
			} else {
				rb.velocity = direction * speed * player.playerData.speedMultiplier;
			}

			player.playerData.moveDireciton = rb.velocity;
		}

		void PerformDash(InputAction.CallbackContext context)
		{
			if (isInDash) {
				return;
			}
			isInDash = true;
			speed = dashSpeed;

			player.OnDodge?.Invoke();
			StartCoroutine(DashDelay(dashTime));
		}
		void PerformThrustDash(PlayerData data, float s, float t, int d)
		{
			direction = data.aimDirection;
			isInDash = true;
			speed = s;
			StartCoroutine(TrustDelay(t));
		}
		void PerformBeamPull(GameObject enemy, float v, float stunTime)
		{
			direction = enemy.transform.position - transform.position;
			var s = direction.magnitude;
			speed = v;
			var t = s / v;
			isInDash = true;
			StartCoroutine(BeamPullDelay(t, stunTime));
		}
		void PerformKicked(Vector2 direction, float v, float s)
		{
			var t = s / v;
			this.direction = direction;
			isInDash = true;
			speed = v;
			StartCoroutine(DashDelay(t));
		}

		IEnumerator DashDelay(float delay)
		{
			yield return new WaitForSeconds(delay);
			speed = basicSpeed;
			isInDash = false;
		}
		IEnumerator TrustDelay(float delay)
		{
			yield return new WaitForSeconds(delay);
			speed = basicSpeed;
			isInDash = false;
			player.EndBladeThrust();
		}
		IEnumerator BeamPullDelay(float delay, float stunTime)
		{
			yield return new WaitForSeconds(delay);
			speed = basicSpeed;
			isInDash = false;
			player.EndBeamPullTowardsEnemy(stunTime);
		}





		private delegate void VoidFunction();

		private readonly float KickTime = 0.5f;
		private int disableImput;

		public void Kick(Vector2 direction)
		{
			rb.velocity = direction;
			disableImput++;
			StartCoroutine(Wait(KickTime, () => disableImput--));

		}
		public void Kick(Vector2 direction, float force)
		{
			Kick(direction * force);
		}

		private IEnumerator Wait(float time, Action func)
		{
			yield return new WaitForSeconds(time);
			func();
		}

	}
}
