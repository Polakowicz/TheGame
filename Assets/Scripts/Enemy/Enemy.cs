using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int MaxHP;
	public bool Pullable;

	public EnemySharedData SharedData;

	public Action OnMeleeAttackAnimationStarts;

	private void Awake()
	{
		SharedData = new EnemySharedData();
		SharedData.HP = MaxHP;
	}

	void Start()
	{
		
		SharedData.Player = GameObject.FindGameObjectWithTag("Player");
		SharedData.Enemy = gameObject;
		GameEventSystem.Instance.OnPlayerDied += NullPlayerReference;
	}

	private void OnDestroy()
	{
		GameEventSystem.Instance.OnPlayerDied -= NullPlayerReference;
	}

	private void NullPlayerReference()
	{
		SharedData.Player = null;
	}

	//Health
	public Action<int> OnGetHit;
	public Action OnDied;

	public void Hit(int dmg)
	{
		//Debug.Log($"HP {SharedData.HP}, dmg {dmg}");
		SharedData.HP = Mathf.Clamp(SharedData.HP - dmg, 0, int.MaxValue);
		
		if (SharedData.HP <= 0) {
			OnDied?.Invoke();
			Destroy(gameObject);//Temporary
		} else {
			OnGetHit?.Invoke(dmg);
		}
	}

	//Stun
	public Action OnGetStuned;
	public Action OnGetStunedEnded;

	public void Stun(float time)
	{
		SharedData.Stunned = true;
		OnGetStuned?.Invoke();
		StartCoroutine(WaitStunTime(time));
	}
	IEnumerator WaitStunTime(float time)
	{
		yield return new WaitForSeconds(time);
		SharedData.Stunned = false;
		OnGetStunedEnded?.Invoke();
	}

	//Pulling
	public Action OnPulled;
	public Action OnPullEnded;

	public void Pull(float speed)
	{
		if (!Pullable) {
			return;
		}
		SharedData.PullSpeed = speed;
		SharedData.Pulled = true;
		OnPulled?.Invoke();
	}
	public void EndPull()
	{
		SharedData.Pulled = false;
		OnPullEnded?.Invoke();	
	}

	//Freez
	Freeze freez;
	float freezStrength;
	public void Freez(Freeze freez, float strength)
	{
		this.freez = freez;
		this.freez.UnfreezEnemy += Unfreez;
		freezStrength = strength;
		SharedData.SpeedMultiplier -= freezStrength;
	}
	public void Unfreez()
	{
		freez.UnfreezEnemy -= Unfreez;
		freez = null;
		SharedData.SpeedMultiplier += freezStrength;
	}

	//Whack-a-mole
	public void Overthrow(int damage, float stunTime)
	{
		Hit(damage);
		Stun(stunTime);
	}
}