using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
	public class Speeding : Skill
	{
		private PlayerMovement movement;

		[SerializeField] private float speedMultiplier;
		[SerializeField] private float skillTime;

		private void Start()
		{
			movement = GetComponentInParent<PlayerMovement>();
		}

		public override void UseSkill()
		{
			movement.SpeedMultiplier = speedMultiplier;
			StartCoroutine(SkillDuration(skillTime));
		}

		private IEnumerator SkillDuration(float time)
		{
			yield return new WaitForSeconds(time);
			movement.SpeedMultiplier = 1;
		}
	}
}
