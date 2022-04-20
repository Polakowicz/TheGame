using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MeleeWeapon : Weapon
{
	//Components
	[SerializeField] PlayerEventSystem eventSystem;
	[SerializeField] LayerMask melleWeaponLayerMask;
	[SerializeField] Collider2D meleeRange;
	[SerializeField] Collider2D blockRange;

	//Internal variables
	ContactFilter2D attackContactFilter;

	//Parameters
	[SerializeField] int basicAttackDamage;

	[SerializeField] float thrustSpeed;
	[SerializeField] float thrustTime;
	[SerializeField] int thrustDmg;

	void Start()
	{
		attackContactFilter = new ContactFilter2D {
			layerMask = melleWeaponLayerMask,
			useLayerMask = true,
			useTriggers = true
		};
	}

	public override void PerformBasicAttack()
	{
		List<Collider2D> hits = new List<Collider2D>();
		meleeRange.OverlapCollider(attackContactFilter, hits);
		foreach (Collider2D hit in hits) {
			hit.GetComponent<Enemy>().Hit(basicAttackDamage);
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
