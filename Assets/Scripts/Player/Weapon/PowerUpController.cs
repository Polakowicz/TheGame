using System;
using System.Collections;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
	public float DoubleBlatedLeftTime;
	public int forceFieldchargesRemaining;
	public float piercingBulletsLeftTime;
	public int explosiveBulletsRemaining;

	public Action<PowerUp.PowerType, bool> OnPowerUpChanged;

	void Update()
	{
		if (DoubleBlatedLeftTime > 0) {
			DoubleBlatedLeftTime -= Time.deltaTime;
			if (DoubleBlatedLeftTime <= 0) {
				OnPowerUpChanged?.Invoke(PowerUp.PowerType.DoubleBlade, false);
			}
		}

		if (piercingBulletsLeftTime > 0) {
			piercingBulletsLeftTime -= Time.deltaTime;
			if (piercingBulletsLeftTime <= 0) {
				OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotPiercing, false);
			}
		}
	}

	public bool HitForceField()
	{
		if (forceFieldchargesRemaining > 0) {
			forceFieldchargesRemaining--;
			if (forceFieldchargesRemaining <= 0) {
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
				if(DoubleBlatedLeftTime <= 0) {
					DoubleBlatedLeftTime = powerUp.Duration;
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.DoubleBlade, true);
				}
				DoubleBlatedLeftTime = powerUp.Duration;
				break;
			case PowerUp.PowerType.ShotPiercing:
				if (piercingBulletsLeftTime <= 0) {
					piercingBulletsLeftTime = powerUp.Duration;
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotPiercing, true);
				}
				piercingBulletsLeftTime = powerUp.Duration;
				break;
			case PowerUp.PowerType.ShotExplosion:
				if (explosiveBulletsRemaining <= 0) {
					explosiveBulletsRemaining = powerUp.Duration;
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.ShotExplosion, true);
				}
				explosiveBulletsRemaining = powerUp.Duration;
				break;
			case PowerUp.PowerType.ForceField:
				if (forceFieldchargesRemaining <= 0) {
					forceFieldchargesRemaining = powerUp.Duration;
					OnPowerUpChanged?.Invoke(PowerUp.PowerType.ForceField, true);
				}
				forceFieldchargesRemaining = powerUp.Duration;
				break;
		}
	}
	
}