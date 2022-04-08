using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponInputController : MonoBehaviour
{
    //Components
    [SerializeField] PlayerInput playerInput;

    //InputActions
    InputAction switchWeaponAction;
    InputAction basicAttackActinon;
    InputAction strongerAttackAction;
    InputAction alternativeAttackAction;
    InputAction scrollAction;

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
        alternativeAttackAction = playerInput.actions["Alternative attack"];
        scrollAction = playerInput.actions["Scroll"];
    }

    void SubscribeToEvents()
	{
        switchWeaponAction.performed += SwitchWeapon;

        basicAttackActinon.performed += PerformeBasicAttack;

        strongerAttackAction.canceled += CancelStrongerAttack;
        strongerAttackAction.performed += PerformStrongerAttack;

        alternativeAttackAction.started += StartAlternativeAttack;
        alternativeAttackAction.performed += PerformAlternativeAttack;
        alternativeAttackAction.canceled += CancelAlternativeAttack;
    }

	void OnDestroy()
	{
        switchWeaponAction.performed -= SwitchWeapon;

        basicAttackActinon.performed -= PerformeBasicAttack;

        strongerAttackAction.canceled -= CancelStrongerAttack;
        strongerAttackAction.performed -= PerformStrongerAttack;

        alternativeAttackAction.started -= StartAlternativeAttack;
        alternativeAttackAction.performed -= PerformAlternativeAttack;
        alternativeAttackAction.canceled -= CancelAlternativeAttack;
    }

	void Update()
	{
        var scrollInputValue = scrollAction.ReadValue<float>();
        if (scrollInputValue != 0) {
            equippedWeapon.PerformBeamPullAction(scrollInputValue);
		}
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
	}

    //Weapons attakcs
    //===============
	void PerformeBasicAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.PerformBasicAttack();
	}

    void CancelStrongerAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.CancelStrongerAttack();
	}
    void PerformStrongerAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.PerformStrongerAttack();
	}

    void StartAlternativeAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.StartAlternativeAttack();
	}
    void PerformAlternativeAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.PerformAlternativeAttack();
	}
    void CancelAlternativeAttack(InputAction.CallbackContext context)
	{
        equippedWeapon.CancelAlternativeAttack();
	}
}
