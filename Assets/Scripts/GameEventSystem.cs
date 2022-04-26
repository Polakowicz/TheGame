using System;
using System.Collections;
using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
	public static GameEventSystem Instance;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	public Action OnPlayerDied;
}
