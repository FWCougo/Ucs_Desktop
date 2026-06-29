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

    [SerializeField] private SpriteRenderer g_Sprite;

    private void Start()
    {
        p_WEAPON = GetComponentInParent<PLAYER_WEAPONS>();

        Reload();
        clip = gun_so.shotClip;
    }

    public override void flipWeapon(bool flipped)
    {
        g_Sprite.flipX = flipped;
    }

    public override void UseWeapon(Vector3 dir)
    {
        Shoot();
    }

    public override void changeLayer(int layer)
    {
        g_Sprite.sortingOrder = layer;
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
        float waitTime = gun_so.realoadTime;

        yield return new WaitForSeconds(waitTime);

        Reload();

        yield return null;
    }

    IEnumerator ShootCoroutine()
    {
        source.PlayOneShot(clip);

        canShoot = false;

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
