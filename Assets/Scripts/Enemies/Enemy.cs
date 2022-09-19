using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
	public class Enemy : MonoBehaviour
	{
		public Animator Animator { get; private set; }
		public SpriteRenderer SpriteRenderer { get; private set; }
		public GameObject Player { get; private set; }

		public class EnemyData
		{
			public float speedMultiplier = 1;
		}
		public EnemyData Data { get; set; }

		private void Start()
		{
			Player = GameObject.FindGameObjectWithTag("Player");
			Animator = GetComponent<Animator>();
			SpriteRenderer = GetComponent<SpriteRenderer>();

			Data = new EnemyData();
		}

		private void Update()
		{
			var distance = Vector2.Distance(gameObject.transform.position, Player.transform.position);
			Animator.SetFloat("Distance", distance);
		}
	}
}