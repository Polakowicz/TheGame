using System.Collections;
using UnityEngine;

public class EnemySharedData
{
	public int HP;
	public GameObject Enemy;
	public GameObject Player;
	public float DistanceToPlayer {
		get {
			return Vector2.Distance(Enemy.transform.position, Player.transform.position);
		}
	}
	public Vector2 DirectionToPlayer {
		get {
			return Player.transform.position - Enemy.transform.position;
		}
	}
	public float PullSpeed;
	public bool Stunned;
	public bool Pulled;
	public float SpeedMultiplier = 1;
}