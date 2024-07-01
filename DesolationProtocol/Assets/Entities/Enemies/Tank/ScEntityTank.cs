using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ScEntityTank : ScEntityEnemy
{

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
        if (Vector3.Distance(_target.position, transform.position) < 3)
        {
            if (_active)
            {
                RotateTracking();
                _anim.SetBool("InRange", true);
            }
        }
        else
        {
            if (!_active)
            {
                StopTracking();
                _anim.SetBool("InRange", false);
            }
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.LookRotation(_target.position - transform.position), Stats.movementSpeed * Time.deltaTime);
        }
    }
    public override void TakeDamage(float incomingDamage, float incomingPenLinear = 0, float incomingPenPerc = 0)
    {
        base.TakeDamage(incomingDamage, incomingPenLinear, incomingPenPerc);
        _audioSource.Play();
    }
}