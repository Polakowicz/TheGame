using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RangedWeapon : Weapon
{
	//Components
	[SerializeField] Transform gunTransform;
	[SerializeField] GameObject bulletPrefab;

	public override void PerformBasicAttack()
	{
		UnityEngine.Object.Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
	}

	public override void StartStrongerAttack()
	{
		Debug.Log("Range stronger attack start");
	}
	public override void CancelStrongerAttack()
	{
		Debug.Log("Range stronger attack start");
	}
	public override void PerformStrongerAttack() 
	{
		//this weapon has start and cancel stronger attack
		return;
	}

	public override void PerformAlternativeAttack()
	{
		Debug.Log("Range aalternative attack");
	}
}
