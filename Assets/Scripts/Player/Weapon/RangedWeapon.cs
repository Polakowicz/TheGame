using System;
using System.Collections;
using UnityEngine;

namespace script.Player.Weapon
{
	[Serializable]
	public class RangedWeapon : Weapon
	{
		//Components
		private PlayerManager player;
		

		[SerializeField] private Transform gunBarrel;
		private GameObject bullet;

		//Bullet Types
		[SerializeField] private GameObject defaultBullet;
		[SerializeField] private GameObject explosiveBullet;
		[SerializeField] private GameObject piercingBullet;

		//AutoFire
		[SerializeField] private float autoFireStartRate;
		[SerializeField] private float autoFireMaxRate;
		[SerializeField] private float autoFireRateIncrease;
		[SerializeField] private float autoFireStartDispersion;
		[SerializeField] private float autoFireMaxDispersion;
		[SerializeField] private float autoFireDispersinIncrease;
		private Coroutine autoFireCoroutine;
		private float autoFireRate;
		private float autoFireDispersion;

		//Beam
		[SerializeField] private LayerMask beamLayerMask;
		[SerializeField] private float beamPullSpeed;
		[SerializeField] private float beamDistance = 10f;
		[SerializeField] private float beamPullCooldown;
		[SerializeField] private float beamStunTime = 1;
		private LineRenderer bimRenderer;
		private GameObject beamHit;

		private void Start()
		{
			player = GetComponentInParent<PlayerManager>();
			bimRenderer = GetComponent<LineRenderer>();

			Type = WeaponType.Blaster;
			bullet = defaultBullet;

			bimRenderer.enabled = false;
	
			player.powerUpController.OnPowerUpChanged += ChangePowerUp;
		}

		void Update()
		{
			if (beamHit != null) {
				bimRenderer.SetPosition(0, transform.position);
				bimRenderer.SetPosition(1, beamHit.transform.position);
			}
		}

		//Shooting
		private void ShootBullet()
		{
			ShootBullet(gunBarrel.rotation);
		}
		private void ShootBullet(Quaternion rotation)
		{
			player.AudioManager.Play("RangedBasicAttack");
			Instantiate(bullet, gunBarrel.position, rotation);

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
			autoFireRate = autoFireStartRate;
			autoFireDispersion = autoFireStartDispersion;
			while (true) {
				ShootBullet(GetRandomisedAccuracy());
				yield return new WaitForSeconds(autoFireRate);
				if (autoFireDispersion < autoFireMaxDispersion) {
					autoFireDispersion += autoFireDispersinIncrease * Mathf.Abs(autoFireMaxDispersion - autoFireDispersion);
				}
				if (autoFireRate > autoFireMaxRate) {
					autoFireRate -= autoFireRateIncrease * Mathf.Abs(autoFireMaxRate - autoFireRate);
				}
			}
		}
		Quaternion GetRandomisedAccuracy()
		{
			var rotation = gunBarrel.rotation.eulerAngles.z;
			float newRotation = UnityEngine.Random.Range(rotation - autoFireDispersion, rotation + autoFireDispersion);
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
			var hit = Physics2D.Raycast(gunBarrel.position, gunBarrel.up, beamDistance, beamLayerMask);
			if (hit.collider != null) {
				beamHit = hit.collider.gameObject;
				bimRenderer.SetPosition(0, transform.position);
				bimRenderer.SetPosition(1, beamHit.transform.position);
				bimRenderer.enabled = true;
			}
		}
		public override void CancelAlternativeAttack()
		{
			beamHit = null;
			bimRenderer.enabled = false;
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
