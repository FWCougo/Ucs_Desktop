using UnityEngine;

[CreateAssetMenu(menuName = "WEAPONS / Gun")]
public class GUN_SO : WEAPON_SO
{
    public int maxAmmo = 8;
    public float realoadTime = 3;
    public BULLET_PLAYER bulletPrefab;
    public float bulletLifeSpan = 1f;
    public AudioClip shotClip;
}
