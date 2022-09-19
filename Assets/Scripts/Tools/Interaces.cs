
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

		public void Hit(GameObject attacker, int damage, HitWeapon weapon = HitWeapon.OTHER);
		public void StunHit(GameObject attacker, int damage, float stunTime, HitWeapon weapon = HitWeapon.OTHER);
		public void Stun(GameObject attacker, float time, float strength = 1, HitWeapon weapon = HitWeapon.OTHER);
	}

	public enum Interaction
	{
		None,
		Checkpoint,
	}
	public interface IInteract
	{
		public Interaction Interact(GameObject sender);
	}

	public interface IRiposte
	{
		public void Riposte(GameObject sender);
	}

	public interface IKick
	{
		public void Kick(Vector2 direction, int damage);
	}
}
