using System;
using System.Collections;
using UnityEngine;

public class Goblin : MonoBehaviour
{
	private Animator animator;
	public GameObject Player { get; private set; }

	[SerializeField] GameObject wave;

	[SerializeField] private int rockHitsToDie = 3;
	[SerializeField] private int hp = 100000;
	[SerializeField] private float kickSpeed;
	[SerializeField] private float kickDistance;
	[SerializeField] private int kickDamage;

	private bool alive;

	private void Start()
	{
		animator = GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag("Player");
		alive = true;
	}

	private void Update()
	{
		if (!alive) return;

		var distance = Vector2.Distance(transform.position, Player.transform.position);
		animator.SetFloat("Distance", distance);
	}

	private void Kick()
	{
		Player.GetComponent<PlayerEventSystem>().Kick(kickSpeed, kickDistance, kickDamage);
	}
	private void CreateWave()
	{
		Instantiate(wave);
	}
	
	public void HitWithRock()
	{
		rockHitsToDie--;
		if(rockHitsToDie <= 0) {
			Die();
		}
	}
	public void HitWithBlaster(int dmg)
	{
		hp = Mathf.Clamp(hp - dmg, 0, int.MaxValue);

		if (hp == 0) {
			Die();
		}
	}
	private void Die()
	{
		alive = false;
		//Trigger die animation;
	}
	private void Destroy()
	{
		Destroy(gameObject);
	}
}