using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MeleeWeapon : Weapon
{
	//Components
	[SerializeField] PlayerEventSystem eventSystem;
	[SerializeField] Collider2D meleeRange;
	[SerializeField] LayerMask melleWeaponLayerMask;
	[SerializeField] Collider2D blockRange;

	//Internal variables
	ContactFilter2D contactFilter;

	//Parameters
	[SerializeField] float thrustSpeed;
	[SerializeField] float thrustTime;
	[SerializeField] int thrustDmg;

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
				hit.GetComponent<Enemy>().GetHit(damage);
			}
		}
	}
	
	public override void PerformStrongerAttack()
	{
		Debug.Log("MeleeWeapon Stronger attack");
		eventSystem.StartBladeThrust(thrustSpeed, thrustTime, thrustDmg);
	}
	
	public override void PerformAlternativeAttack()
	{
		Debug.Log("Melee alternative attack");
	}
}
