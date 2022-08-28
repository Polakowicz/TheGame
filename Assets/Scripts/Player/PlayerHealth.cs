using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
	public class PlayerHealth : MonoBehaviour, IHit
	{
		private PlayerManager player;
		public int HP { get; private set; }

		private void Start()
		{
			player = GetComponentInParent<PlayerManager>();
		}

		public void Hit(int damage, IHit.HitWeapon weapon)
		{
			Debug.LogError("Player IHit not implemented");
		}
	}
}