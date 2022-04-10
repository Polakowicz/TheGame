using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hp;

    void Die()
	{
        Destroy(gameObject);
	}

    public void GetHit(int damage)
	{
        hp -= damage;
        if(hp <= 0) {
            Die();
		}
	}
}
