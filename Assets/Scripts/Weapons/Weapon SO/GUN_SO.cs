using UnityEngine;

[CreateAssetMenu(menuName = "WEAPONS / Gun")]
public class GUN_SO : WEAPON_SO
{
    public int maxAmmo = 8;
    public float realoadTime = 3;
    public BULLET bulletPrefab;
    public float bulletTimeSpan = 1f;

}
