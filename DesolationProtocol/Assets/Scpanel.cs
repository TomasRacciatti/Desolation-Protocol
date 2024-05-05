using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scpanel : MonoBehaviour
{
    [SerializeField] private ScTurret Turret;

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Turret.TurnOff();
    }
}
