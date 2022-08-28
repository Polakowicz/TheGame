using Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
	public class Speeding : Skill
	{
		private PlayerManager player;

		[SerializeField] private float speedIncrease;
		[SerializeField]private float skillTime;

		private void Start()
		{
			player = GetComponentInParent<PlayerManager>();
		}

		public override void UseSkill()
		{
			//playerEventSystem.playerData.speedMultiplier += speedIncrease;
			//TODO
			StartCoroutine(SkillDuration(skillTime));
		}

		IEnumerator SkillDuration(float time)
		{
			yield return new WaitForSeconds(time);
			//playerEventSystem.playerData.speedMultiplier -= speedIncrease;
			//TODO
		}
	}
}
