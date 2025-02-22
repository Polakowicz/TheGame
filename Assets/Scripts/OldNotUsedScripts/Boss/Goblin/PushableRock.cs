﻿using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

public class PushableRock : MonoBehaviour, IHit
{
	private Rigidbody2D rb;
	private Collider2D col;

	private float pushSpeed = 10;
	private float fallSpeed = 10;

	bool pushed;
	bool falling;

	private void Start()
	{
		col = GetComponent<Collider2D>();
		col.enabled = false;
		rb = GetComponent<Rigidbody2D>();
		rb.interpolation = RigidbodyInterpolation2D.None;
		falling = true;
		StartCoroutine(Fall());
	}

	private IEnumerator Fall()
	{
		var target = rb.position.y - GoblinWave.rockYdistance;
		rb.velocity = new Vector2(0, -fallSpeed);
		while (rb.position.y > target) {
			yield return null;
		}

		rb.velocity = Vector2.zero;
		col.enabled = true;
		falling = false;
	}

	public void Push(Vector2 direction)
	{
		Debug.Log("Push");
		if (falling) return;

		pushed = true;
		rb.velocity = direction.normalized * pushSpeed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(falling) return;

		if (!pushed) return;

		if(collision.gameObject.layer == LayerMask.NameToLayer("Boss")) {
			collision.gameObject.GetComponent<IHit>().Hit(gameObject, 1, IHit.HitWeapon.Rock);
		}

		Destroy(gameObject);
	}

    public void Hit(GameObject attacker, int damage, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
    {
		Push(transform.position - attacker.transform.position);
    }

    public void StunHit(GameObject attacker, int damage, float stunTime, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
    {
        Hit(attacker, damage, weapon);
    }

    public void Stun(GameObject attacker, float time, float strength = 1, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
    {
        
    }
}
