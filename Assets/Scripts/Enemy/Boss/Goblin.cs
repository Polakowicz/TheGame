using System;
using System.Collections;
using UnityEngine;

public class Goblin : MonoBehaviour
{
	private Animator animator;
	private GameObject player;

	[SerializeField] private int rockHitsToDie = 3;
	[SerializeField] private int hp = 100000;
	[SerializeField] private float kickSpeed;
	[SerializeField] private float kickDistance;
	[SerializeField] private int kickDamage;

	private bool alive;

	private void Start()
	{
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		if (!alive) return;

		var distance = Vector2.Distance(transform.position, player.transform.position);
		animator.SetFloat("Distance", distance);
	}

	private void Kick()
	{
		player.GetComponent<PlayerEventSystem>().Kick(kickSpeed, kickDistance, kickDamage);
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