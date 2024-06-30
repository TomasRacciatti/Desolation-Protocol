using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _minDistance = 0.5f;
    [SerializeField] private float _maxDistance = 1.0f;
    [SerializeField] private float _smooth = 10.0f;
    [SerializeField] private float _hitDistanceModifier = 0.87f;
    [SerializeField] private LayerMask _collisionMask;

    private Vector3 dollyDir;
    [SerializeField] private Vector3 dollyDirAdjusted;
    private float distance;

    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    void Update()
    {
        Vector3 desiredCameraPos = _player.TransformPoint(dollyDir * _maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(_player.position, desiredCameraPos, out hit, _collisionMask))
        {
            distance = Mathf.Clamp((hit.distance * _hitDistanceModifier), _minDistance, _maxDistance);
        }
        else
        {
            distance = _maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * _smooth);
    }
}
