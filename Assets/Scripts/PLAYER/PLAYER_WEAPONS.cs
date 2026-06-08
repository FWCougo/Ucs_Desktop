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
        if (mainGun == _w) { return; }

        WEAPON _weapon = Instantiate(_w,handTransform);
        _weapon.transform.localPosition = Vector3.zero;

        mainGun =_weapon;
        currentGun = mainGun;

    }
    public bool SetSecondWeapon(WEAPON _w)
    {
        if(secondGun == _w) { return false; }

        currentGun.gameObject.SetActive(false);

        WEAPON _weapon = Instantiate(_w, handTransform);
        _weapon.transform.localPosition = Vector3.zero;

        secondGun = _weapon;
        currentGun = secondGun;

        return true;
    }
    public void ChangeBackToMain()
    {
        currentGun.gameObject.SetActive(false);
        currentGun = mainGun;
        currentGun.gameObject.SetActive(true);
    }

    public void UseWeapon(Vector2 _mouseDir)
    {
        currentGun.UseWeapon(_mouseDir);
    }
}
