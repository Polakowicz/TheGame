using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Properties
    [SerializeField] int hp;

    public void GetHit(int damage)
	{
        hp -= damage;
        if(hp <= 0) {
            Destroy(gameObject);
		}
	}
}
