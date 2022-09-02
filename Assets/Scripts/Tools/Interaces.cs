
using Scripts.Player;
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
			WhackAMole,
			SkillFreeze,
			OTHER
		}

		public void Hit(int damage, HitWeapon weapon);
		public void StunHit(int damage, float stunTime, HitWeapon weapon);
		public void Stun(HitWeapon weapon, float time, float strength = 1);
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
