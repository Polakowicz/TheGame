using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    public class EnemyDieAndDropBoosters : MonoBehaviour
    {
        public void Die()
        {
            Destroy(gameObject);
        }
    }
}