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
		return;
	}
	public override void CancelStrongerAttack()
	{
		Debug.Log("Range stronger attack end");
	}
	public override void PerformStrongerAttack() 
	{
		Debug.Log("Range stronger attack start");
	}

	public override void PerformAlternativeAttack()
	{
		Debug.Log("Range aalternative attack");
	}
}
