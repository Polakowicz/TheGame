using System.Collections;
using UnityEngine;

public class Speeding : Skill
{
	PlayerEventSystem playerEventSystem;

	[SerializeField]
	float speedIncrease;
	[SerializeField]
	float skillTime;

	void Start()
	{
		playerEventSystem = GetComponentInParent<PlayerEventSystem>();
	}

	public override void UseSkill()
	{
		playerEventSystem.playerData.speedMultiplier += speedIncrease;
		StartCoroutine(SkillDuration(skillTime));
	}

	IEnumerator SkillDuration(float time)
	{
		yield return new WaitForSeconds(time);
		playerEventSystem.playerData.speedMultiplier -= speedIncrease;
	}
}
