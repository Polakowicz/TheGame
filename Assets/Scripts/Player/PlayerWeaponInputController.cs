using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponInputController : MonoBehaviour
{
    //Components
    [SerializeField] PlayerEventSystem playerEventSystem;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] MeleeWeapon meleeWeapon;
    [SerializeField] RangedWeapon rangedWeapon;

    //InputActions
    InputAction switchWeaponAction;
    InputAction basicAttackActinon;
    InputAction strongerAttackAction;
    InputAction alternativeAttackAction;
    InputAction pullAction;
    InputAction interactAcction;

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
        pullAction = playerInput.actions["Scroll"];
        interactAcction = playerInput.actions["Interact"];
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

        interactAcction.performed += Finish;
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

        interactAcction.performed -= Finish;
    }

	void Update()
	{
        var scrollInputValue = pullAction.ReadValue<float>();
        if (scrollInputValue != 0) {
            equippedWeapon.PerformBeamPullAction(scrollInputValue);
		}
	}

	void SwitchWeapon(InputAction.CallbackContext context)
	{
        FindObjectOfType<AudioManager>().Play("SwitchWeapon");
		if (equipedMeleeWeapon) {
            equippedWeapon = rangedWeapon;
            equipedMeleeWeapon = false;
            playerEventSystem.ChangedWeapon(PlayerData.Weapon.Blaster);

        } else {
            equippedWeapon = meleeWeapon;
            equipedMeleeWeapon = true;
            playerEventSystem.ChangedWeapon(PlayerData.Weapon.Blade);
        }
        
	}
    void Finish(InputAction.CallbackContext context)
	{
        meleeWeapon.Interact();
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
