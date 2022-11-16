using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies.Mechanics
{
    public class ShootInIntervals : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private float interval;
        [SerializeField] private float bulletSpeed;

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

        private IEnumerator Shoot()
        {
            while (active)
            {
                yield return new WaitForSeconds(interval);
                var bullet = Instantiate(prefab, spawnPosition.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = transform.forward * bulletSpeed;
            }
            


        }
        
    }
}