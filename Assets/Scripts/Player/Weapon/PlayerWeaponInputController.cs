using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    public class PlayerWeaponInputController : MonoBehaviour
    {
        //Components
        private PlayerManager player;
        private PlayerInput input;
        
        //InputActions
        private InputAction switchWeaponAction;
        private InputAction basicAttackActinon;
        private InputAction strongerAttackAction;
        private InputAction alternativeAttackAction;
        private InputAction pullAction;

        //Weapons
        private BladePlayerWeapon bladeWeapon;
        private BlasterPlayerWeapon blasterWeapon;
        private PlayerWeapon equippedWeapon;

        private void Awake()
        {
            // Get components
            player = GetComponentInParent<PlayerManager>();
            input = GetComponentInParent<PlayerInput>();
            bladeWeapon = GetComponentInChildren<BladePlayerWeapon>();
            blasterWeapon = GetComponentInChildren<BlasterPlayerWeapon>();

            // Set variables
            equippedWeapon = bladeWeapon;

			// Create action inputs
			switchWeaponAction = input.actions["Switch weapon"];
			basicAttackActinon = input.actions["Basic attack"];
			strongerAttackAction = input.actions["Stronger attack"];
			alternativeAttackAction = input.actions["Alternative attack"];
			pullAction = input.actions["Scroll"];
		}
		private void Start()
        {
            // Ssubscribe to events
			switchWeaponAction.performed += SwitchWeapon;
			basicAttackActinon.performed += PerformeBasicAttack;
			strongerAttackAction.canceled += CancelStrongerAttack;
			strongerAttackAction.performed += PerformStrongerAttack;
			alternativeAttackAction.started += StartAlternativeAttack;
			alternativeAttackAction.performed += PerformAlternativeAttack;
			alternativeAttackAction.canceled += CancelAlternativeAttack;
		}
        private void OnDestroy()
        {
            // Unsubscribe events
            switchWeaponAction.performed -= SwitchWeapon;
            basicAttackActinon.performed -= PerformeBasicAttack;
            strongerAttackAction.canceled -= CancelStrongerAttack;
            strongerAttackAction.performed -= PerformStrongerAttack;
            alternativeAttackAction.started -= StartAlternativeAttack;
            alternativeAttackAction.performed -= PerformAlternativeAttack;
            alternativeAttackAction.canceled -= CancelAlternativeAttack;
        }

        private void Update()
        {
            // Perform pull if icroll input
            var scrollInputValue = pullAction.ReadValue<float>();
            if (scrollInputValue != 0) {
                equippedWeapon.PerformBeamPullAction(scrollInputValue);
            }
        }

        private void SwitchWeapon(InputAction.CallbackContext context)
        {
            // Change equiped weapon 
			if (equippedWeapon.Type == PlayerWeapon.WeaponType.Blade) {
                equippedWeapon = blasterWeapon;
            } else {
                equippedWeapon = bladeWeapon;
            }

			// Play switch weapon sound
			player.AudioManager.Play("SwitchWeapon");

			// Play switch weapon animation
			player.AnimationController.ChangeGun(equippedWeapon.Type);
		}

		//Weapons attakcs
		private void PerformeBasicAttack(InputAction.CallbackContext context) => equippedWeapon.PerformBasicAttack();

		private void CancelStrongerAttack(InputAction.CallbackContext context) => equippedWeapon.CancelStrongerAttack();
		private void PerformStrongerAttack(InputAction.CallbackContext context) => equippedWeapon.PerformStrongerAttack();

		private void StartAlternativeAttack(InputAction.CallbackContext context) => equippedWeapon.StartAlternativeAttack();
		private void PerformAlternativeAttack(InputAction.CallbackContext context) => equippedWeapon.PerformAlternativeAttack();
		private void CancelAlternativeAttack(InputAction.CallbackContext context) => equippedWeapon.CancelAlternativeAttack();
	}
}
