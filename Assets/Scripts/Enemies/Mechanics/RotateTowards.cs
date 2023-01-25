using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    public class RotateTowards : MonoBehaviour
    {
        [SerializeField] private GameObject Target;
        [SerializeField] private bool active;
        [SerializeField] private float plusAngle;
        public bool Active { get => active; set => active = value; }

        private void Awake()
        {
            if (Target == null)
            {
                Target = GameObject.FindGameObjectWithTag("Player");
            }
        }

        void Update()
        {
            if (!active) return;

            var direction = Target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90 + plusAngle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}