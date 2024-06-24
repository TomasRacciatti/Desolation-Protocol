using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTurret : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Target;
    public float RotationSpeed;
    public float Range;
    public float RangeOfSight;
    public Transform Rotator;
    public Transform ShootPoint;
    public LayerMask PerceptibleLayer;
    public bool TurretOn;
    public AudioManager AudioManager;

    public float FireRate;
    private float _counter;

    private void Start()
    {
        Target = FindObjectOfType<ScWeapon>().transform;
        TurretOn = true;
    }

    private void Update()
    {
        _counter += Time.deltaTime;
        
        //print(PlayerInSight);
        if (TurretOn == true)
        {
            if (Vector3.Distance(transform.position, Target.position) < RangeOfSight)   //chequeamos si ve o no al jugador
            {
                //print("Player in sight");
                Vector3 DirectionToPlayer = (Target.position - Rotator.position);
                DirectionToPlayer.y = 0;
                DirectionToPlayer = DirectionToPlayer.normalized;

                Debug.DrawRay(Rotator.position, DirectionToPlayer * RangeOfSight, Color.green);

                if (Physics.Raycast(Rotator.position, DirectionToPlayer, out RaycastHit Hit, RangeOfSight, PerceptibleLayer))
                {
                    if (Hit.transform == Target.transform) //si lo ve hacemos que apunte al jugador
                    {
                        //print("Te veo");
                        Aim(DirectionToPlayer);
                    }
                    else
                    {
                        //print("No te veo");
                        Idle();
                    }
                }

            }
            else
            {
                //print("Player out of sight");
                Idle();
            }
        }
        
    }

    public void Idle() 
    {
        Rotator.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }

    public void Aim(Vector3 Direction)
    {

        
        Quaternion QuaDirection = Quaternion.LookRotation(Direction);
        Rotator.rotation = Quaternion.Slerp(Rotator.rotation, QuaDirection, Time.deltaTime * RotationSpeed * 0.25f); //uso slerp porque es mas fluido y rapido que RotateTowards

        Debug.DrawRay(Rotator.position, Rotator.forward * Range, Color.red);

        if (Physics.Raycast(Rotator.position, Rotator.forward, out RaycastHit Hit, Range, PerceptibleLayer))
        {
            if (Hit.transform == Target.transform) 
            {
                //print("Te disparo");
                Fire();

            }
            
        }
    }

    public void Fire()
    {
        if(_counter >= FireRate)
        {
            _counter = 0;
            Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            PlaySoundShoot();
        }
    }

    public void TurnOn()
    {
        TurretOn = true;
        Rotator.Rotate(-40, 0, 0);
    }
    public void TurnOff()
    {
        if(TurretOn == true)
        {
            //print("Me apague");
            TurretOn = false;
            Rotator.Rotate(40,0,0);
        }
        Invoke("TurnOn", 10f);
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Rotator.position, Range);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Rotator.position, RangeOfSight);
    }
    public void PlaySoundShoot()
    {
        AudioManager.PlaySound("SFX","turret");
    }
}
