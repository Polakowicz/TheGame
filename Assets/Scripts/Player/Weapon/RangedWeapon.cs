﻿using System;
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
	[SerializeField] float fireRatePercentageIncrease;

	[SerializeField] float startDispersion;
	[SerializeField] float maxDispersion;
	[SerializeField] float dispersinPercentageIncrease;

	//Internal variables
	Coroutine autoFireCoroutine;
	float fireRate;
	float dispersion;

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
		if(autoFireCoroutine != null) {
			player.StopCoroutine(autoFireCoroutine);
		}
	}

	public override void PerformAlternativeAttack()
	{
		Debug.Log("Range aalternative attack");
	}

	IEnumerator AutoFire()
	{
		fireRate = startFireRate;
		dispersion = startDispersion;
		while (true) {
			UnityEngine.Object.Instantiate(bulletPrefab, gunTransform.position, GetRandomisedAccuracy());
			yield return new WaitForSeconds(fireRate);
			if (dispersion < maxDispersion) {
				dispersion += dispersinPercentageIncrease * Mathf.Abs(maxDispersion - dispersion);
			}
			if (fireRate > maxFireRate) {
				fireRate -= fireRatePercentageIncrease * Mathf.Abs(maxFireRate - fireRate);
			}
		}
	}

	Quaternion GetRandomisedAccuracy()
	{
		var rotation = gunTransform.rotation.eulerAngles.z;
		float newRotation = UnityEngine.Random.Range(rotation - dispersion, rotation + dispersion);
		return Quaternion.Euler(0,0,newRotation);
	}
}
