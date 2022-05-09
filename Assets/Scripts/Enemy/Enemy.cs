using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private struct Data
	{
		public Freeze FreezeScript;
		public float FreezStrength;
	}

	private Data data;
	public EnemySharedData SharedData { get; private set; }

	[SerializeField] public int MaxHP { get; private set; }
	[SerializeField] public bool Pullable { get; private set; }

	public Action OnMeleeAttackStart;

	public Action<int> OnDamaged;
	public Action OnDied;
	public Action OnStuned;
	public Action OnStunEnded;
	public Action OnPulled;
	public Action OnPullEnded;

	private void Awake()
	{
		SharedData = new EnemySharedData(gameObject, MaxHP);
		data = new Data();
	}
	private void Start()
	{
		SharedData.Player = GameObject.FindGameObjectWithTag("Player");
		GameEventSystem.Instance.OnPlayerDied += NullPlayerReference;
	}
	private void OnDestroy()
	{
		GameEventSystem.Instance.OnPlayerDied -= NullPlayerReference;
	}

	public void Damage(int dmg)
	{
		SharedData.HP = Mathf.Clamp(SharedData.HP - dmg, 0, int.MaxValue);

		if (SharedData.HP <= 0) {
			OnDied?.Invoke();
			Destroy(gameObject);//Temporary
		} else {
			OnDamaged?.Invoke(dmg);
		}
	}
	public void Stun(float time)
	{
		SharedData.Stunned = true;
		OnStuned?.Invoke();
		StartCoroutine(WaitForStunToEnd(time));
	}
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
	public void Overthrow(int damage, float stunTime)
	{
		Damage(damage);
		Stun(stunTime);
	}//Wack-a-mole Skill
	public void Freez(Freeze freez, float strength)
	{
		data.FreezeScript = freez;
		data.FreezeScript.UnfreezEnemy += Unfreez;
		data.FreezStrength = strength;
		SharedData.SpeedMultiplier -= data.FreezStrength;
	}
	public void Unfreez()
	{
		data.FreezeScript.UnfreezEnemy -= Unfreez;
		data.FreezeScript = null;
		SharedData.SpeedMultiplier += data.FreezStrength;
	}

	private void NullPlayerReference()
	{
		SharedData.Player = null;
	}	//When Player dies
	private IEnumerator WaitForStunToEnd(float time)
	{
		yield return new WaitForSeconds(time);
		SharedData.Stunned = false;
		OnStunEnded?.Invoke();
	}
	

	
	
	
}