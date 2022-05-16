using System.Collections;
using UnityEngine;

public class GoblinAnimationController : MonoBehaviour
{
	private Goblin main;

	private void Start()
	{
		main = GetComponent<Goblin>();
		main.OnPlayerInKickRange += StartKickAnimation;
	}
	private void OnDestroy()
	{
		main.OnPlayerInKickRange -= StartKickAnimation;
	}

	private void StartKickAnimation()
	{
		Debug.Log("Start kick animation");
	}
	public void KickPlayer()
	{
		main.OnKickPlayer?.Invoke();
	}


}