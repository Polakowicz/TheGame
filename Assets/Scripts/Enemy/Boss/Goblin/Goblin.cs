using Interfaces;
using System;
using System.Collections;
using UnityEngine;

public class Goblin : MonoBehaviour, IHit
{
	private readonly int MaxHP = 10000;
	private readonly int RockHitsResilience = 3;
	private readonly float KickDistance = 2.5f;
	private readonly float KickForce = 10f;

	private int RockDamage => MaxHP / RockHitsResilience + 1;

	private Animator animator;
	public GameObject Player { get; private set; }


	[SerializeField] GameObject wave;

	

	private float kickSpeed;
	private float kickDistance;
	private int kickDamage;

	
	private bool alive = true;
	private bool active = true;
	private int hp;

	private bool deleay;

	private void Start()
	{
		animator = GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag("Player");

		hp = MaxHP;
	}

	private void Update()
	{
		if (!alive) return;
		if (!active) return;

		var distance = Vector2.Distance(transform.position, Player.transform.position);
		if(distance <= KickDistance) {
			animator.SetTrigger("Kick");
		}



		//animator.SetFloat("Distance", distance);

		//if (deleay) return;

		//if(distance < 3) {
		//	Kick();
		//	StartCoroutine(Delay());
		//} else if (distance < 10) {
		//	CreateWave();
		//	StartCoroutine(Delay());
		//}
	}
	private void CreateWave()
	{
		Instantiate(wave, transform.position, Quaternion.identity);
	}
	IEnumerator Delay()
	{
		deleay = true;
		yield return new WaitForSeconds(2);
		deleay = false;
	}



	private void Kick()
	{
		var direction = Player.transform.position - transform.position;
		Debug.Log(direction);
		Player.GetComponent<IKick>()?.Kick(direction * KickForce);
		//Player.GetComponent<PlayerEventSystem>().Kick(direction, kickSpeed, kickDistance, kickDamage);
	}
	private void Die()
	{
		alive = false;
		animator.SetTrigger("Die");
	}
	private void Destroy()
	{
		Destroy(gameObject);
	}
	public void Hit(int damage, IHit.HitWeapon weapon)
	{
		switch (weapon) {
			case IHit.HitWeapon.Bullet:
				hp--;
				break;
			case IHit.HitWeapon.Rock:
				hp -= RockDamage;
				break;
		}

		hp = Mathf.Clamp(hp, 0, MaxHP);
		if (hp == 0) {
			Die();
		}
		//Invoke Hud change;
	}
}