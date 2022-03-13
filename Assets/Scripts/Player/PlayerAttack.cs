using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	//Components
	PlayerInput playerInput;

	//Internal variables
	bool gamepad;

	void Start()
	{
		playerInput = GetComponent<PlayerInput>();

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
			Debug.Log("Gamepad controlls");
		} else {
			Debug.Log("Keyboard controlls");
		}
	}

	public void OnControlsChanged(PlayerInput input)
	{
		gamepad = input.currentControlScheme.Equals("Gamepad");
		//TODO it is public and made throw the inspector because onControlsChange does not work
	}
}
