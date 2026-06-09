using System.Collections;
using UnityEngine;

public class GUN : WEAPON
{
    [SerializeField]
    public GUN_SO gun_so;

    [SerializeField]
    private int currentAmmo;

    public bool canShoot = true;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;

    private PLAYER_WEAPONS p_WEAPON;

    private void Start()
    {
        p_WEAPON = GetComponentInParent<PLAYER_WEAPONS>();

        Reload();
        clip = gun_so.shotClip;
    }

    public override void UseWeapon(Vector3 dir)
    {
        Shoot();
    }

    public virtual void Shoot()
    {
        if (currentAmmo <= 0)
        {
            canShoot = false;

            if (!isMainWeapon)
            {
                p_WEAPON.ChangeBackToMain();
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(ReloadCoroutine());
            }
        }
        else
        {
            if(canShoot)
            StartCoroutine(ShootCoroutine());
        }
    }

    private void Reload()
    {
        currentAmmo = gun_so.maxAmmo;

        canShoot = true;
    }

    IEnumerator ReloadCoroutine()
    {
        print("Reloading " + gun_so.itemName);

        float waitTime = gun_so.realoadTime;

        yield return new WaitForSeconds(waitTime);

        Reload();

        yield return null;
    }

    IEnumerator ShootCoroutine()
    {
        source.PlayOneShot(clip);

        canShoot = false;

        print("Shooting " + gun_so.itemName);

        currentAmmo--;
        if (!isMainWeapon)
        {
            float _cAmmo = currentAmmo;
            float _maxAmmo = gun_so.maxAmmo;

            p_WEAPON.ReduceAmmo(_cAmmo / _maxAmmo);
        }
        Shoot();

        float waitTime = gun_so.cadency;

        yield return new WaitForSeconds(waitTime);

        canShoot = true;


        yield return null;
    }

}
