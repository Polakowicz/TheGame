using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class PlayerAim : MonoBehaviour
	{
		[SerializeField]
		private Transform crosshair;
		private PlayerManager player;
		private PlayerInput input;

		private InputAction aimAction;
		private bool gamepad;
		private float gamepadCrosshariDistance;

		private void Start()
		{
			player = GetComponentInParent<PlayerManager>();
			input = GetComponentInParent<PlayerInput>();

			aimAction = input.actions["Aim"];
			gamepadCrosshariDistance = (crosshair.position - transform.position).magnitude;
		}

		private void Update()
		{
			Vector2 lookDirection;
			if (gamepad) {
				lookDirection = aimAction.ReadValue<Vector2>();
				if (lookDirection == Vector2.zero) {
					return;
				}
				crosshair.transform.localPosition = Vector2.up * gamepadCrosshariDistance;
			} else {
				var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(aimAction.ReadValue<Vector2>());
				crosshair.transform.position = mousePos;
				lookDirection = mousePos - (Vector2)transform.position;
			}
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90)); ;
			player.AimDirection = lookDirection.normalized;
		}

		public void OnControlsChanged(PlayerInput input)
		{
			//To change in future. It is public and made through the inspector because onControlsChange does not work
			gamepad = input.currentControlScheme.Equals("Gamepad");
			if (gamepad) {
				crosshair.localPosition = (Vector2)crosshair.localPosition.normalized * gamepadCrosshariDistance;
			}
		}
	}
}
