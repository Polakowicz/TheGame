using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	//Components
	PlayerInput playerInput;

	//Input actions
	InputAction aimAction;

	//Parameters
	[SerializeField] GameObject crosshair;
	[SerializeField] float crosshairDistanc = 2f;

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
		UpdateCrosshairPosition();
	}

	private void UpdateCrosshairPosition()
	{
		if (gamepad) {
			var aimDirection = aimAction.ReadValue<Vector2>();
			if (aimDirection == Vector2.zero) {
				return;
			}
			crosshair.transform.localPosition = aimDirection.normalized * crosshairDistanc;	
		} else {
			var mousePos = aimAction.ReadValue<Vector2>();
			crosshair.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
		}
	}

	public void OnControlsChanged(PlayerInput input)
	{
		gamepad = input.currentControlScheme.Equals("Gamepad");
		//TODO it is public and made through the inspector because onControlsChange does not work
	}
}
