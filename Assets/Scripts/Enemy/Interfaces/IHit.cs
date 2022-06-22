using System.Collections;
using UnityEngine;

namespace Interfaces
{
	public interface IHit 
	{
		public enum HitWeapon
		{
			Sword,
			Bullet,
			Rock
		}

		public void Hit(int damage, HitWeapon weapon);
	}
}