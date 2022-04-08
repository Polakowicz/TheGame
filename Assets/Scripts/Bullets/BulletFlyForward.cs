using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlyForward : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * rb.rotation), Mathf.Cos(Mathf.Deg2Rad * rb.rotation)) * speed;
    }
}
