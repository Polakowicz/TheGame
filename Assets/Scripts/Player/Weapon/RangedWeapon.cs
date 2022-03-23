using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RangedWeapon : Weapon
{
	//Components
	[SerializeField] Transform gunTransform;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] MonoBehaviour player;

	//Parameters
	[SerializeField] float startFireRate;
	[SerializeField] float maxFireRate;
	[SerializeField] float differenceFireRat;

	//Internal variables
	Coroutine autoFireCoroutine;
	float fireRate;

	public override void PerformBasicAttack()
	{
		UnityEngine.Object.Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
	}

	public override void PerformStrongerAttack() 
	{
		autoFireCoroutine = player.StartCoroutine(AutoFire());
	}
	public override void CancelStrongerAttack()
	{
		player.StopCoroutine(autoFireCoroutine);
	}

	public override void PerformAlternativeAttack()
	{
		Debug.Log("Range aalternative attack");
	}

	public IEnumerator AutoFire()
	{
		fireRate = startFireRate;
		while (true) {
			Debug.Log("shoot");
			PerformBasicAttack();
			yield return new WaitForSeconds(fireRate);
			if (fireRate > maxFireRate) {
				fireRate -= differenceFireRat;
			}
		}
	}
}
