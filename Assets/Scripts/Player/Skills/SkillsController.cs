using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillsController : MonoBehaviour
{
	[SerializeField]
	PlayerInput input;

	InputAction selectFreezTimeAction;
	InputAction selectWarpAction;
	InputAction selectWhackAMoleAction;
	InputAction selectSpeeding;
	InputAction useSkillAction;

	public enum SkillType
	{
		FreezeTime,
		Warp,
		WhackAMole,
		Speeding
	}

	public IDictionary<SkillType, Skill> skills = new Dictionary<SkillType, Skill>();

	SkillType equipedSkill = SkillType.FreezeTime;

	void Start()
	{
		CreateSkillsDictionary();
		CreateActionInputs();
		SubscribeToInputs();
	}
	void CreateSkillsDictionary()
	{
		skills.Add(SkillType.FreezeTime, GetComponentInChildren<Freeze>());
		skills.Add(SkillType.Warp, GetComponentInChildren<Warp>());
		skills.Add(SkillType.WhackAMole, GetComponentInChildren<WhackAMole>());
		skills.Add(SkillType.Speeding, GetComponentInChildren<Speeding>());
	}
	void CreateActionInputs()
	{
		selectFreezTimeAction = input.actions["Select Freeze Time Skill"];
		selectWarpAction = input.actions["Select Warp Skill"];
		selectWhackAMoleAction = input.actions["Select Whack-a-mole Skill"];
		selectSpeeding = input.actions["Select Speeding Skill"];
		useSkillAction = input.actions["Use Skill"];
	}
	void SubscribeToInputs()
	{
		selectFreezTimeAction.performed += SelectFreezTimeSkill;
		selectWarpAction.performed += SelectWarpSkill;
		selectWhackAMoleAction.performed += SelectWhackAMoleSkill;
		selectSpeeding.performed += SelectSpeedingSkill;
		useSkillAction.started += StartUsingSkill;
		useSkillAction.performed += UseSkill;
		useSkillAction.canceled += StopUsingSkill;
	}

	void OnDestroy()
	{
		UnsubscribeToInputs();
	}
	void UnsubscribeToInputs()
	{
		selectFreezTimeAction.performed -= SelectFreezTimeSkill;
		selectWarpAction.performed -= SelectWarpSkill;
		selectWhackAMoleAction.performed -= SelectWhackAMoleSkill;
		selectSpeeding.performed += SelectSpeedingSkill;
		useSkillAction.started -= StartUsingSkill;
		useSkillAction.performed -= UseSkill;
		useSkillAction.canceled -= StopUsingSkill;
	}

	void SelectFreezTimeSkill(InputAction.CallbackContext context)
	{
		equipedSkill = SkillType.FreezeTime;
	}
	void SelectWarpSkill(InputAction.CallbackContext context)
	{
		equipedSkill = SkillType.Warp;
	}
	void SelectWhackAMoleSkill(InputAction.CallbackContext context)
	{
		equipedSkill = SkillType.WhackAMole;
	}
	void SelectSpeedingSkill(InputAction.CallbackContext context)
	{
		equipedSkill = SkillType.Speeding;
	}

	void StartUsingSkill(InputAction.CallbackContext context)
	{
		skills[equipedSkill].StartUsingSkill();
	}
	void UseSkill(InputAction.CallbackContext context)
	{
		skills[equipedSkill].UseSkill();
	}
	void StopUsingSkill(InputAction.CallbackContext context)
	{
		skills[equipedSkill].StopUsingSkill();
	}
}
