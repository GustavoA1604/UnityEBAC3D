using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : EnemyBase
{
    [Header("Waypoints")]
    public GameObject[] wayPoints;
    private int _index = 0;

    public float minDistance = 1f;
    public float speed = 1f;

    void Update()
    {
        if (Vector3.Distance(transform.position, wayPoints[_index].transform.position) < minDistance)
        {
            _index = (_index + 1) % wayPoints.Length;
        }

        transform.position = Vector3.MoveTowards(transform.position, wayPoints[_index].transform.position, Time.deltaTime * speed);
    }
}
