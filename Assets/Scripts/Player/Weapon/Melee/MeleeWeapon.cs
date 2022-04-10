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
			layerMask = melleWeaponLayerMask,
			useLayerMask = true
		};
	}

	public override void PerformBasicAttack()
	{
		List<Collider2D> hits = new List<Collider2D>();
		meleeRange.OverlapCollider(contactFilter, hits);
		foreach (Collider2D hit in hits) {
			hit.GetComponent<Enemy>().GetHit(damage);
		}
	}
	
	public override void PerformStrongerAttack()
	{
		eventSystem.StartBladeThrust(thrustSpeed, thrustTime, thrustDmg);
	}
	
	public override void PerformAlternativeAttack()
	{
		eventSystem.StartBladeBlock();
	}
	public override void CancelAlternativeAttack()
	{
		eventSystem.EndBladeBlock();
	}
}
