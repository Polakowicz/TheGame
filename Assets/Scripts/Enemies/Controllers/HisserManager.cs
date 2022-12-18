using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    public class HisserManager : MonoBehaviour
    {
        private EnemyMaleeAttack maleeAttack;

        private void Start()
        {
            maleeAttack = GetComponent<EnemyMaleeAttack>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(Attack());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StopAllCoroutines();
            }
        }

        IEnumerator Attack()
        {
            while (true)
            {
                Debug.Log("Attack");
                maleeAttack.Attack();
                yield return new WaitForSeconds(1f);
            }  
        }
    }
}