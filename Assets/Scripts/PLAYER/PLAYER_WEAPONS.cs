using UnityEngine;

public class PLAYER_WEAPONS : MonoBehaviour
{
    [Header("WEAPONS REFS")]
    [SerializeField]
    private WEAPON mainGun;
    [SerializeField]
    private WEAPON secondGun;
    [SerializeField]
    private WEAPON currentGun;

    [Header("HAND REFERENCES")]
    [SerializeField] private Transform handTransform;

    private void Start()
    {
        currentGun = mainGun;
    }
    public void SetMainWeapon(WEAPON _w)
    {
        WEAPON _weapon = Instantiate(_w,handTransform);
        _weapon.transform.localPosition = Vector3.zero;

        mainGun =_weapon;
        currentGun = mainGun;

    }
    public void ChangeWeapon(WEAPON _w)
    {
        currentGun = _w;             
    }

    public void UseWeapon(Vector2 _mouseDir)
    {
        currentGun.UseWeapon(_mouseDir);
    }
}
