using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ammo : MonoBehaviour
{
    [SerializeField] private int _Ammo;
    [SerializeField] private int _AmmoMax;
    [SerializeField] private bool IsFiring;
    public Text AmmoCounter;


    void Start()
    {
        _Ammo = _AmmoMax;
    }


    void Update()
    {
        AmmoCounter.text = _Ammo.ToString();
        if (Input.GetMouseButtonDown(0) && !IsFiring && _Ammo > 0)
        {
            IsFiring = true;
            _Ammo--;
            IsFiring=false;
        }
    }
}
