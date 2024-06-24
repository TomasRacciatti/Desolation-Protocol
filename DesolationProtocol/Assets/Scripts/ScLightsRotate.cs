using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScLightsRotate : MonoBehaviour
{
    [SerializeField] float speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, speed * Time.deltaTime, 0);
    }
}
