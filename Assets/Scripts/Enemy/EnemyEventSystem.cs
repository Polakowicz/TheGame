using System;
using System.Collections;
using UnityEngine;


public class EnemyEventSystem : MonoBehaviour
{
	[SerializeField] private EnemyData data;
	
	//Pulling
	public Action<Transform, float> OnGetPulledValues;
	public Action OnGetPulled;
	public Action OnGetPulledCanceled;

	public void Pull(Transform position, float speed)
	{
		if (!data.Pullable) {
			return;
		}
		OnGetPulled?.Invoke();
		OnGetPulledValues?.Invoke(position, speed);
	}

	public void CancelPulling()
	{
		OnGetPulledCanceled.Invoke();
	}

	//Health
	public Action<int> OnGetHit;
	public Action OnDied;

	public void Hit(int dmg)
	{
		data.HP = Mathf.Clamp(data.HP - dmg, 0, int.MaxValue);
		if (data.HP <= 0) {
			OnDied?.Invoke();
			Destroy(gameObject);
		} else {
			OnGetHit?.Invoke(dmg);
		}
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
