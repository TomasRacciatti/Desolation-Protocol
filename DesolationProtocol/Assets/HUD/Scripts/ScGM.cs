using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScGM : MonoBehaviour
{
    [SerializeField] private int _Ammo;
    [SerializeField] private int _AmmoMax;
    [SerializeField] private bool IsFiring;
    public Text AmmoCounter;
    
    public Text WaveCounter;
    [SerializeField] private int _Wave;


    void Start()
    {
        _Ammo = _AmmoMax;
        _Wave = 1;
    }


    void Update()
    {
        AmmoCounter.text = _Ammo.ToString();
        WaveCounter.text = "Oleada " + _Wave.ToString();
        if (Input.GetMouseButtonDown(0) && !IsFiring && _Ammo > 0)
        {
            IsFiring = true;
            _Ammo--;
            IsFiring=false;
        }
    }

    public void WaveIncrease()
    {
        _Wave++;
    }
}
