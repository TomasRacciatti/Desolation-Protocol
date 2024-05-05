using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

 

public class ScTank : MonoBehaviour
{
    
    [SerializeField] private NavMeshAgent _agent;
    public Transform Player;
    public float speed = 5f;
    private Animator _anim;
    private void Awake()
    {
        Player = FindObjectOfType<ScPlayer>().transform;
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }


    /*private void Start()
    {
        Invoke("die", 5f);
    }
    */
    private void Update()
    {
        _agent.SetDestination(Player.position);

    }
    
    public void die()
    {
        _anim.SetTrigger("dead");
    }


}
