using Scripts.Bullets;
using Scripts.Interfaces;
using Scripts.Tools;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
	[Serializable]
	public class BlasterPlayerWeapon : PlayerWeapon
	{
		// Other need components
		private PlayerManager playerManagerComponent;
		private PlayerMovement playerMovementComponent;
		private LineRenderer beamRendererComponent;
		
		// Position (offset) in whitch bullets will be instantiate
		[SerializeField] private Transform gunBarrel;

		// Pools of bullets of specific type
		[Header("Bullet pools")]
		[SerializeField] private ObjectPool basicBulletPool;
		[SerializeField] private ObjectPool explosiveBulletPool;
		[SerializeField] private ObjectPool piercingBulletPool;
		private ObjectPool currentlySelectedPool;
		[Space(20)]
	
		[Header("Auto Fire Delay")]
		// Delay between next bullet instantiainstantiate
		[SerializeField] private float autoFireStartDelay = 0.4f;
		[SerializeField] private float autoFireMinimalDelay = 0.1f;
		private float autoFireCurrentDelay;

		// Multiplier how much delay decrease every shot
		[SerializeField] private float autoFireDalayDecreaseMultiplier = 0.1f;

		// Auto fire coroutine to stop it when needed
		private Coroutine autoFireCoroutine;



		[Header("Auto Fire Dispersion")]
		// Random dispersion for auto fire shots in degrees
		[SerializeField] private float autoFireStartDispersion = 0;
		[SerializeField] private float autoFireMaxDispersion = 40;
		[SerializeField] private float autoFireDispersionIncreaseMultiplier = 0.1f;
		private float autoFireCurrentDispersion;
		[Space(20)]
		
		

		[Header("Beam")]
		[SerializeField] private LayerMask beamLayerMask;
		[SerializeField] private float beamMaxRange = 7f;

		// How fast object should move when pulled
		[SerializeField] private float beamPullSpeed = 15f;

		// For how long object should be stuned when pulled to it
		[SerializeField] private float beamPullStunTime = 1f;
		private GameObject beamHit;

		private bool isPlayerPullingToOtherObject = false;


		private void Awake()
		{
			// Get components
			playerManagerComponent = GetComponentInParent<PlayerManager>();
			playerMovementComponent = GetComponentInParent<PlayerMovement>();
			beamRendererComponent = GetComponent<LineRenderer>();

			// Nothing is hit by beam at start
			beamRendererComponent.enabled = false;

			// Set default variables values
			Type = WeaponType.Blaster;
			currentlySelectedPool = basicBulletPool;
		}
		private void Start()
		{
			// Subscribe to events
			playerManagerComponent.PowerUpController.OnPowerUpChanged += ChangePowerUp;
		}
		private void OnDestroy()
		{
			// Unsubscribe events
			playerManagerComponent.PowerUpController.OnPowerUpChanged -= ChangePowerUp;
		}

		private void Update()
		{
			// Draw beam if something is hit by it
			if (beamHit != null) {
				DrawBim();
			}
		}

		// Shoot bullet
		public override void PerformBasicAttack() => ShootBullet();
		// Start autofire
		public override void PerformStrongerAttack() => autoFireCoroutine = StartCoroutine(AutoFire());
		// Stop autofire
		public override void CancelStrongerAttack()
		{
			if (autoFireCoroutine != null) {
				StopCoroutine(autoFireCoroutine);
			}
		}

		private void ShootBullet()
		{
			ShootBullet(gunBarrel.rotation);
		}
		private void ShootBullet(Quaternion rotation)
		{
			// Play shooting sound
			playerManagerComponent.AudioManager.Play("RangedBasicAttack");

			// Get bullet from pool
			var bullet = currentlySelectedPool.GetObject();

			// Set bullet position, rotation and velocity
			bullet.transform.SetPositionAndRotation(transform.position, rotation);
			var bulletRb = bullet.GetComponent<Rigidbody2D>();
			bulletRb.rotation = bullet.transform.rotation.eulerAngles.z;
			bulletRb.velocity = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * bulletRb.rotation),
				Mathf.Cos(Mathf.Deg2Rad * bulletRb.rotation)) * bullet.GetComponent<Bullet>().Speed;

			// If explosive bullet powerup is active, decrease explosives left
			if (currentlySelectedPool == explosiveBulletPool)
			{
				playerManagerComponent.PowerUpController.ShootExplosiveBullet();
			}
		}
		IEnumerator AutoFire()
		{
			// Set current varibles to start values
			autoFireCurrentDelay = autoFireStartDelay;
			autoFireCurrentDispersion = autoFireStartDispersion;

			while (true) {
				ShootBullet(GetRandomisedAccuracy());
				yield return new WaitForSeconds(autoFireCurrentDelay);

				// Calculate new dispersion and delay
				if (autoFireCurrentDispersion < autoFireMaxDispersion) {
					autoFireCurrentDispersion += autoFireDispersionIncreaseMultiplier * Mathf.Abs(autoFireMaxDispersion - autoFireCurrentDispersion);
				}
				if (autoFireCurrentDelay > autoFireMinimalDelay) {
					autoFireCurrentDelay -= autoFireDalayDecreaseMultiplier * Mathf.Abs(autoFireMinimalDelay - autoFireCurrentDelay);
				}
			}
		}

		// Return Quaternion with random Z rotation in dispersion range
		Quaternion GetRandomisedAccuracy()
		{
			var rotation = gunBarrel.rotation.eulerAngles.z;
			float newRotation = UnityEngine.Random.Range(rotation - autoFireCurrentDispersion, rotation + autoFireCurrentDispersion);
			return Quaternion.Euler(0, 0, newRotation);
		}
		
		// Shoot beam
		public override void StartAlternativeAttack()
		{
			// Cant shot beam if player is not in walk state
			if (playerManagerComponent.State != PlayerManager.PlayerState.Walk) return;

			// Try to hit object with raycast
			var hit = Physics2D.Raycast(gunBarrel.position, gunBarrel.up, beamMaxRange, beamLayerMask);
			if (hit.collider == null) return;

			beamHit = hit.collider.gameObject;

			// Set up line renderer
			beamRendererComponent.positionCount = 2;
			DrawBim();
			beamRendererComponent.enabled = true;
		}
		// Disable beam
		public override void CancelAlternativeAttack()
		{
			// Cant disable beam during pulling to other object
		if (isPlayerPullingToOtherObject) return;

			beamHit = null;

			// Reset line renderer settings
			beamRendererComponent.enabled = false;
			beamRendererComponent.positionCount = 0;
		}

		private void DrawBim()
		{
			beamRendererComponent.SetPosition(0, transform.position);
			beamRendererComponent.SetPosition(1, beamHit.transform.position);
		}

		public override void PerformBeamPullAction(float input)
		{
			// Cant pull if beam is not attached to other object
			if (beamHit == null) return;

			if (input > 0) {
				PullPlayerTowardsTarget();
			} else if (input < 0) {
				PullTargetTowardsPlayer();
			}
		}
		private void PullPlayerTowardsTarget()
		{
			// Cant pull towars tager if already pulling
			if(isPlayerPullingToOtherObject) return ;

			isPlayerPullingToOtherObject = true;

			// Caulate pull parameters
			Vector2 direction = beamHit.transform.position - transform.position;
			var time = direction.magnitude / beamPullSpeed;

			// Perform dash and desible beam after
			playerMovementComponent.MoveInDirection(direction, beamPullSpeed, time, () => {
				if (beamHit.TryGetComponent<IHit>(out var enemy))
					enemy.Stun(gameObject, beamPullStunTime);

				isPlayerPullingToOtherObject = false;
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
				currentlySelectedPool = active ? explosiveBulletPool : basicBulletPool;
			} else if (type == PowerUp.PowerType.ShotPiercing) {
				currentlySelectedPool = active ? piercingBulletPool : basicBulletPool;
			}
		}
	}
}
