using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerType
	{
		DoubleBlade,
		ForceField,
		ShotPiercing,
		ShotExplosion,
		ShotBounce
	}

	public PowerType powerType;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) {
			return;
		}

		collision.GetComponent<PlayerEventSystem>().GetPowerUp(powerType);
	}
}
