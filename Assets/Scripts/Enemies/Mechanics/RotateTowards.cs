using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    public class RotateTowards : MonoBehaviour
    {
        [SerializeField] private GameObject Target;
        public bool Active { get; set; }

        
        void Update()
        {
            var direction = Target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}