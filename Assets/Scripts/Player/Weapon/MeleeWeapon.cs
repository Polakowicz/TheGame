﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MeleeWeapon : Weapon
{
	//Components
	[SerializeField] Collider2D meleeRange;
	[SerializeField] LayerMask melleWeaponLayerMask;

	//Internal variables
	ContactFilter2D contactFilter;

	public MeleeWeapon()
	{
		contactFilter = new ContactFilter2D {
			layerMask = melleWeaponLayerMask
		};
	}

	public override void PerformBasicAttack()
	{
		List<Collider2D> hits = new List<Collider2D>();
		meleeRange.OverlapCollider(contactFilter, hits);
		foreach (Collider2D hit in hits) {
			if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
				hit.GetComponent<Enemy>().OnGetHit(damage);
			}
		}
	}

	public override void StartStrongerAttack()
	{
		//this weapon has perfomr stronger attack
		return;
	}
	public override void CancelStrongerAttack()
	{
		//this weapon has perfomr stronger attack
		return;
	}
	public override void PerformStrongerAttack()
	{
		Debug.Log("Melee stronger attack");
	}

	public override void PerformAlternativeAttack()
	{
		Debug.Log("Melee alternative attack");
	}
}
