using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScWeapon : MonoBehaviour
{
    //variables armas
    public GameObject bullet;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float shootTime = 0.2f;
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private bool automatic;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Image _ammoUI;

    public ParticleSystem muzzleFlash;

    //funcionamiento
    private bool shooting , reloading = false;
    public int bulletsLeft;
    private ScCooldown shootCd = new ScCooldown();
    
    
    public Camera fpsCam;
    public Transform atackPoint;
    private Animator _anim;


    public AudioManager AudioManager;
    public Text ammoText;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        shootCd.ResetCooldown();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !reloading && bulletsLeft < magazineSize)
        {
            Reload();
        }

        if (automatic && shooting)
        {
            TryShoot();
        }
    }
    private void TryShoot()
    {
        if (shootCd.IsReady && shooting && !reloading && bulletsLeft > 0)
        {

            Shoot();
            muzzleFlash.Play();
            AudioManager.PlaySound("SFX", "shoot");

        }
    }

    public void SetShooting(bool value)
    {
        shooting = value;
        if (shooting)
        {
            TryShoot();
        }
    }

    private void Shoot()
    {
        shootCd.StartCooldown(shootTime);
        Invoke("TryShoot", shootTime);
        bulletsLeft--;
        UpdateAmmo();
        
         if (_anim != null)
        {
            _anim.SetTrigger("Fire");
        }

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetpoint;

        if (Physics.Raycast(ray,out RaycastHit hit, 100, layerMask))
        {
            targetpoint = hit.point;
        }
        else
        {
            targetpoint = ray.GetPoint(100);
        }
        Vector3 direction = atackPoint.position - targetpoint;

        Debug.DrawRay(targetpoint, targetpoint - atackPoint.position);
        GameObject currentBullet = Instantiate(bullet, atackPoint.position, Quaternion.LookRotation(targetpoint - atackPoint.position));
        currentBullet.GetComponent<ScProjectile>().owner = GetComponent<ScEntity>();

        if (_anim != null)
        {
            _anim.SetInteger("bullets", bulletsLeft);
        }

        if (bulletsLeft <= 0)
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (reloading) return;

        reloading = true;
        _anim.SetBool("F_reload", true);
        AudioManager.PlaySound("SFX", "reload");

        Invoke("FinishReload", reloadTime);
        Invoke("UpdateAmmo", reloadTime);
        //UpdateAmmo();
    }

    private void FinishReload()
    {
        reloading = false;
        bulletsLeft = magazineSize;
        _anim.SetBool("F_reload", false);
        _anim.SetInteger("bullets", bulletsLeft);

        if (shooting)
        {
            TryShoot();
        }
    }

    private void UpdateAmmo()
    {
        _ammoUI.fillAmount = (float)bulletsLeft / magazineSize;
    }
}
