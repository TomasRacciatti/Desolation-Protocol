using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class ScEntityPlayer : ScEntity
{
    [SerializeField] public Transform FocusRotator;
    [SerializeField] public float sens = 1;
    [SerializeField] private ScHud Hud;
    public Vector3 movement;
    public int totaljumps = 1;
    private int _jumps = 1;
    public float airControl = 0.8f;
    public bool landed = true;
    public float experience = 0f;

    [Header("Step Climb")]

    [SerializeField] protected GameObject stepRayUpper;
    [SerializeField] protected GameObject stepRayLower;
    [SerializeField] protected float stepHeight = 0.3f;
    [SerializeField] protected float stepSmooth = 0.1f;

    protected override void Awake()
    {
        base.Awake();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _jumps = totaljumps;
        Hud.CountHP();
        _anim = GetComponentInChildren<Animator>();
    }

    protected override void Update()
    {
        if (health > 0)
        {
            _rigidbody.transform.Rotate(Vector3.up, Input.GetAxis("MouseX") * sens, Space.World);
            FocusRotator.Rotate(FocusRotator.right, Mathf.Clamp(-1 * Input.GetAxis("MouseY") * sens, -90, 90), Space.World);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (health > 0)
        {
            if (movement != Vector3.zero)
            {

                if (new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude <= Stats.movementSpeed)
                {
                    if (landed)
                    {
                        //_rigidbody.AddForce(Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * Stats.movementSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
                        _rigidbody.velocity = Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * Stats.movementSpeed + new Vector3(0, _rigidbody.velocity.y, 0);
                    }
                    else
                    {
                        //_rigidbody.AddForce(Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * Stats.movementSpeed * Time.fixedDeltaTime * airControl, ForceMode.VelocityChange);
                        _rigidbody.velocity = Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * Stats.movementSpeed * airControl + new Vector3(0, _rigidbody.velocity.y, 0);
                    }
                }
                else
                {
                    _rigidbody.velocity = _rigidbody.velocity.normalized * Stats.movementSpeed;
                }
            }
            else
            {
                _rigidbody.velocity = Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * Stats.movementSpeed + new Vector3(0, _rigidbody.velocity.y, 0);
                //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x * 0.95f, _rigidbody.velocity.y, _rigidbody.velocity.z * 0.95f);
            }
        }
        //(Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * _rigidbody.velocity).normalized.x;

        bool isMoving = _rigidbody.velocity.magnitude > 0.1f;
        _anim.SetBool("IsMoving", isMoving);

        _anim.SetFloat("XAxis", movement.x, 0.1f, Time.deltaTime);
        _anim.SetFloat("ZAxis", movement.z, 0.1f, Time.deltaTime);

        StepClimb();
    }


    public void OnLand()
    {
        _anim.SetTrigger("Landed");
        _jumps = totaljumps;
        landed = true;
        _anim.SetBool("Landed", landed);
    }

    public void OnAir()
    {
        _jumps = totaljumps - 1;
        landed = false;
        _anim.SetBool("Landed", landed);
    }

    public override void TakeDamage(float incomingDamage, float incomingPenLinear = 0, float incomingPenPerc = 0)
    {
        base.TakeDamage(incomingDamage, incomingPenLinear, incomingPenPerc);
        Hud.CountHP();
        AudioManager.PlaySound("SFX", "damage1");

    }

    public override void Heal(float heal)
    {
        base.Heal(heal);
        Hud.CountHP();
    }

    protected override void Die()
    {
        base.Die();
        _anim.SetTrigger("Dead");
        Invoke("OnDeathLoadMainMenu", 5f);
        movement = Vector3.zero;
    }

    private void OnDeathLoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    //inputs
    public void Test(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed)
        {
            Invoke("OnDeathLoadMainMenu", 5f);
        }
    }

    public void Movement(InputAction.CallbackContext CallbackContext)
    {
        if ((CallbackContext.performed || CallbackContext.canceled) && health > 0)
        {
            movement = new Vector3(CallbackContext.ReadValue<Vector2>().x, 0f, CallbackContext.ReadValue<Vector2>().y);
        }
    }

    public void Jump(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed && health > 0)
        {
            if (_jumps > 0)
            {
                _anim.SetTrigger("Jump");
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
                _rigidbody.AddForce(_rigidbody.transform.up * Stats.jumpForce * 5f, ForceMode.VelocityChange);
                if (!landed)
                {
                    _jumps--;
                }
            }
        }
    }

    public void Interact(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed && health > 0)
        {

        }
    }

    public void Shoot(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed && health > 0 && Time.timeScale != 0)
        {
            GetComponent<ScWeapon>().SetShooting(true);
        }
        if (CallbackContext.canceled)
        {
            GetComponent<ScWeapon>().SetShooting(false);
        }
    }

    private void TryAbility(InputAction.CallbackContext CallbackContext, int Selected)
    {
        if (CallbackContext.performed && health > 0) TryAbility(Selected);
    }

    public void TryAbility0(InputAction.CallbackContext CallbackContext)
    {
        TryAbility(CallbackContext, 0);
    }

    public void TryAbility1(InputAction.CallbackContext CallbackContext)
    {
        TryAbility(CallbackContext, 1);
    }

    public void TryAbility2(InputAction.CallbackContext CallbackContext)
    {
        TryAbility(CallbackContext, 2);
    }

    public void TryAbility3(InputAction.CallbackContext CallbackContext)
    {
        TryAbility(CallbackContext, 3);
    }

    public void Pause(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed)
        {
            Hud.TogglePause();
        }
    }

    public void PlaySoundFoot()
    {
        AudioManager.PlaySound("SFX", "Run");
    }

    private void StepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                _rigidbody.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }

        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
        {
            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
            {
                _rigidbody.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
        {
            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
            {
                _rigidbody.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }

    public override void Silenced(bool action)
    {
        base.Silenced(action);
        Hud.Silencers(silencers != 0);
    }
}