using Unity.VisualScripting;
using UnityEngine;

public class SHOTGUN : GUN
{
    [SerializeField]
    private Transform boca_transform;

    [SerializeField]
    private Vector3 direction = Vector3.left;

    [Range(-0.5f, 0.5f)]
    public float randomness = 0.5f;

    public override void UseWeapon(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            direction = dir;
        }

        MultipleShot();
        base.UseWeapon(dir);
    }

    public Vector3 SpreadDirection(Vector3 direction, float spreadAngle, int index, int total)
    {
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Distribui proporcionalmente: -20°, -10°, 0°, 10°, 20°
        float t = (total > 1) ? (index / (float)(total - 1)) : 0.5f;
        float offset = Mathf.Lerp(-spreadAngle / 2f, spreadAngle / 2f, t);

        float finalAngle = (baseAngle + offset) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(finalAngle), 0, Mathf.Sin(finalAngle));
    }

    public void MultipleShot()
    {
        if (canShoot)
        {
            CAMERA_SHAKE.Instance.ShakeDefault();
            int bulletCount = 5;
            for (int i = 0; i < bulletCount; i++)
            {
                BULLET_PLAYER _bullet = Instantiate(gun_so.bulletPrefab, boca_transform.position, Quaternion.identity).GetComponent<BULLET_PLAYER>();
                Vector3 spreadDir = SpreadDirection(direction, 40f, i, bulletCount);
                _bullet.ReceiveDirection(spreadDir, gun_so.bulletLifeSpan);
            }
        }
    }
}
