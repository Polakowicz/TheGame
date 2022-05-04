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
		OnPullEnded?.Invoke();	
	}

	//Freez
	Freez freez;
	public void Freez(Freez freez)
	{
		this.freez = freez;
		this.freez.UnfreezEnemy += Unfreez;
		Debug.Log($"{gameObject} freez");
	}
	public void Unfreez()
	{
		freez.UnfreezEnemy -= Unfreez;
		freez = null;
		Debug.Log($"{gameObject} unfreez");
	}

	//Whack-a-mole
	public void Overthrow()
	{
		Debug.Log($"{gameObject} overthrow");
	}
}