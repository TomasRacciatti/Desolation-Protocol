using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScEntitySilencer : ScEntityEnemy
{
    [SerializeField] private float walkDistance = 20f;
    [SerializeField] public float silenceDistance = 40f;
    [SerializeField] private LayerMask layerMask;


    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }
    protected override void Update()
    {
        base.Update();
        if (Vector3.Distance(_target.position, transform.position) < walkDistance)
        {
            StopTracking();
            _anim.SetBool("InRange", true);
        }
        else
        {
            KeepTracking();
            _anim.SetBool("InRange", false);
        }
    }

    public void Silence(Collider other, bool action)
    {
        if (health > 0)
        {
            ScEntity otherEntity = other.GetComponent<ScEntity>();
            if (otherEntity && Team != otherEntity.Team)
            {             
                otherEntity.Silenced(action);
            }
        }
    }

    protected override void Die()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, silenceDistance, layerMask);
        _audioSource.Play();

        foreach (Collider collider in hitColliders)
        {
            ScEntity otherEntity = collider.GetComponent<ScEntity>();
            if (otherEntity && Team != otherEntity.Team)
            {
                otherEntity.Silenced(false);
            }
        }
        base.Die();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, walkDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, silenceDistance);
    }
}