using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Properties
    [SerializeField] int hp;
    [SerializeField] float speed;

    public void OnGetHit(int damage)
	{
        hp -= damage;
        Debug.Log("HP: " + hp);
        if(hp <= 0) {
            Destroy(gameObject);
		}
	}
}
