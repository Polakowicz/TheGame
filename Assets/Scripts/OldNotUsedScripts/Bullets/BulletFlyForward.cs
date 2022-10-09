using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Bullets
{
    public class BulletFlyForward : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] private float speed = 10f;

        private void Start()
        {
            //rb = GetComponent<Rigidbody2D>();
            //rb.velocity = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * rb.rotation),
            //    Mathf.Cos(Mathf.Deg2Rad * rb.rotation)) * speed;
        }
    }
}
