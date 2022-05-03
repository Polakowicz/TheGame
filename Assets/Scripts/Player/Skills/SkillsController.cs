﻿using System;
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
	InputAction useSkillAction;

	public enum SkillType
	{
		FreezTime,
		Warp,
		WhackAMole
	}

	[SerializeField]
	SkillType[] skillTypes;
	[SerializeField]
	Skill[] skills;

	SkillType equipedSkill = SkillType.FreezTime;

	void Start()
	{
		CreateActionInputs();
		SubscribeToInputs();
	}
	void CreateActionInputs()
	{
		selectFreezTimeAction = input.actions["Select Freez Time Skill"];
		selectWarpAction = input.actions["Select Warp Skill"];
		selectWhackAMoleAction = input.actions["Select Whack-a-mole Skill"];
		useSkillAction = input.actions["Use Skill"];
	}
	void SubscribeToInputs()
	{
		selectFreezTimeAction.performed += SelectFreezTimeSkill;
		selectWarpAction.performed += SelectWarpSkill;
		selectWhackAMoleAction.performed += SelectWhackAMoleSkill;
		useSkillAction.started += StartUsingSkill;
		useSkillAction.performed += UseSkill;
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
		useSkillAction.started -= StartUsingSkill;
		useSkillAction.performed -= UseSkill;
	}

	void SelectFreezTimeSkill(InputAction.CallbackContext context)
	{
		equipedSkill = SkillType.FreezTime;
	}
	void SelectWarpSkill(InputAction.CallbackContext context)
	{
		equipedSkill = SkillType.Warp;
	}
	void SelectWhackAMoleSkill(InputAction.CallbackContext context)
	{
		equipedSkill = SkillType.WhackAMole;
	}

	void StartUsingSkill(InputAction.CallbackContext context)
	{
		for(int i = 0; i < skillTypes.Length; i++) {
			if(skillTypes[i] == equipedSkill) {
				skills[i].StartUsingSkill();
				return;
			}
		}
	}

	void UseSkill(InputAction.CallbackContext context)
	{
		for (int i = 0; i < skillTypes.Length; i++) {
			if (skillTypes[i] == equipedSkill) {
				skills[i].UseSkill();
				return;
			}
		}
	}
}
