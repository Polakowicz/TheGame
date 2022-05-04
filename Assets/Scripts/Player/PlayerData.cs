using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerData
{
	public enum Weapon
	{
		Blaster,
		Blade
	}

	public int MaxHP;

	public Weapon weapon;
	public Vector2 aimDirection;
	public Vector2 moveDireciton;
	public int HP;
	public GameObject enemyToPulled;
	public float speedMultiplier = 1;//100%
}
