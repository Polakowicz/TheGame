using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventSystem : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;

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
	}

    public void EndBladeBlock()
	{
        OnBladeBlockEnded?.Invoke();
	}

    //Blaaster beam
    public event Action<GameObject, float> OnBeamPullTowardsEnemyStarted;
    public event Action OnBeamPullTowardsEnemyEnded;
    
    public void StartBeamPullTowardsEnemy(GameObject enemy, float speed)
	{
        Debug.Log(enemy);
        playerData.enemyToPulled = enemy;
        OnBeamPullTowardsEnemyStarted?.Invoke(enemy, speed);
	}

    public void EndBeamPullTowardsEnemy()
	{
        playerData.enemyToPulled.GetComponent<EnemyEventSystem>().Stun();
        playerData.enemyToPulled = null;
        OnBeamPullTowardsEnemyEnded?.Invoke();
	}
}
