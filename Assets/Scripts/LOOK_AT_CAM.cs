using UnityEngine;

public class LOOK_AT_CAM : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private bool x, y, z;
    [SerializeField] private Vector3 dir;

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void OnDrawGizmos()
    {
        Rotate();
    }

    void Rotate()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position,Vector3.right);


        
        if (!x)
        {
            rotation.x = dir.x;
        }
        if (!y)
        {
            rotation.y = dir.y;
        }
        if (!z)
        {
            rotation.z = dir.z;
        }
        

        transform.rotation = rotation;
    }
}
