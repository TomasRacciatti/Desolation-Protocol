using UnityEngine;

public class ScEntityTank : ScEntityEnemy
{

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        

    }

    public override void Start()
    {
        base.Start();
        KeepTracking();
    }

    protected override void Update()
    {
        base.Update();
        if (_active && !_isDead)
        {
            if (Vector3.Distance(_target.position, transform.position) < 1.8f)
            {
                
                _anim.SetBool("InRange", true);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_target.position - transform.position), 100 * Time.deltaTime);
            }
            else
            {
                
                _anim.SetBool("InRange", false);
            }
        }
    }

    public override void TakeDamage(float incomingDamage, float incomingPenLinear = 0, float incomingPenPerc = 0)
    {
        base.TakeDamage(incomingDamage, incomingPenLinear, incomingPenPerc);
        _audioSource.Play();
    }
}