using Unity.VisualScripting;
using UnityEngine;

public class SHOTGUN : GUN
{
    [SerializeField]
    private Transform boca_transform;

    [SerializeField]
    private Vector3 direction;

    public override void UseWeapon(Vector3 dir)
    {
        direction = dir;
        MultipleShot();
        base.UseWeapon(dir);
    }

    public void MultipleShot()
    {
        print("Multiple Shot");

        if (canShoot)
        {
            for(int i = 0; i < 5; i++)
            {
                BULLET _bullet = Instantiate(gun_so.bulletPrefab, boca_transform.position, Quaternion.identity).GetComponent<BULLET>();
                _bullet.ReceiveDirection(direction);
            }
            
        }
    }
}
