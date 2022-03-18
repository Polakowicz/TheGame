using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class MeleeWeapon : Weapon
{
	public override void PerformeBasicAttack()
	{
		Debug.Log("Melle basic attack");
	}
}
