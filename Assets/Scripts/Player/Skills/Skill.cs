using UnityEngine;

namespace Scripts.Player
{
	public abstract class Skill : MonoBehaviour
	{
		public virtual void StartUsingSkill() { }
		public virtual void UseSkill() { }
		public virtual void StopUsingSkill() { }
	}
}
