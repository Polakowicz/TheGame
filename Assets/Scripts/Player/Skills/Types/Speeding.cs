using System.Collections;
using UnityEngine;

public class Speeding : Skill
{
	PlayerManager playerEventSystem;

	[SerializeField]
	float speedIncrease;
	[SerializeField]
	float skillTime;

	void Start()
	{
		playerEventSystem = GetComponentInParent<PlayerManager>();
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
