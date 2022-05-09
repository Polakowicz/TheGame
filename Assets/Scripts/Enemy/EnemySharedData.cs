using System.Collections;
using UnityEngine;

public class EnemySharedData
{
	private GameObject thisGameObject;

	public int HP;
	public GameObject Player;
	public float DistanceToPlayer {
		get {
			return Vector2.Distance(ThisObject.transform.position, Player.transform.position);
		}
	}
	public Vector2 DirectionToPlayer {
		get {
			return Player.transform.position - ThisObject.transform.position;
		}
	}
	public float PullSpeed;
	public bool Stunned;
	public bool Pulled;
	public float SpeedMultiplier;

	public EnemySharedData(GameObject thisGameObject, int hp)
	{
		this.thisGameObject = thisGameObject;
		HP = hp;
		SpeedMultiplier = 1;
	}
}