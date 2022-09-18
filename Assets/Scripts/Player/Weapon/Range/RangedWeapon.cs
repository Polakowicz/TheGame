using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
	[Serializable]
	public class RangedWeapon : Weapon
	{
		private Manager player;
		private Movement movement;
		[SerializeField] private Transform gunBarrel;

		//Bullet Types
		[Header("Bullet Types")]
		[SerializeField] private GameObject defaultBullet;
		[SerializeField] private GameObject explosiveBullet;
		[SerializeField] private GameObject piercingBullet;
		[Space(20)]
		private GameObject bullet;

		//AutoFire
		[Header("Auto Fire")]
		[SerializeField] private float autoFireStartRate = 0.4f;
		[SerializeField] private float autoFireMaxRate = 0.1f;
		[SerializeField] private float autoFireRateIncrease = 0.1f;
		[SerializeField] private float autoFireStartDispersion = 0;
		[SerializeField] private float autoFireMaxDispersion = 40;
		[SerializeField] private float autoFireDispersinIncrease = 0.1f;
		[Space(20)]
		private Coroutine autoFireCoroutine;
		private float autoFireRate;
		private float autoFireDispersion;

		//Beam
		[Header("Beam")]
		[SerializeField] private LayerMask beamLayerMask;
		[SerializeField] private float beamPullSpeed = 15f;
		[SerializeField] private float beamRange = 7f;
		[SerializeField] private float beamStunTime = 1f;
		[SerializeField] private float beamPullStunTime = 1f;
		[Space(20)]
		private LineRenderer bimRenderer;
		private GameObject beamHit;

		private void Start()
		{
			player = GetComponentInParent<Manager>();
			movement = GetComponentInParent<Movement>();
			bimRenderer = GetComponent<LineRenderer>();

			Type = WeaponType.Blaster;
			bullet = defaultBullet;

			bimRenderer.enabled = false;
	
			player.PowerUpController.OnPowerUpChanged += ChangePowerUp;
		}
		private void OnDestroy()
		{
			player.PowerUpController.OnPowerUpChanged -= ChangePowerUp;
		}

		private void Update()
		{
			if (beamHit != null) {
				DrawBim();
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
				player.PowerUpController.ShootExplosiveBullet();
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

		//Beam
		private void DrawBim()
		{
			bimRenderer.SetPosition(0, transform.position);
			bimRenderer.SetPosition(1, beamHit.transform.position);
		}
		public override void StartAlternativeAttack()
		{
			var hit = Physics2D.Raycast(gunBarrel.position, gunBarrel.up, beamRange, beamLayerMask);
			if (hit.collider == null) return;
			
			beamHit = hit.collider.gameObject;
			bimRenderer.positionCount = 2;
			DrawBim();
			bimRenderer.enabled = true;
			
		}
		public override void CancelAlternativeAttack()
		{
			if (player.State == Manager.PlayerState.Dash) return;

			beamHit = null;
			bimRenderer.enabled = false;
			bimRenderer.positionCount = 0;
		}

		//Pull
		public override void PerformBeamPullAction(float input)
		{
			if (beamHit == null) return;

			if (input > 0) {
				PullPlayerTowardsTarget();
			} else if (input < 0) {
				PullTargetTowardsPlayer();
			}
		}
		private void PullPlayerTowardsTarget()
		{
			Vector2 direction = beamHit.transform.position - transform.position;
			var time = direction.magnitude / beamPullSpeed;
			movement.MoveInDirection(direction, beamPullSpeed, time, () => {
				if (beamHit.TryGetComponent<OldEnemy>(out var enemy))
					enemy.Stun(beamPullStunTime);
				CancelAlternativeAttack();
			});
			
		}
		private void PullTargetTowardsPlayer()
		{
			beamHit.GetComponent<IPullable>()?.Pull(beamPullSpeed);
		}


		public void ChangePowerUp(PowerUp.PowerType type, bool active)
		{
			if (type == PowerUp.PowerType.ShotExplosion) {
				bullet = active ? explosiveBullet : defaultBullet;
			} else if (type == PowerUp.PowerType.ShotPiercing) {
				bullet = active ? piercingBullet : defaultBullet;
			}
		}
	}
}
