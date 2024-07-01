using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScEntityEnemy : ScEntity
{
    private NavMeshAgent _agent;
    protected Transform _target;
    protected bool _active = true;
    protected bool rotateidle = false;
    protected bool _isDead = false;

    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
        _target = FindObjectOfType<ScEntityPlayer>().transform;
    }

    public override void Start()
    {
        base.Start();
        _agent.speed = Stats.movementSpeed;
    }

    protected override void Update()
    {
        if (_active && _agent)
        {
            if (_target && !_isDead)
            {
                _agent.SetDestination(_target.position);
            }
            else
            {
                _target = FindObjectOfType<ScEntityPlayer>().transform;
            }
        }
    }

    protected override void Die()
    {
        _isDead = true;
        StopTracking();
        Destroy(_agent);
        Destroy(gameObject, 3);
        base.Die();
    }

    protected void KeepTracking()
    {
        if (health > 0)
        {
            _active = true;
            _agent.SetDestination(_target.position);
        }
    }

    protected void StopTracking()
    {
        _active = false;
        if (_agent)
        {
            _agent.SetDestination(transform.position);
        }
    }

    /*
    // NAVMESH MODIFICADO PARA QUE NO HAGA FALTA HACER ESTO POR CODIGO

     
    protected override void StepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                AdjustAgentPosition(new Vector3(0f, stepSmooth, 0f));
            }
        }

        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
        {
            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
            {
                AdjustAgentPosition(new Vector3(0f, stepSmooth, 0f));
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
        {
            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
            {
                AdjustAgentPosition(new Vector3(0f, stepSmooth, 0f));
            }
        }
    }

    private void AdjustAgentPosition(Vector3 adjustment)
    {
        _agent.updatePosition = false;
        _rigidbody.position += adjustment;
        _agent.nextPosition = _rigidbody.position;
        _agent.updatePosition = true;
    }*/
}