using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    public class RotateTowards : MonoBehaviour
    {
        [SerializeField] private GameObject Target;
        [SerializeField] private bool active;
        public bool Active { get => active; set => active = value; }

        
        void Update()
        {
            var direction = Target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}