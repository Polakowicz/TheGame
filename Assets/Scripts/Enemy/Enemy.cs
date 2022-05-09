using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private struct PrivateData
	{
		public Freeze FreezeScript;
		public float FreezStrength;
	}
	public class SharedData
	{
		private GameObject gameObject;

		public GameObject Player;
		public int HP;
		public float PullSpeed;
		public bool Stunned;
		public bool Pulled;
		public float SpeedMultiplier;

		public float DistanceToPlayer {
			get {
				return Vector2.Distance(gameObject.transform.position, Player.transform.position);
			}
		}
		public Vector2 DirectionToPlayer {
			get {
				return Player.transform.position - gameObject.transform.position;
			}
		}

		public SharedData(GameObject thisGameObject, int maxHP)
		{
			gameObject = thisGameObject;
			HP = maxHP;
			Player = GameObject.FindGameObjectWithTag("Player");
			SpeedMultiplier = 1;
		}
	}

	private PrivateData privateData;
	public SharedData Data { get; private set; }

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
		Data = new SharedData(gameObject, MaxHP);
		privateData = new PrivateData();
	}
	private void Start()
	{
		Data.Player = GameObject.FindGameObjectWithTag("Player");
		GameEventSystem.Instance.OnPlayerDied += NullPlayerReference;
	}
	private void OnDestroy()
	{
		GameEventSystem.Instance.OnPlayerDied -= NullPlayerReference;
	}

	public void Damage(int dmg)
	{
		Data.HP = Mathf.Clamp(Data.HP - dmg, 0, int.MaxValue);

		if (Data.HP <= 0) {
			OnDied?.Invoke();
			Destroy(gameObject);//Temporary
		} else {
			OnDamaged?.Invoke(dmg);
		}
	}
	public void Stun(float time)
	{
		Data.Stunned = true;
		OnStuned?.Invoke();
		StartCoroutine(WaitForStunToEnd(time));
	}
	public void Pull(float speed)
	{
		if (!Pullable) {
			return;
		}
		Data.PullSpeed = speed;
		Data.Pulled = true;
		OnPulled?.Invoke();
	}
	public void EndPull()
	{
		Data.Pulled = false;
		OnPullEnded?.Invoke();
	}
	public void Overthrow(int damage, float stunTime)
	{
		Damage(damage);
		Stun(stunTime);
	}//Wack-a-mole Skill
	public void Freez(Freeze freez, float strength)
	{
		privateData.FreezeScript = freez;
		privateData.FreezeScript.UnfreezEnemy += Unfreez;
		privateData.FreezStrength = strength;
		Data.SpeedMultiplier -= privateData.FreezStrength;
	}
	public void Unfreez()
	{
		privateData.FreezeScript.UnfreezEnemy -= Unfreez;
		privateData.FreezeScript = null;
		Data.SpeedMultiplier += privateData.FreezStrength;
	}

	private void NullPlayerReference()
	{
		Data.Player = null;
	}//When Player dies
	private IEnumerator WaitForStunToEnd(float time)
	{
		yield return new WaitForSeconds(time);
		Data.Stunned = false;
		OnStunEnded?.Invoke();
	}
	

	
	
	
}