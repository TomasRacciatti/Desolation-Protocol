using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

 

public class ScTank : MonoBehaviour
{

     [SerializeField] private NavMeshAgent _agent;
     public Transform Player;

     public float speed = 5f;

    private void Awake()
    {
        Player = FindObjectOfType<ScPlayer>().transform;
       
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //Player = FindObjectOfType<ScPlayer>().transform;
       // _agent.SetDestination(Player.position);
    }
    private void Update()
    {
        _agent.SetDestination(Player.position);


    }

    


}
