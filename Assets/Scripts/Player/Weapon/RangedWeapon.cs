using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RangedWeapon : Weapon
{
	//Components
	[SerializeField] Transform gunTransform;
	[SerializeField] GameObject bulletPrefab;

	public override void PerformeBasicAttack()
	{
		UnityEngine.Object.Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
	}
}
