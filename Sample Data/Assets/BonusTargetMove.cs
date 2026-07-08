using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTargetMove : MonoBehaviour
{
    public float moveRange = 3.0f;
    public float moveSpeed = 6.0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float x = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;
        transform.position = startPosition + new Vector3(x, 0, 0);
    }
}