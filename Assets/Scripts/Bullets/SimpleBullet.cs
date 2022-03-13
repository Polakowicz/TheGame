using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    //Components
    Rigidbody2D rb;

    //Parameters
    [SerializeField] float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
