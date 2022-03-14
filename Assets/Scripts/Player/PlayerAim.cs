using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
	//Components
	PlayerInput playerInput;
	Rigidbody2D rb;
	[SerializeField] Transform crosshair;

	//Input actions
	InputAction aimAction;

	//Parameters
	[SerializeField] float gamepadCrosshariDistance = 1f;

	//Internal variables
	bool gamepad;

	void Start()
	{
		playerInput = GetComponent<PlayerInput>();
		rb = GetComponent<Rigidbody2D>();
		aimAction = playerInput.actions["Aim"];
		//playerInput.onControlsChanged += OnControlsChanged;
	}

	void OnDestroy()
	{
		//playerInput.onControlsChanged -= OnControlsChanged;
	}

	void Update()
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
		rb.rotation = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
	}

	public void OnControlsChanged(PlayerInput input)
	{
		gamepad = input.currentControlScheme.Equals("Gamepad");
		//To change in future. It is public and made through the inspector because onControlsChange does not work
	}
}
