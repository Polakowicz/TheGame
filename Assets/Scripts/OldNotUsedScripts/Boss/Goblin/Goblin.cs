using Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

public class Goblin : MonoBehaviour, IHit
{
	private readonly int MaxHP = 10000;
	private readonly int RockHitsResilience = 3;
	private readonly float KickDistance = 2.5f;
	private readonly float KickForce = 20f;

	private int RockDamage => MaxHP / RockHitsResilience + 1;

	private Animator animator;
	public GameObject Player { get; private set; }


	[SerializeField] GameObject wave;

	
	
	private bool alive = true;
	private bool active = true;
	private int hp;

	private void Start()
	{
		animator = GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag("Player");

		hp = MaxHP;
	}

	private void Update()
	{
		if (!alive) return;

		var distance = Vector2.Distance(transform.position, Player.transform.position);
		animator.SetFloat("Distance", distance);
		if (distance <= KickDistance) {
			active = false;
		}
	}

	public void Hit(GameObject attacker, int damage, IHit.HitWeapon weapon)
	{
		switch (weapon)
		{
			case IHit.HitWeapon.OTHER:
				hp--;
				break;
			case IHit.HitWeapon.Rock:
				hp -= RockDamage;
				break;
		}

		hp = Mathf.Clamp(hp, 0, MaxHP);
		if (hp == 0) {
			Die();
		} else if (active) {
			animator.SetTrigger("HitGround");
		}
	}

	private void Kick()
	{
		var direction = Player.transform.position - transform.position;
		Player.GetComponent<IKick>()?.Kick(direction.normalized * KickForce, 0);
	}
	private void CreateWave()
	{
		Instantiate(wave, transform.position, Quaternion.identity);
	}
	private void Deactive() => active = false;
	private void Active() => active = true;

	
	private void Die()
	{
		alive = false;
		animator.SetTrigger("Die");
	}
	private void Destroy()
	{
		Destroy(gameObject);
	}

	public void StunHit(GameObject attacker, int damage, float stunTime, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
	{
		throw new NotImplementedException();
	}

	public void Stun(GameObject attacker, float time, float strength = 1, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
	{
		throw new NotImplementedException();
	}
}