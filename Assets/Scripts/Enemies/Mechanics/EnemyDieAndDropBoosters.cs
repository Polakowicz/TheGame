using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    public class EnemyDieAndDropBoosters : MonoBehaviour
    {
        [SerializeField] private GameObject[] itemsList;
        [SerializeField] private float chanceToDropItem = 0.15f;

        public void DropRandomItem()
        {
            var random = Random.Range(0f, 1f);
            if (random > chanceToDropItem) return;

            random = Random.Range(0, itemsList.Length);
            var position = (Vector2)transform.position + Random.insideUnitCircle;
            Instantiate(itemsList[Mathf.FloorToInt(random)], position, Quaternion.identity);
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}