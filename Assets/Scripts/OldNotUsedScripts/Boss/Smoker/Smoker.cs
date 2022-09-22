using System.Collections;
using UnityEngine;


public class Smoker : MonoBehaviour, IPullable
{
	[SerializeField] GameObject bullet;

	private Animator animator;
	public GameObject Player {get; private set;}

	private int pullsToDie = 3;

	private bool alive;

	private void Start()
	{
		animator = GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag("Player");
		alive = true;
	}

	private void Update()
	{
		
	}

	public void Pull(float speed)
	{
		pullsToDie--;
		if(pullsToDie <= 0) {
			Destroy(gameObject);
		}
	}
}
