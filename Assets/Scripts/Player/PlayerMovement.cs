using Interfaces;
using System;
using System.Collections;
using Scripts.Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class PlayerMovement : ExtendedMonoBehaviour, IKick
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
		private float speed;

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			player = GetComponentInParent<PlayerManager>();
			input = GetComponentInParent<PlayerInput>();

			moveAction = input.actions["Move"];
			dashAction = input.actions["Dash"];

			dashAction.performed += PerformDash;
			//player.OnBladeThrustStarted += PerformThrustDash;
			player.OnKicked += PerformKicked;

			speed = basicSpeed;
		}

		private void OnDestroy()
		{
			dashAction.performed -= PerformDash;
			//player.OnBladeThrustStarted -= PerformThrustDash;
			player.OnKicked -= PerformKicked;
		}

		private void Update()
		{
			if (player.State == PlayerManager.PlayerState.Dash) return;

			direction = moveAction.ReadValue<Vector2>();
			rb.velocity = direction.normalized * basicSpeed;
			player.playerData.moveDireciton = rb.velocity;
		}


		private void PerformDash(InputAction.CallbackContext context)
		{
			Dash(direction, dashSpeed, dashTime, null);
		}
		public void Dash(Vector2 direction, float speed, float time, Action func)
		{
			if (player.State == PlayerManager.PlayerState.Dash) return;

			player.State = PlayerManager.PlayerState.Dash;
			rb.velocity = direction.normalized * speed;
			StartCoroutine(Dashing(time, func));
		}
		private IEnumerator Dashing(float time, Action after)
		{
			yield return new WaitForSeconds(time);
			speed = basicSpeed;
			player.State = PlayerManager.PlayerState.Walk;
			after?.Invoke();
		}







		void PerformThrustDash(PlayerData data, float s, float t, int d)
		{
			direction = data.aimDirection;
			player.State = PlayerManager.PlayerState.Dash;
			speed = s;
			StartCoroutine(TrustDelay(t));
		}

		void PerformKicked(Vector2 direction, float v, float s)
		{
			var t = s / v;
			this.direction = direction;
			player.State = PlayerManager.PlayerState.Dash;
			speed = v;
			StartCoroutine(DashDelay(t));
		}

		IEnumerator DashDelay(float delay)
		{
			yield return new WaitForSeconds(delay);
			speed = basicSpeed;
			player.State = PlayerManager.PlayerState.Walk;
		}
		IEnumerator TrustDelay(float delay)
		{
			yield return new WaitForSeconds(delay);
			speed = basicSpeed;
			player.State = PlayerManager.PlayerState.Walk;
			//player.EndBladeThrust();
		}
		IEnumerator BeamPullDelay(float delay, float stunTime)
		{
			yield return new WaitForSeconds(delay);
			speed = basicSpeed;
			player.State = PlayerManager.PlayerState.Walk;
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
