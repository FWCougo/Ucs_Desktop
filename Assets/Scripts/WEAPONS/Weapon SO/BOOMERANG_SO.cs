using UnityEngine;

[CreateAssetMenu(menuName = "WEAPONS / BOOMERANG")]
public class BOOMERANG_SO : WEAPON_SO
{
    public BULLET bulletPrefab;
    public float b_LifeSpan = 3f;
    public float b_Range = 3f;
    // No BOOMERANG_SO
    public float b_ReturnAcceleration = 2f; // Quanto mais alto, mais rápido acelera
}
