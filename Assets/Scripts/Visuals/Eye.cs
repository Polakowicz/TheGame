using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public float factor = 0.25f;
    public float limit = 0.08f;
    private Vector3 center;

    private void Start()
    {
        center = transform.position;
    }

    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0.0f;

        Vector3 dir = pos * factor;

        dir = Vector3.ClampMagnitude(dir, limit);
        transform.position = center + dir;
    }
}
