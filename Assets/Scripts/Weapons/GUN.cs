using System.Collections;
using UnityEngine;

public class GUN : WEAPON
{
    [SerializeField]
    public GUN_SO gun_so;

    [SerializeField]
    private int currentAmmo;

    public bool canShoot = true;

    private void Start()
    {
        Reload();
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
            StartCoroutine(ReloadCoroutine());
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
        canShoot = false;

        print("Shooting " + gun_so.itemName);

        currentAmmo--;

        Shoot();

        float waitTime = gun_so.cadency;

        yield return new WaitForSeconds(waitTime);

        canShoot = true;


        yield return null;
    }

}
