using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RangedWeapon : Weapon
{
	//Components
	[SerializeField] PlayerEventSystem playerEventSystem;
	[SerializeField] Transform gunTransform;
	[SerializeField] GameObject defaultBullet;
	[SerializeField] GameObject explosiveBullet;
	[SerializeField] GameObject piercingBullet;
	
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
	[SerializeField] float beamPullCooldown;

	//Internal variables
	GameObject beamHit;
	Coroutine autoFireCoroutine;
	GameObject bullet;
	float fireRate;
	float dispersion;

	void Start()
	{
		bullet = defaultBullet;

		playerEventSystem.powerUpController.OnPowerUpChanged += ChangePowerUp;
	}

	//Shooting
	public override void PerformBasicAttack()
	{
		Instantiate(bullet, gunTransform.position, gunTransform.rotation);

		if (bullet == explosiveBullet) {
			playerEventSystem.powerUpController.ShootExplosiveBullet();
		}

		playerEventSystem.OnGunFire?.Invoke();
	}

	public override void PerformStrongerAttack() 
	{
		autoFireCoroutine = StartCoroutine(AutoFire());
		playerEventSystem.OnGunFire?.Invoke();
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
			Instantiate(defaultBullet, gunTransform.position, GetRandomisedAccuracy());
			playerEventSystem.OnGunFire?.Invoke();
			if (bullet == explosiveBullet) {
				playerEventSystem.powerUpController.ShootExplosiveBullet();
			}
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

	public void ChangePowerUp(PowerUp.PowerType type, bool active)
	{
		if (type == PowerUp.PowerType.ShotExplosion) {
			bullet = active ? explosiveBullet : defaultBullet;
		} else if (type == PowerUp.PowerType.ShotPiercing) {
			bullet = active ? piercingBullet : defaultBullet;
		}
	}

	//Beam
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

	//Pull
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
		CancelAlternativeAttack();
	}
	private void PullTargetTowardsPlayer()
	{
		beamHit.GetComponent<Enemy>().Pull(beamPullSpeed);
	}
}
