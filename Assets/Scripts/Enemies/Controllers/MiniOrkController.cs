using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemies
{
    public class MiniOrkController : MonoBehaviour
    {
        private Enemy manager;

        [SerializeField] private float spotedDistance;
        [SerializeField] private float meleeAttackDistance;

		private void Start()
		{
			manager = GetComponent<Enemy>();
		}

		private void Update()
		{
			

		}
	}
}
