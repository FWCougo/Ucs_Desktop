using Unity.VisualScripting;
using UnityEngine;

public class PISTOL : GUN
{
    [SerializeField]
    private Transform boca_transform;

    [SerializeField]
    private Vector3 direction = Vector3.left;

    [SerializeField] private shakeMode shakeMode;

    [SerializeField] private bool piercing = false;

    public override void UseWeapon(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            direction = dir;
        }

        SingleShot();
        base.UseWeapon(dir);
    }

    public Vector3 TransformDirection(Vector3 direction)
    {
        return new Vector3(direction.x, 0, direction.y);
    }

    public void SingleShot()
    {
        if (canShoot)
        {
            CAMERA_SHAKE.Instance.ShakeWithEnum(shakeMode);
            BULLET_PLAYER _bullet = Instantiate(gun_so.bulletPrefab, boca_transform.position, Quaternion.identity).GetComponent<BULLET_PLAYER>();
            Vector3 spreadDir = TransformDirection(direction);
            _bullet.ReceiveDirection(spreadDir, gun_so.bulletLifeSpan, piercing);
        }
    }
}
