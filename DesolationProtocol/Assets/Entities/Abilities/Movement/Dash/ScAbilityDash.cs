using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(fileName = "Dash", menuName = "Abilities/Movement/Dash")]
public class ScAbilityDash : ScAbility
{
    [SerializeField] private float strength;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve timeline;

    public override void Try(ScEntity entity)
    {
        ScEntityPlayer player = entity.GetComponent<ScEntityPlayer>();

        if (player && player.movement != Vector3.zero)
        {
            base.Try(entity);
        }
    }
    protected override void Activate(ScEntity entity)
    {
        base.Activate(entity);
        StartCoroutine(Dashing(entity));
    }

    private IEnumerator Dashing(ScEntity entity)
    {
        Rigidbody _rigidbody = entity.GetComponent<Rigidbody>();
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
        Vector3 direction;
        ScEntityPlayer player = entity.GetComponent<ScEntityPlayer>();
        if (player && player.movement != Vector3.zero)
        {
            direction = Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * player.movement;
        }
        else
        {
            direction = _rigidbody.transform.forward;
        }
        float timer = 0f;
        while (timer < duration && _rigidbody)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(direction * strength * timeline.Evaluate(timer), ForceMode.Impulse);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}