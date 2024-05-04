using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScTurret : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Player;
    public float RotationSpeed;
    public float Range;
    public float RangeOfSight;
    public Transform Rotator;
    public Transform ShootPoint;
    public LayerMask PerceptibleLayer;

    private void Start()
    {
        //Player = FindObjectOfType<Player>().transform;           
    }

    private void Update()
    {
        bool PlayerInSight = Vector3.Distance(transform.position, Player.position) < RangeOfSight;
        print(PlayerInSight);

        if (PlayerInSight == true)
        {
            print("Player in sight");
            Vector3 DirectionToPlayer = (Player.position - Rotator.position).normalized;
            DirectionToPlayer.y = 0;

            Debug.DrawRay(Rotator.position, DirectionToPlayer*RangeOfSight, Color.green);

            if (Physics.Raycast(Rotator.position, DirectionToPlayer, out RaycastHit Hit,RangeOfSight,PerceptibleLayer))
            {
                if (Hit.transform == Player.transform)
                {
                    print("Te veo");
                    Aim(DirectionToPlayer);
                }
                else
                {
                    print("No te veo");
                }
            }
            
        }
        else
        {
            print("Player out of sight");
            Idle();
        }
    }

    public void Idle() 
    {
        Rotator.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }

    public void Aim(Vector3 Direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(Direction);
        Rotator.rotation = Quaternion.Slerp(Rotator.rotation, lookRotation, Time.deltaTime * RotationSpeed * 0.25f);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Rotator.position, Range);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Rotator.position, RangeOfSight);
    }

}
