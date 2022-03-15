using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAim))]
public class PlayerAttack : MonoBehaviour
{
	//Components
	PlayerInput playerInput;
	Rigidbody2D rb;
	[SerializeField] Transform gun;
	[SerializeField] Transform sword;

	//Input actions
	InputAction fireAction;
	InputAction meleeAttackAction;

	//Parameter
	[SerializeField] GameObject bullet;
	[SerializeField] float fireRate = 0.2f;
	[SerializeField] LayerMask meleeAttackLayerMask;

	//Internal variables
	Coroutine fireCoroutine;

	void Start()
	{
		playerInput = GetComponent<PlayerInput>();
		rb = GetComponent<Rigidbody2D>();

		fireAction = playerInput.actions["Fire"];
		meleeAttackAction = playerInput.actions["Melee attack"];

		fireAction.started += OnWeaponStarFire;
		fireAction.canceled += OnWeaponEndFire;
		meleeAttackAction.performed += OnMeleeAttackPerformed;
	}

	private void OnDestroy()
	{
		fireAction.started -= OnWeaponStarFire;
		fireAction.canceled -= OnWeaponEndFire;
		meleeAttackAction.performed -= OnMeleeAttackPerformed;
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
			Instantiate(bullet, gun.position, gun.rotation);
			yield return new WaitForSeconds(fireRate);
		}
	}

	void OnMeleeAttackPerformed(InputAction.CallbackContext context)
	{
		Collider2D[] hits = Physics2D.OverlapBoxAll(sword.position, new Vector2(1,0.25f), rb.rotation, meleeAttackLayerMask);
		foreach (var hit in hits) {
			Debug.Log(hit);
		}
	}

}
