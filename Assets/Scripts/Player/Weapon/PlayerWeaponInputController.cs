using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace script.Player
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

        void Start()
        {
            player = GetComponentInParent<PlayerManager>();
            input = GetComponentInParent<PlayerInput>();
            meleeWeapon = GetComponentInChildren<MeleeWeapon>();
            rangedWeapon = GetComponentInChildren<RangedWeapon>();

            equippedWeapon = meleeWeapon;

            creatActionInputs();
            SubscribeToEvents();
        }

        void creatActionInputs()
        {
            switchWeaponAction = input.actions["Switch weapon"];
            basicAttackActinon = input.actions["Basic attack"];
            strongerAttackAction = input.actions["Stronger attack"];
            alternativeAttackAction = input.actions["Alternative attack"];
            pullAction = input.actions["Scroll"];
            interactAcction = input.actions["Interact"];
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
            if (equippedWeapon.Type == Weapon.WeaponType.Blade) {
                equippedWeapon = rangedWeapon;
            } else {
                equippedWeapon = meleeWeapon;
            }

            player.ChangedWeapon(equippedWeapon.Type);

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
}
