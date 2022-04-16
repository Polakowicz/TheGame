using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RangedWeapon : Weapon
{
	//Components
	[SerializeField] PlayerEventSystem playerEventSystem;
	[SerializeField] Transform gunTransform;
	[SerializeField] GameObject bulletPrefab;
	
	//Parameters
	[SerializeField] float startFireRate;
	[SerializeField] float maxFireRate;
	[SerializeField] float fireRatePercentageIncrease;

	[SerializeField] float startDispersion;
	[SerializeField] float maxDispersion;
	[SerializeField] float dispersinPercentageIncrease;

	[SerializeField] LayerMask beamHitLayerMask;
	[SerializeField] float beamPullSpeed;
	[SerializeField] float beamDistance = 10f;

	//Internal variables
	GameObject beamHit;
	Coroutine autoFireCoroutine;
	float fireRate;
	float dispersion;

	public override void PerformBasicAttack()
	{
		Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
	}

	public override void PerformStrongerAttack() 
	{
		autoFireCoroutine = StartCoroutine(AutoFire());
	}
	public override void CancelStrongerAttack()
	{
		if(autoFireCoroutine != null) {
			StopCoroutine(autoFireCoroutine);
		}
	}
	IEnumerator AutoFire()
	{
		fireRate = startFireRate;
		dispersion = startDispersion;
		while (true) {
			Instantiate(bulletPrefab, gunTransform.position, GetRandomisedAccuracy());
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

	public override void StartAlternativeAttack()
	{
		var hit = Physics2D.Raycast(gunTransform.position, gunTransform.up, beamDistance, beamHitLayerMask);
		if (hit.collider != null) {
			beamHit = hit.collider.gameObject;
		}
	}
	public override void CancelAlternativeAttack()
	{
		beamHit = null;
	}

	public override void PerformBeamPullAction(float input)
	{
		if (beamHit == null) {
			return;
		}

		if (input > 0) {
			PullPlayerTowardsTarget();
		} else if (input < 0) {
			PullTargetTowardsPlayer();
		}
	}
	private void PullPlayerTowardsTarget()
	{
		playerEventSystem.StartBeamPullTowardsEnemy(beamHit, beamPullSpeed);
	}
	private void PullTargetTowardsPlayer()
	{
		beamHit.GetComponent<EnemyEventSystem>().Pull(transform, beamPullSpeed);
	}
}
