using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAim))]
public class PlayerAttack : MonoBehaviour
{
	//Components
	PlayerInput playerInput;
	PlayerAim playerAim;

	//Input actions
	InputAction fire;

	void Start()
	{
		playerInput = GetComponent<PlayerInput>();
		playerAim = GetComponent<PlayerAim>();

		fire = playerInput.actions["Fire"];
		fire.started += OnWeaponStarFire;
		fire.canceled += OnWeaponEndFire;
	}

	private void OnDestroy()
	{
		fire.started -= OnWeaponStarFire;
		fire.canceled -= OnWeaponEndFire;
	}

	void OnWeaponStarFire(InputAction.CallbackContext context)
	{
		Debug.Log("Start fire");
	}

	void OnWeaponEndFire(InputAction.CallbackContext context)
	{
		Debug.Log("End fire");
	}

}
