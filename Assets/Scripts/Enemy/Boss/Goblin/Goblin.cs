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

	private bool deleay;

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

		if (deleay) return;

		if(distance < 3) {
			Kick();
			StartCoroutine(Delay());
		} else if (distance < 10) {
			CreateWave();
			StartCoroutine(Delay());
		}
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
		Player.GetComponent<PlayerEventSystem>().Kick(direction, kickSpeed, kickDistance, kickDamage);
	}
	private void CreateWave()
	{
		Instantiate(wave, transform.position, Quaternion.identity);
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