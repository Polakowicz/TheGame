using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace script.Player.Weapon
{
    public class PlayerWeaponInputController : MonoBehaviour
    {
        //Components
        private PlayerManager player;
        private PlayerInput input;
        private MeleeWeapon meleeWeapon;
        private RangedWeapon rangedWeapon;

        //InputActions
        private InputAction switchWeaponAction;
        private InputAction basicAttackActinon;
        private InputAction strongerAttackAction;
        private InputAction alternativeAttackAction;
        private InputAction pullAction;
        private InputAction interactAcction;

        //Internal Variables
        private Weapon equippedWeapon;

        private void Start()
        {
            player = GetComponentInParent<PlayerManager>();
            input = GetComponentInParent<PlayerInput>();
            meleeWeapon = GetComponentInChildren<MeleeWeapon>();
            rangedWeapon = GetComponentInChildren<RangedWeapon>();

            equippedWeapon = meleeWeapon;

            CreatActionInputs();
            SubscribeToEvents();
        }
        private void CreatActionInputs()
        {
            switchWeaponAction = input.actions["Switch weapon"];
            basicAttackActinon = input.actions["Basic attack"];
            strongerAttackAction = input.actions["Stronger attack"];
            alternativeAttackAction = input.actions["Alternative attack"];
            pullAction = input.actions["Scroll"];
            interactAcction = input.actions["Interact"];
        }
        private void SubscribeToEvents()
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

        private void OnDestroy()
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

        private void Update()
        {
            var scrollInputValue = pullAction.ReadValue<float>();
            if (scrollInputValue != 0) {
                equippedWeapon.PerformBeamPullAction(scrollInputValue);
            }
        }

        private void SwitchWeapon(InputAction.CallbackContext context)
        {
            player.AudioManager.Play("SwitchWeapon");
            if (equippedWeapon.Type == Weapon.WeaponType.Blade) {
                equippedWeapon = rangedWeapon;
            } else {
                equippedWeapon = meleeWeapon;
            }

            player.ChangedWeapon(equippedWeapon.Type);

        }
        private void Finish(InputAction.CallbackContext context)
        {
            meleeWeapon.Interact();
        }

        //Weapons attakcs
        //===============
        private void PerformeBasicAttack(InputAction.CallbackContext context)
        {
            equippedWeapon.PerformBasicAttack();
        }

        private void CancelStrongerAttack(InputAction.CallbackContext context)
        {
            equippedWeapon.CancelStrongerAttack();
        }
        private void PerformStrongerAttack(InputAction.CallbackContext context)
        {
            equippedWeapon.PerformStrongerAttack();
        }

        private void StartAlternativeAttack(InputAction.CallbackContext context)
        {
            equippedWeapon.StartAlternativeAttack();
        }
        private void PerformAlternativeAttack(InputAction.CallbackContext context)
        {
            equippedWeapon.PerformAlternativeAttack();
        }
        private void CancelAlternativeAttack(InputAction.CallbackContext context)
        {
            equippedWeapon.CancelAlternativeAttack();
        }
    }
}
