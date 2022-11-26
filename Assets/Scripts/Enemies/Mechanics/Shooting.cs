using Scripts.Bullets;
using Scripts.Tools;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies.Mechanics
{
    public class Shooting : MonoBehaviour
    {
        [Header("Required data")]
        [SerializeField] private ObjectPool pool;
        [SerializeField] private Transform target;
        [SerializeField] private Transform spawnPosition;
        [Space(20)]

        [Header("Dispartion")]
        [SerializeField] private bool isDispertionActive = false;
        [SerializeField] private float minDispartion = 0;
        [SerializeField] private float maxDispartion = 0;
        private float dispartion;
        [Space(20)]


        [Header("AutoFire")]
        [SerializeField] private bool isAutofireActive = false;
        [SerializeField] private float autoFireDeley = 1;
     

        private void Awake()
        {
            dispartion = minDispartion;
        }

        public GameObject Shoot()
        {
            var bullet = pool.GetObject();
            Quaternion rotation = isDispertionActive ? GetRandomisedAccuracy(transform.rotation) : transform.rotation;
            bullet.transform.SetPositionAndRotation(spawnPosition.position, rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bullet.GetComponent<Bullet>().Speed;

            return bullet;
        }

        private Quaternion GetRandomisedAccuracy(Quaternion rotation)
        {
            float newRotation = Random.Range(rotation.eulerAngles.z - dispartion, rotation.eulerAngles.z + dispartion);
            return Quaternion.Euler(0, 0, newRotation);
        }

        private IEnumerator AutoFire()
        {
            do
            {
                Shoot();

                yield return new WaitForSeconds(autoFireDeley);
            } while (isAutofireActive);
        }

        public void ActivateAutoFire()
        {
            // Can't start 2nd auto fire
            if (isAutofireActive) return;

            isAutofireActive = true;
            StartCoroutine(AutoFire());
        }

        public void DeactivateAutoFire()
        {
            if(!isAutofireActive) return;

            isAutofireActive=false;
        }
    }
}