using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float minDist = .2f;
    public float initialSpeed = 10f;
    public float acceleration = 5f;
    private float _currentSpeed;

    private void Start()
    {
        _currentSpeed = initialSpeed;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Player._instance.transform.position) > minDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player._instance.transform.position, Time.deltaTime * _currentSpeed);
            _currentSpeed += Time.deltaTime * acceleration;
        }
    }
}
