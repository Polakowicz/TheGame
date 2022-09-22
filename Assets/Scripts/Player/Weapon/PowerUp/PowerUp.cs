using UnityEngine;

namespace Scripts.Player
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
			// Only player can trigger and only by collider, not triggers
			if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;
			if (collision.isTrigger) return;

			collision.gameObject.GetComponent<PlayerManager>().PowerUpController.AddPowerUp(this);
			Destroy(gameObject);
		}
	}
}
