using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTurret : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Player;
    public float Range;
    public float RangeOfSight;
    public Transform Rotator;
    public Transform ShootPoint;

    private void Start()
    {
        //Player = FindObjectOfType<Player>().transform;           
    }

    private void Update()
    {
        float Distance = Vector3.Distance(transform.position, Player.position);
        print(Distance);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Rotator.position, Range);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Rotator.position, RangeOfSight);
    }

}
