﻿using System;
using System.Collections;
using UnityEngine;

namespace script.Player.Weapon
{
	[Serializable]
	public class RangedWeapon : Weapon
	{
		private PlayerManager player;

		private Coroutine autoFireCoroutine;

		//Components
		[SerializeField] Transform gunTransform;
		[SerializeField] GameObject defaultBullet;
		[SerializeField] GameObject explosiveBullet;
		[SerializeField] GameObject piercingBullet;
		LineRenderer lineRenderer;

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
		[SerializeField] float beamStunTime = 1;

		//Internal variables
		GameObject beamHit;
		
		GameObject bullet;
		float fireRate;
		float dispersion;

		void Start()
		{
			player = GetComponentInParent<PlayerManager>();

			Type = WeaponType.Blaster;

			lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.enabled = false;
			bullet = defaultBullet;

			player.powerUpController.OnPowerUpChanged += ChangePowerUp;
		}

		void Update()
		{
			if (beamHit != null) {
				lineRenderer.SetPosition(0, transform.position);
				lineRenderer.SetPosition(1, beamHit.transform.position);
			}
		}

		//Shooting
		private void ShootBullet()
		{
			ShootBullet(gunTransform.rotation);
		}
		private void ShootBullet(Quaternion rotation)
		{
			player.AudioManager.Play("RangedBasicAttack");
			Instantiate(bullet, gunTransform.position, rotation);

			if (bullet == explosiveBullet) {
				player.powerUpController.ShootExplosiveBullet();
			}
		}

		public override void PerformBasicAttack()
		{
			ShootBullet();
		}
		public override void PerformStrongerAttack()
		{
			autoFireCoroutine = StartCoroutine(AutoFire());
		}
		public override void CancelStrongerAttack()
		{
			if (autoFireCoroutine != null) {
				StopCoroutine(autoFireCoroutine);
			}
		}
		IEnumerator AutoFire()
		{
			fireRate = startFireRate;
			dispersion = startDispersion;
			while (true) {
				ShootBullet(GetRandomisedAccuracy());
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
			return Quaternion.Euler(0, 0, newRotation);
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
				lineRenderer.SetPosition(0, transform.position);
				lineRenderer.SetPosition(1, beamHit.transform.position);
				lineRenderer.enabled = true;
			}
		}
		public override void CancelAlternativeAttack()
		{
			beamHit = null;
			lineRenderer.enabled = false;
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
			player.StartBeamPullTowardsEnemy(beamHit, beamPullSpeed, beamStunTime);
			CancelAlternativeAttack();
		}
		private void PullTargetTowardsPlayer()
		{
			beamHit.GetComponent<IPullable>().Pull(beamPullSpeed);
		}
	}
}
