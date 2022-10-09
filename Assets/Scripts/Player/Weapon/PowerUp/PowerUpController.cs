using System;
using UnityEngine;

namespace Scripts.Player
{
	[Serializable]
	public class PowerUpController : MonoBehaviour
	{
		// Powerup information
		public float DoubleBladeTimeLeft { get; private set; }
		public int ForceFieldChargesRemaining { get; private set; }
		public float PiercingBulletsTimeLeft { get; private set; }
		public int ExplosiveBulletsRemaining { get; private set; }

		// Event invoked when powerup is enabled or disabled
		public Action<PowerUp.PowerType, bool> OnPowerUpChanged;

		private void Update()
		{
			// Decrease Doubl blade time left
			if (DoubleBladeTimeLeft > 0) {
				DoubleBladeTimeLeft -= Time.deltaTime;
				if (DoubleBladeTimeLeft <= 0) {
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.DoubleBlade, false);
				}
			}

			// Decrease piercing bullet time left
			if (PiercingBulletsTimeLeft > 0) {
				PiercingBulletsTimeLeft -= Time.deltaTime;
				if (PiercingBulletsTimeLeft <= 0) {
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotPiercing, false);
				}
			}
		}

		// Return if force field absorb hit
		public bool HitForceField()
		{
			if (ForceFieldChargesRemaining <= 0)
				return false;


			ForceFieldChargesRemaining--;

			// Inform listiners if powerup has eneded
			if (ForceFieldChargesRemaining <= 0) {
				OnPowerUpChanged?.Invoke(PowerUp.PowerType.ForceField, false);
			}
			return true;
		}

		// Return if explosive has been shot
		public bool ShootExplosiveBullet()
		{
			if (ExplosiveBulletsRemaining <= 0)
				return false;

			ExplosiveBulletsRemaining--;

			// Inform listiners if powerup has eneded
			if (ExplosiveBulletsRemaining <= 0) {
				OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotExplosion, false);
			}
			return true;
		}

		public void AddPowerUp(PowerUp powerUp)
		{
			switch (powerUp.Type) {

				case PowerUp.PowerType.DoubleBlade:
					DoubleBladeTimeLeft = powerUp.Duration;
					break;


				case PowerUp.PowerType.ShotPiercing:
					// If explosive bullets powerup is active, disable it first
					if(ExplosiveBulletsRemaining > 0) {
						ExplosiveBulletsRemaining = 0;
						OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotExplosion, false);
					}

					PiercingBulletsTimeLeft = powerUp.Duration;
					break;


				case PowerUp.PowerType.ShotExplosion:
					// If piercing bullets powerup is active, disable it first
					if (PiercingBulletsTimeLeft > 0) {
						PiercingBulletsTimeLeft = 0;
						OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotPiercing, false);
					}

					ExplosiveBulletsRemaining = powerUp.Duration;
					break;


				case PowerUp.PowerType.ForceField:
					ForceFieldChargesRemaining = powerUp.Duration;
					break;
			}

			// Inform listiners about powerup activation
			OnPowerUpChanged?.Invoke(powerUp.Type, true);
		}
	}
}