using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
	public class Enemy : MonoBehaviour
	{
		private Animator animator;

		public SpriteRenderer SpriteRenderer { get; private set; }
		public GameObject Player { get; private set; }

		private void Start()
		{
			Player = GameObject.FindGameObjectWithTag("Player");
			animator = GetComponent<Animator>();
			SpriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			var distance = Vector2.Distance(gameObject.transform.position, Player.transform.position);
			animator.SetFloat("Distance", distance);

		}
	}
}