
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    EnemyEventSystem enemyEventSystem;

    [SerializeField] int hp;

	void Start()
	{
		enemyEventSystem = GetComponent<EnemyEventSystem>();
		enemyEventSystem.OnGetHit += LoseHealth;
	}

	void OnDestroy()
	{
		enemyEventSystem.OnGetHit -= LoseHealth;
	}

    void LoseHealth(int damage)
	{
        hp -= damage;
        if(hp <= 0) {
            Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}

}
