using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerData
{


	public int MaxHP;

	public Weapon.WeaponType weapon;
	public Vector2 aimDirection;
	public Vector2 moveDireciton;
	public int HP;
	public GameObject enemyToPulled;
	public float speedMultiplier = 1;//100%
	public bool blocking;
}
