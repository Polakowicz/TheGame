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

	[SerializeField] float startAccuracy;
	[SerializeField] float endAccuracy;
	[SerializeField] float differenceAccuracy;

	//Internal variables
	Coroutine autoFireCoroutine;
	float fireRate;
	float accuracy;

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

	IEnumerator AutoFire()
	{
		fireRate = startFireRate;
		accuracy = startAccuracy;
		while (true) {
			UnityEngine.Object.Instantiate(bulletPrefab, gunTransform.position, GetRandomisedAccuracy());
			yield return new WaitForSeconds(fireRate);
			if (accuracy < endAccuracy) {
				accuracy += differenceAccuracy;
			}
			if (fireRate > maxFireRate) {
				fireRate -= differenceFireRat;
			}
			Debug.Log(fireRate);
		}
	}

	Quaternion GetRandomisedAccuracy()
	{
		var rotation = gunTransform.rotation.eulerAngles.z;
		float newRotation = UnityEngine.Random.Range(rotation - accuracy, rotation + accuracy);
		return Quaternion.Euler(0,0,newRotation);
	}
}
