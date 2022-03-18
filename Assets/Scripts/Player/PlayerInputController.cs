using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    //Components
    PlayerInput playerInput;

    //InputActions
    InputAction switchWeaponAction;
    InputAction performBasicAttackActinon;

    //Parameters
    [SerializeField] MeleeWeapon meleeWeapon;
    [SerializeField] RangedWeapon rangedWeapon;

    //Internal Variables
    Weapon equippedWeapon;
    bool equipedMeleeWeapon = true;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        equippedWeapon = meleeWeapon;

        creatActionInputs();
        SubscribeToEvents();
    }

    void creatActionInputs()
	{
        switchWeaponAction = playerInput.actions["Switch weapon"];
        performBasicAttackActinon = playerInput.actions["Basic attack"];
    }

    void SubscribeToEvents()
	{
        switchWeaponAction.performed += SwitchWeapon;
        performBasicAttackActinon.performed += PerformeBasicAttack;
	}

	void OnDestroy()
	{
        switchWeaponAction.performed += SwitchWeapon;
        performBasicAttackActinon.performed -= PerformeBasicAttack;
    }

    void SwitchWeapon(InputAction.CallbackContext context)
	{
		if (equipedMeleeWeapon) {
            equippedWeapon = rangedWeapon;
            equipedMeleeWeapon = false;
		} else {
            equippedWeapon = meleeWeapon;
            equipedMeleeWeapon = true;
        }
        Debug.Log("Switch weapon");
	}

	void PerformeBasicAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.PerformeBasicAttack();
	}
}
