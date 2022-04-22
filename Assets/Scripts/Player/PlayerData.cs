using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerData
{
	public int MaxHP;
	public int NewForceFieldCharges;

	public Vector2 aimDirection;	
	public int HP;
	public GameObject enemyToPulled;
	public int forceFieldchargesRemaining;
}
