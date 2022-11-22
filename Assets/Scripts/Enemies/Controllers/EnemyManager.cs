using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Enemies
{
	public class EnemyManager : MonoBehaviour
	{
		// Components
		public Animator Animator { get; private set; }
		public SpriteRenderer SpriteRenderer { get; private set; }

		// Player gameobject to 
		public GameObject Player { get; private set; }

		// Data shared between enemy components
		public class EnemyData
		{
			public float speedMultiplier = 1;
		}
		public EnemyData Data { get; set; }

		private void Awake()
		{
			Animator = GetComponent<Animator>();
			SpriteRenderer = GetComponent<SpriteRenderer>();

			Data = new EnemyData();
		}

		private void Start()
		{
			Player = GameObject.FindGameObjectWithTag("Player");
			Assert.IsNotNull(Player);
		}

		private void Update()
		{
			var distance = Vector2.Distance(gameObject.transform.position, Player.transform.position);
			Animator.SetFloat("Distance", distance);
		}

		public Action OnDamaged; 
	}
}