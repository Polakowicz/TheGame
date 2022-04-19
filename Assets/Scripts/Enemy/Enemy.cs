using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int MaxHP;
	public bool Pullable;
	public float ChanceToBeStunned;
	public float StunTime;

	public EnemySharedData SharedData;

	void Start()
	{
		SharedData = new EnemySharedData();
		SharedData.Player = GameObject.FindGameObjectWithTag("Player");
		SharedData.Enemy = gameObject;
	}

	//Health
	public Action<int> OnGetHit;
	public Action OnDied;

	public void Hit(int dmg)
	{
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

	public void Stun()
	{
		if (UnityEngine.Random.value > ChanceToBeStunned) {
			return;
		}

		SharedData.Stunned = true;
		OnGetStuned?.Invoke();
		StartCoroutine(WaitStunTime(StunTime));
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
		OnPullEnded.Invoke();	
	}
}