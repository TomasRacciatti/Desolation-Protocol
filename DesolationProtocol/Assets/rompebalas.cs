using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rompebalas : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ScProjectile scProjectile = GetComponent<ScProjectile>();
        if (scProjectile)
        {
            Destroy(other.gameObject);
        }
    }
}
