using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class SkillsController : MonoBehaviour
	{
		private PlayerInput input;

		private InputAction selectFreezTimeAction;
		private InputAction selectWarpAction;
		private InputAction selectWhackAMoleAction;
		private InputAction selectSpeeding;
		private InputAction useSkillAction;

		public enum SkillType
		{
			FreezeTime,
			Warp,
			WhackAMole,
			Speeding
		}

		private readonly IDictionary<SkillType, Skill> skills = new Dictionary<SkillType, Skill>();
		private SkillType equipedSkill = SkillType.FreezeTime;

		private void Start()
		{
			input = GetComponentInParent<PlayerInput>();

			CreateSkillsDictionary();
			CreateActionInputs();
			SubscribeToInputs();
		}
		private void CreateSkillsDictionary()
		{
			skills.Add(SkillType.FreezeTime, GetComponentInChildren<Freeze>());
			skills.Add(SkillType.Warp, GetComponentInChildren<Warp>());
			skills.Add(SkillType.WhackAMole, GetComponentInChildren<WhackAMole>());
			skills.Add(SkillType.Speeding, GetComponentInChildren<Speeding>());
		}
		private void CreateActionInputs()
		{
			selectFreezTimeAction = input.actions["Select Freeze Time Skill"];
			selectWarpAction = input.actions["Select Warp Skill"];
			selectWhackAMoleAction = input.actions["Select Whack-a-mole Skill"];
			selectSpeeding = input.actions["Select Speeding Skill"];
			useSkillAction = input.actions["Use Skill"];
		}
		private void SubscribeToInputs()
		{
			selectFreezTimeAction.performed += SelectFreezTimeSkill;
			selectWarpAction.performed += SelectWarpSkill;
			selectWhackAMoleAction.performed += SelectWhackAMoleSkill;
			selectSpeeding.performed += SelectSpeedingSkill;
			useSkillAction.started += StartUsingSkill;
			useSkillAction.performed += UseSkill;
			useSkillAction.canceled += StopUsingSkill;
		}

		private void OnDestroy()
		{
			UnsubscribeToInputs();
		}
		private void UnsubscribeToInputs()
		{
			selectFreezTimeAction.performed -= SelectFreezTimeSkill;
			selectWarpAction.performed -= SelectWarpSkill;
			selectWhackAMoleAction.performed -= SelectWhackAMoleSkill;
			selectSpeeding.performed += SelectSpeedingSkill;
			useSkillAction.started -= StartUsingSkill;
			useSkillAction.performed -= UseSkill;
			useSkillAction.canceled -= StopUsingSkill;
		}

		private void SelectFreezTimeSkill(InputAction.CallbackContext context) => SelectSkill(SkillType.FreezeTime);
		private void SelectWarpSkill(InputAction.CallbackContext context) => SelectSkill(SkillType.Warp);
		private void SelectWhackAMoleSkill(InputAction.CallbackContext context) => SelectSkill(SkillType.WhackAMole);
		private void SelectSpeedingSkill(InputAction.CallbackContext context) => SelectSkill(SkillType.Speeding);	
		private void SelectSkill(SkillType skill)
		{
			equipedSkill = skill;
			Debug.Log($"Euiped skill: {skill}");
		}

		private void StartUsingSkill(InputAction.CallbackContext context) => skills[equipedSkill].StartUsingSkill();
		private void UseSkill(InputAction.CallbackContext context) => skills[equipedSkill].UseSkill();
		private void StopUsingSkill(InputAction.CallbackContext context) => skills[equipedSkill].StopUsingSkill();
	}
}
