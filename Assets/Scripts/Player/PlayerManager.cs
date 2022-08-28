using Scripts.Player.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public AudioManager AudioManager { get; private set; }
    public AnimationController AnimationController { get; private set; }
    public PowerUpController PowerUpController { get; private set; }

    public enum PlayerState
	{
        Walk,
        Dash
	}
    public PlayerState State;
    public Vector2 AimDirection;
    public Vector2 MoveDirection;

	void Start()
	{
        AudioManager = FindObjectOfType<AudioManager>();
        AnimationController = GetComponentInChildren<AnimationController>();
	}
}
