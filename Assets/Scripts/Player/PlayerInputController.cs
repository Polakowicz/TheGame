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
    InputAction performBasicAttackActino;

    //Parameters
    [SerializeField] MeleeWeapon meleeWeapon;
    [SerializeField] RangedWeapon rangedWeapon;

    //Internal Variables
    Weapon equippedWeapon;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        switchWeaponAction = playerInput.actions["Switch weapon"];
        performBasicAttackActino = playerInput.actions["Basic attack"];
    }
}
