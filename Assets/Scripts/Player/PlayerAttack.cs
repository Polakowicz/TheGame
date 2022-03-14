using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAim))]
public class PlayerAttack : MonoBehaviour
{
	//Components
	PlayerInput playerInput;
	Rigidbody2D rb;

	//Input actions
	InputAction fireAction;

	//Parameter
	[SerializeField] GameObject bullet;
	[SerializeField] float fireRate = 0.2f;

	//Internal variables
	Coroutine fireCoroutine;
	

	void Start()
	{
		playerInput = GetComponent<PlayerInput>();
		rb = GetComponent<Rigidbody2D>();

		fireAction = playerInput.actions["Fire"];
		fireAction.started += OnWeaponStarFire;
		fireAction.canceled += OnWeaponEndFire;
	}

	private void OnDestroy()
	{
		fireAction.started -= OnWeaponStarFire;
		fireAction.canceled -= OnWeaponEndFire;
	}

	void OnWeaponStarFire(InputAction.CallbackContext context)
	{
		fireCoroutine = StartCoroutine(Shoot());
	}

	void OnWeaponEndFire(InputAction.CallbackContext context)
	{
		StopCoroutine(fireCoroutine);
	}

	IEnumerator Shoot()
	{
		while (true) {
			Instantiate(bullet, rb.position, rb.transform.rotation);
			yield return new WaitForSeconds(fireRate);
		}
	}

}
