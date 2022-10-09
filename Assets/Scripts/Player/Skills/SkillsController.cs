using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class SkillsController : MonoBehaviour
	{
		// Components
		private PlayerInput inputComponent;

		// Select skill
		private InputAction selectFreezTimeAction;
		private InputAction selectWarpAction;
		private InputAction selectWhackAMoleAction;
		private InputAction selectSpeedingAction;

		// Use skill
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

		private void Awake()
		{
			// Get input component
			inputComponent = GetComponentInParent<PlayerInput>();

			// Create skills dictionary
			skills.Add(SkillType.FreezeTime, GetComponentInChildren<FreezeSkill>());
			skills.Add(SkillType.Warp, GetComponentInChildren<WarpSkill>());
			skills.Add(SkillType.WhackAMole, GetComponentInChildren<WhackAMoleSkill>());
			skills.Add(SkillType.Speeding, GetComponentInChildren<SpeedingSkill>());

			// Create action inputs
			selectFreezTimeAction = inputComponent.actions["Select Freeze Time Skill"];
			selectWarpAction = inputComponent.actions["Select Warp Skill"];
			selectWhackAMoleAction = inputComponent.actions["Select Whack-a-mole Skill"];
			selectSpeedingAction = inputComponent.actions["Select Speeding Skill"];
			useSkillAction = inputComponent.actions["Use Skill"];
		}
		private void Start()
		{
			// Subscribe to inputs
			selectFreezTimeAction.performed += SelectFreezTimeSkill;
			selectWarpAction.performed += SelectWarpSkill;
			selectWhackAMoleAction.performed += SelectWhackAMoleSkill;
			selectSpeedingAction.performed += SelectSpeedingSkill;
			useSkillAction.started += StartUsingSkill;
			useSkillAction.performed += UseSkill;
			useSkillAction.canceled += StopUsingSkill;
		}
		private void OnDestroy()
		{
			// Unsubscrive to inputs
			selectFreezTimeAction.performed -= SelectFreezTimeSkill;
			selectWarpAction.performed -= SelectWarpSkill;
			selectWhackAMoleAction.performed -= SelectWhackAMoleSkill;
			selectSpeedingAction.performed += SelectSpeedingSkill;
			useSkillAction.started -= StartUsingSkill;
			useSkillAction.performed -= UseSkill;
			useSkillAction.canceled -= StopUsingSkill;
		}

		// Select skill
		private void SelectFreezTimeSkill(InputAction.CallbackContext context) => SelectSkill(SkillType.FreezeTime);
		private void SelectWarpSkill(InputAction.CallbackContext context) => SelectSkill(SkillType.Warp);
		private void SelectWhackAMoleSkill(InputAction.CallbackContext context) => SelectSkill(SkillType.WhackAMole);
		private void SelectSpeedingSkill(InputAction.CallbackContext context) => SelectSkill(SkillType.Speeding);
		private void SelectSkill(SkillType skill)
		{
			equipedSkill = skill;
			Debug.Log($"Equiped skill: {equipedSkill}");
		}

		// Use skill
		private void StartUsingSkill(InputAction.CallbackContext context) => skills[equipedSkill].StartUsingSkill();
		private void UseSkill(InputAction.CallbackContext context) => skills[equipedSkill].UseSkill();
		private void StopUsingSkill(InputAction.CallbackContext context) => skills[equipedSkill].StopUsingSkill();
	}
}
