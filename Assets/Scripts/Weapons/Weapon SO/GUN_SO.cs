using UnityEngine;

[CreateAssetMenu(menuName = "WEAPONS / Gun")]
public class GUN_SO : WEAPON_SO
{
    public int maxAmmo = 8;
    public int realoadTime = 3;
    public BULLET bulletPrefab;
}
