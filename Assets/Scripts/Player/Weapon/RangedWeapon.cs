using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RangedWeapon : Weapon
{
	//Components
	[SerializeField] Transform gunTransform;
	[SerializeField] GameObject bulletPrefab;

	public override void PerformAlternativeAttack()
	{
		throw new NotImplementedException();
	}

	public override void PerformBasicAttack()
	{
		UnityEngine.Object.Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
	}

	public override void PerformStrongerAttack()
	{
		throw new NotImplementedException();
	}
}
