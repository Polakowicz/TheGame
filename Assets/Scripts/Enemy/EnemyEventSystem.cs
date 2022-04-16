using System;
using System.Collections;
using UnityEngine;


public class EnemyEventSystem : MonoBehaviour
{
	private EnemyData data;

	public Action<int> OnGetHit;
	public Action OnDied;
	public void GetHit(int dmg)
	{
		data.HP = Mathf.Clamp(data.HP - dmg, 0, int.MaxValue);
		if(data.HP <= 0) {
			OnDied?.Invoke();
		} else {
			OnGetHit?.Invoke(dmg);
		}
	}

	public Action OnGetCaught;
	public Action OnGetCaughtCanceled;
	public Action<Transform, float> OnGetPulled;
	public Action OnGetPulledCanceled;
	public void GetCaught()
	{
		OnGetCaught?.Invoke();
	}

	public void CancelGetCaught()
	{
		OnGetPulledCanceled?.Invoke();
		OnGetCaughtCanceled?.Invoke();
	}

	public void Pull(Transform position, float speed)
	{
		OnGetPulled?.Invoke(position, speed);
	}

	public void CancelPull()
	{
		OnGetPulledCanceled.Invoke();
	}


	//Stun
	public Action OnGetStuned;
	public Action OnGetStunedEnded;
	public void Stun(float time)
	{
		OnGetStuned?.Invoke();
		StartCoroutine(WaitStunTime(time));
	}

	IEnumerator WaitStunTime(float time)
	{
		yield return new WaitForSeconds(time);
		OnGetStunedEnded?.Invoke();
	}

}
