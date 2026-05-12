using UnityEngine;

public class BULLET : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 20f;
    private Vector3 bulletDir;

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    public void ReceiveDirection(Vector3 dir)
    {
        bulletDir = dir;
    }

    private void FixedUpdate()
    {
        transform.Translate(bulletDir * bulletSpeed * Time.fixedDeltaTime);
    }

}
