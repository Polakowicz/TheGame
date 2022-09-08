
using Scripts.Player;
using UnityEngine;

namespace Scripts.Interfaces
{ 
    public interface IHit
    {
		public enum HitWeapon
		{
			OTHER
		}

		public void Hit(int damage, HitWeapon weapon = HitWeapon.OTHER);
		public void StunHit(int damage, float stunTime, HitWeapon weapon = HitWeapon.OTHER);
		public void Stun(float time, float strength = 1, HitWeapon weapon = HitWeapon.OTHER);
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
