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

    public Vector2 RandomizeDirection(Vector2 direction, float spreadAngle)
    {
        // Converte a direção para ângulo em graus
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adiciona um offset aleatório dentro do cone de dispersão
        float randomOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

        float finalAngle = (baseAngle + randomOffset) * Mathf.Deg2Rad;

        // Converte de volta para vetor
        return new Vector2(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle));
    }

    public void MultipleShot()
    {
        print("Multiple Shot");
        if (canShoot)
        {
            for (int i = 0; i < 5; i++)
            {
                BULLET _bullet = Instantiate(gun_so.bulletPrefab, boca_transform.position, Quaternion.identity).GetComponent<BULLET>();
                Vector2 spreadDir = RandomizeDirection(direction, 25f);
                _bullet.ReceiveDirection(spreadDir, gun_so.bulletLifeSpan);
            }
        }
    }

    public Vector2 RandomizeDirection(Vector2 direction, float spreadAngle)
    {
        // Converte a direção para ângulo em graus
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adiciona um offset aleatório dentro do cone de dispersão
        float randomOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

        float finalAngle = (baseAngle + randomOffset) * Mathf.Deg2Rad;

        // Converte de volta para vetor
        return new Vector2(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle));
    }
}
