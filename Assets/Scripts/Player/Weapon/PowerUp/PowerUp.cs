using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player.Weapon
{
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

		[SerializeField] private PowerType _type;
		public PowerType Type { get => _type; }

		[SerializeField] private int _duration;
		public int Duration { get => _duration; }

		void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;
			if (collision.isTrigger) return;

			collision.gameObject.GetComponent<PlayerManager>().powerUpController.AddPowerUp(this);
			Destroy(gameObject);
		}
	}
}
