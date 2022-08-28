using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Interfaces
{ 
    public interface IHit
    {
		public enum HitWeapon
		{
			Sword,
			Bullet,
			Rock,
			OTHER
		}

		public void Hit(int damage, HitWeapon weapon);
	}
	public interface IInteract
	{
		public void Interact();
	}
	public interface IRiposte
	{
		public void Riposte();
	}
	public interface IKick
	{
		public void Kick(Vector2 direction, int damage);
	}
}
