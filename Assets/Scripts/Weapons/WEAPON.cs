using UnityEngine;

public abstract class WEAPON : MonoBehaviour
{
    public bool isMainWeapon = false;

    public abstract void UseWeapon(Vector3 dir);
    
}
