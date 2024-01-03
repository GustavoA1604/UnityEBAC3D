using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyWalk : EnemyBase
{
    [Header("Waypoints")]
    public GameObject[] wayPoints;
    private int _index = 0;

    public float minDistance = 1f;
    public float speed = 1f;

    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, wayPoints[_index].transform.position) < minDistance)
        {
            _index = (_index + 1) % wayPoints.Length;
        }

        transform.position = Vector3.MoveTowards(transform.position, wayPoints[_index].transform.position, Time.deltaTime * speed);
        //transform.LookAt(wayPoints[_index].transform.position);
        transform.DOLookAt(wayPoints[_index].transform.position, 1f);
    }
}
