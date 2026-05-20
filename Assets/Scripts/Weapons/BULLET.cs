using UnityEngine;

public class BULLET : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 20f;
    [SerializeField]
    private Vector3 bulletDir;


    public void ReceiveDirection(Vector3 dir, float autoDestroy)
    {
        Destroy(gameObject, autoDestroy);

        bulletDir.x = dir.x;
        bulletDir.z = dir.y;
    }

    private void FixedUpdate()
    {
        transform.Translate(bulletDir * bulletSpeed * Time.fixedDeltaTime);
    }

}
