using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    [SerializeField] public PowerUpController powerUpController;

    public event Action OnGetDamaged;
    public event Action OnHPGained;
    public event Action OnDied;

	void Start()
	{
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
    public Action OnBladeAttack;
    public Action OnGunFire;
    public Action OnGunChanged;
    public Action OnDodge;

    public void ChangedWeapon(PlayerData.Weapon weapon)
	{
        playerData.weapon = weapon;
        OnGunChanged?.Invoke();
	}

    //PowerUp
    public void AddPowerUp(PowerUp powerUp)
	{
        powerUpController.AddPowerUp(powerUp);
	}

    //Blade Thrust
    public event Action<PlayerData, float, float, int> OnBladeThrustStarted;
    public event Action OnBladeThrustEnded;

    public void StartBladeThrust(float speed, float time, int dmg)
	{
        OnBladeThrustStarted?.Invoke(playerData, speed, time, dmg);
	}

    public void EndBladeThrust()
	{
        OnBladeThrustEnded?.Invoke();
	}

    //Blade block
    public event Action OnBladeBlockStarted;
    public event Action OnBladeBlockEnded;

    public void StartBladeBlock()
	{

        OnBladeBlockStarted?.Invoke();
        StartCoroutine(BlockDelay());
	}

    IEnumerator BlockDelay()
	{
        playerData.blocking = true;
        yield return new WaitForSeconds(1);
        playerData.blocking = false;
    }

    public void EndBladeBlock()
	{
        OnBladeBlockEnded?.Invoke();
	}

    //Blaaster beam
    public event Action<GameObject, float, float> OnBeamPullTowardsEnemyStarted;
    public event Action OnBeamPullTowardsEnemyEnded;
    
    public void StartBeamPullTowardsEnemy(GameObject enemy, float speed, float stunTime)
	{
        Debug.Log(enemy);
        playerData.enemyToPulled = enemy;
        OnBeamPullTowardsEnemyStarted?.Invoke(enemy, speed, stunTime);
	}

    public void EndBeamPullTowardsEnemy(float stunTime)
	{
        var enemy = playerData.enemyToPulled.GetComponent<Enemy>();
        if(enemy != null)
            enemy.Stun(stunTime);
        playerData.enemyToPulled = null;
        OnBeamPullTowardsEnemyEnded?.Invoke();
	}

    public event Action<Vector2, float, float> OnKicked;
    public void Kick(Vector2 direction, float speed, float distance, int damage)
	{
        Debug.Log("Kick");
        OnKicked?.Invoke(direction, speed, distance);
        GiveDamage(damage);
	}
}
