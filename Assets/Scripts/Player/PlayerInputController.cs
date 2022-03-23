using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    //Components
    [SerializeField] PlayerInput playerInput;

    //InputActions
    InputAction switchWeaponAction;
    InputAction basicAttackActinon;
    InputAction strongerAttackAction;
    InputAction alternativeAttackAction;

    //Parameters
    [SerializeField] MeleeWeapon meleeWeapon;
    [SerializeField] RangedWeapon rangedWeapon;

    //Internal Variables
    Weapon equippedWeapon;
    bool equipedMeleeWeapon = true;

    void Start()
    {
        equippedWeapon = meleeWeapon;

        creatActionInputs();
        SubscribeToEvents();
    }

    void creatActionInputs()
	{
        switchWeaponAction = playerInput.actions["Switch weapon"];
        basicAttackActinon = playerInput.actions["Basic attack"];
        strongerAttackAction = playerInput.actions["Stronger attack"];
        //alternativeAttackAction = playerInput.actions["Alternative attack"];
    }

    void SubscribeToEvents()
	{
        switchWeaponAction.performed += SwitchWeapon;
        basicAttackActinon.performed += PerformeBasicAttack;
        //strongerAttackAction.started += StartStrongerAttack;
        strongerAttackAction.canceled += CancelStrongerAttack;
        strongerAttackAction.performed += PerformStrongerAttack;
    }

	void OnDestroy()
	{
        switchWeaponAction.performed -= SwitchWeapon;
        basicAttackActinon.performed -= PerformeBasicAttack;
        //strongerAttackAction.started -= StartStrongerAttack;
        strongerAttackAction.canceled -= CancelStrongerAttack;
        strongerAttackAction.performed -= PerformStrongerAttack;
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

    //Weapons attakcs
    //===============
	void PerformeBasicAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.PerformBasicAttack();
	}

    void StartStrongerAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.StartStrongerAttack();
	}
    void CancelStrongerAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.CancelStrongerAttack();
	}
    void PerformStrongerAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.PerformStrongerAttack();
	}

    void PerformAlternativeAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.PerformAlternativeAttack();
	}
}
