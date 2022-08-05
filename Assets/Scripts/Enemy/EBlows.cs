using System.Collections;
using UnityEngine;

public class EBlows : MonoBehaviour
{
	private Enemy main;

	[SerializeField] private int maxDamage;
	[SerializeField] private float blowRange;
	[SerializeField] private float distanceToIgnite;
	[SerializeField] private float timeToBlow;

	private bool ignited;

	private void Start()
	{
		main = GetComponent<Enemy>();
		main.OnDied += Blow;
	}
	private void OnDestroy()
	{
		StopAllCoroutines();
		main.OnDied -= Blow;
	}

	private void Update()
	{
		if(main.Data.DistanceToPlayer <= distanceToIgnite) {
			StartBlow();
		}
	}

	private void StartBlow()
	{
		if (ignited) return;

		ignited = true;
		StartCoroutine(WaitForBlow());
	}
	IEnumerator WaitForBlow()
	{
		main.OnIgnited?.Invoke();
		yield return new WaitForSeconds(timeToBlow);
		Blow();
	}
	private void Blow()
	{
		var mult = blowRange - main.Data.DistanceToPlayer;
		mult /= blowRange;
		var dmg = Mathf.FloorToInt(mult * maxDamage);
		main.Data.Player.GetComponent<Player>().GiveDamage(dmg);
		Destroy(gameObject);//temporary
	}
}