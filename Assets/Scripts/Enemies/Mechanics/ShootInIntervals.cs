﻿using Scripts.Bullets;
using Scripts.Tools;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    public class ShootInIntervals : MonoBehaviour
    {
        [SerializeField] private ObjectPool pool;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private float interval;

        [SerializeField] private bool active;
        public bool Active
        {
            get => active;
            set
            {
                active = value;
                if (active)
                {
                    StartCoroutine(Shoot());
                }
            }
        }

        private void Awake()
        {
            if (active)
            {
                StartCoroutine(Shoot());
            }
        }

        private IEnumerator Shoot()
        {
            while (active)
            {
                yield return new WaitForSeconds(interval);
                var bullet = pool.GetObject();

                bullet.transform.position = spawnPosition.position;
                bullet.transform.rotation = transform.rotation;
                bullet.GetComponent<Rigidbody2D>().velocity = transform.up * bullet.GetComponent<Bullet>().Speed;
            }

        }
        
    }
}