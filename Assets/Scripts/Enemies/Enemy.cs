using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
	public class Enemy : MonoBehaviour
	{
		public GameObject Player { get; private set; }

		private void Start()
		{
			Player = GameObject.FindGameObjectWithTag("Player");
		}
	}
}