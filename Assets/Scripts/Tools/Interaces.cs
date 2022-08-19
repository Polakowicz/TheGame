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
			Rock
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
}
