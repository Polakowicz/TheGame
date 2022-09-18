using System;
using UnityEngine;

namespace Scripts.Player
{
	[Serializable]
	public class PowerUpController : MonoBehaviour
	{
		public float DoubleBladeTimeLeft { get; private set; }
		public int ForceFieldChargesRemaining { get; private set; }
		public float PiercingBulletsTimeLeft { get; private set; }
		public int ExplosiveBulletsRemaining { get; private set; }

		public Action<PowerUp.PowerType, bool> OnPowerUpChanged;

		private void Update()
		{
			if (DoubleBladeTimeLeft > 0) {
				DoubleBladeTimeLeft -= Time.deltaTime;
				if (DoubleBladeTimeLeft <= 0) {
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.DoubleBlade, false);
				}
			}

			if (PiercingBulletsTimeLeft > 0) {
				PiercingBulletsTimeLeft -= Time.deltaTime;
				if (PiercingBulletsTimeLeft <= 0) {
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotPiercing, false);
				}
			}
		}

		public bool HitForceField()
		{
			if (ForceFieldChargesRemaining <= 0)
				return false;

			ForceFieldChargesRemaining--;
			if (ForceFieldChargesRemaining <= 0) {
				OnPowerUpChanged?.Invoke(PowerUp.PowerType.ForceField, false);
			}
			return true;
		}
		public bool ShootExplosiveBullet()
		{
			if (ExplosiveBulletsRemaining <= 0)
				return false;

			ExplosiveBulletsRemaining--;
			if (ExplosiveBulletsRemaining <= 0) {
				OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotExplosion, false);
			}
			return true;
		}

		public void AddPowerUp(PowerUp powerUp)
		{
			Debug.Log($"Added powerUp: {powerUp}");

			switch (powerUp.Type) {
				case PowerUp.PowerType.DoubleBlade:
					DoubleBladeTimeLeft = powerUp.Duration;
					break;
				case PowerUp.PowerType.ShotPiercing:
					if(ExplosiveBulletsRemaining > 0) {
						ExplosiveBulletsRemaining = 0;
						OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotExplosion, false);
					}
					PiercingBulletsTimeLeft = powerUp.Duration;
					break;
				case PowerUp.PowerType.ShotExplosion:
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

			OnPowerUpChanged?.Invoke(powerUp.Type, true);
		}
	}
}