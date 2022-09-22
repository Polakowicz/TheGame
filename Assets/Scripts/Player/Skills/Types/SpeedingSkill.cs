﻿using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
	public class SpeedingSkill : Skill
	{
		private PlayerMovement movementComponent;

		[SerializeField] private float speedMultiplier;
		[SerializeField] private float skillTime;

		private void Awake()
		{
			movementComponent = GetComponentInParent<PlayerMovement>();
		}

		public override void UseSkill()
		{
			movementComponent.SpeedMultiplier = speedMultiplier;
			StartCoroutine(SkillDuration(skillTime));
		}

		private IEnumerator SkillDuration(float time)
		{
			yield return new WaitForSeconds(time);
			movementComponent.SpeedMultiplier = 1;
		}
	}
}
