using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RangedWeapon : Weapon
{
	public override void PerformeBasicAttack()
	{
		Debug.Log("Range basic attack");
	}
}
