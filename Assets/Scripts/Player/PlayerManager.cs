using Scripts.Player.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public AudioManager AudioManager { get; private set; }
    public AnimationController AnimationController { get; private set; }

    public enum PlayerState
	{
        Walk,
        Dash
	}
    public PlayerState State;



    public PlayerData playerData;
    public PowerUpController powerUpController;

    public event Action OnGetDamaged;
    public event Action OnHPGained;
    public event Action OnDied;

	void Start()
	{
        AudioManager = FindObjectOfType<AudioManager>();
        AnimationController = GetComponentInChildren<AnimationController>();
        playerData.HP = playerData.MaxHP;
	}

	//Health
	public void GiveDamage(int dmg)
    {
		if (playerData.blocking) {
            return;
		}

		if (powerUpController.HitForceField()) {
            Debug.Log("Blocked by shield");
            return;
		}

        playerData.HP = Mathf.Clamp(playerData.HP - dmg, 0, playerData.MaxHP);
        Debug.Log($"Player HP: {playerData.HP}");
        if(playerData.HP <= 0) {
            GameEventSystem.Instance.OnPlayerDied?.Invoke();
            OnDied?.Invoke();
            Destroy(gameObject);//temporary solution
		} else {
            OnGetDamaged?.Invoke();
		}
    }
    public void GiveHP(int hp)
	{
        playerData.HP += Mathf.Clamp(playerData.HP + hp, 0, playerData.MaxHP);
        OnHPGained?.Invoke();
    }

    //Weapons

    public Action OnGunChanged;
    public Action OnDodge;

    public void ChangedWeapon(Weapon.WeaponType weapon)
	{
        playerData.weapon = weapon;
        OnGunChanged?.Invoke();
	}





    public event Action<Vector2, float, float> OnKicked;
    public void Kick(Vector2 direction, float speed, float distance, int damage)
	{
        Debug.Log("Kick");
        OnKicked?.Invoke(direction, speed, distance);
        GiveDamage(damage);
	}
}
