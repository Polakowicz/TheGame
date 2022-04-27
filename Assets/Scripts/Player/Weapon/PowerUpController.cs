using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PowerUpController : MonoBehaviour
{
	public float DoubleBladeTimeLeft;
	public int forceFieldChargesRemaining;
	public float piercingBulletsTimeLeft;
	public int explosiveBulletsRemaining;

	public Action<PowerUp.PowerType, bool> OnPowerUpChanged;

	void Update()
	{
		if (DoubleBladeTimeLeft > 0) {
			DoubleBladeTimeLeft -= Time.deltaTime;
			if (DoubleBladeTimeLeft <= 0) {
				OnPowerUpChanged?.Invoke(PowerUp.PowerType.DoubleBlade, false);
			}
		}

		if (piercingBulletsTimeLeft > 0) {
			piercingBulletsTimeLeft -= Time.deltaTime;
			if (piercingBulletsTimeLeft <= 0) {
				OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotPiercing, false);
			}
		}
	}

	public bool HitForceField()
	{
		if (forceFieldChargesRemaining > 0) {
			forceFieldChargesRemaining--;
			if (forceFieldChargesRemaining <= 0) {
				OnPowerUpChanged?.Invoke(PowerUp.PowerType.ForceField, false);
			}
			return true;
		}
		return false;
	}

	public bool ShootExplosiveBullet()
	{
		if (explosiveBulletsRemaining > 0) {
			explosiveBulletsRemaining--;
			if (explosiveBulletsRemaining <= 0) {
				OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotExplosion, false);
			}
			return true;
		}
		return false;
	}

	public void AddPowerUp(PowerUp powerUp)
	{
		switch (powerUp.powerType) {
			case PowerUp.PowerType.DoubleBlade:
				if(DoubleBladeTimeLeft <= 0) {
					DoubleBladeTimeLeft = powerUp.Duration;
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.DoubleBlade, true);
				}
				DoubleBladeTimeLeft = powerUp.Duration;
				break;
			case PowerUp.PowerType.ShotPiercing:
				if (piercingBulletsTimeLeft <= 0) {
					piercingBulletsTimeLeft = powerUp.Duration;
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotPiercing, true);
				}
				piercingBulletsTimeLeft = powerUp.Duration;
				break;
			case PowerUp.PowerType.ShotExplosion:
				if (explosiveBulletsRemaining <= 0) {
					explosiveBulletsRemaining = powerUp.Duration;
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotExplosion, true);
				}
				explosiveBulletsRemaining = powerUp.Duration;
				break;
			case PowerUp.PowerType.ForceField:
				if (forceFieldChargesRemaining <= 0) {
					forceFieldChargesRemaining = powerUp.Duration;
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.ForceField, true);
				}
				forceFieldChargesRemaining = powerUp.Duration;
				break;
		}
	}
	
}