using DG.Tweening;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class PLAYER_WEAPONS : MonoBehaviour
{
    [SerializeField]
    private Transform aim_Trans;

    [Header("WEAPONS REFS")]
    [SerializeField]
    private WEAPON mainGun;
    [SerializeField]
    private WEAPON secondGun;
    [SerializeField]
    private WEAPON currentGun;

    [Header("HAND REFERENCES")]
    [SerializeField] private Transform handTransform;

    [Header("AMMO")]
    [SerializeField] private GameObject ammoUIConteiner;
    [SerializeField] private Image ammoFillImg;

    [Header("SFX")]
    [SerializeField] private AudioSource combatASource;
    [SerializeField] private AudioClip gettingGunClip;


    private void Start()
    {
        currentGun = mainGun;
    }
    public Transform GetHand()
    {
        return handTransform;
    }
    public void SetMainWeapon(WEAPON _w)
    {
        if (mainGun == _w) { return; }
        ammoUIConteiner.SetActive(false);
        WEAPON _weapon = Instantiate(_w,handTransform);
        _weapon.transform.localPosition = Vector3.zero;

        mainGun =_weapon;
        currentGun = mainGun;

        currentGun.isMainWeapon = true;

    }
    public bool SetSecondWeapon(WEAPON _w)
    {
        if(secondGun == _w) { return false; }

        combatASource.pitch = 2;
        combatASource.PlayOneShot(gettingGunClip);

        ammoUIConteiner.SetActive(true);
        ammoFillImg.fillAmount = 1;

        currentGun.gameObject.SetActive(false);

        WEAPON _weapon = Instantiate(_w, handTransform);
        _weapon.transform.localPosition = Vector3.zero;

        secondGun = _weapon;
        currentGun = secondGun;

        return true;
    }
    public void ChangeBackToMain()
    {
        ammoUIConteiner.SetActive(false);
        currentGun.gameObject.SetActive(false);
        currentGun = mainGun;
        currentGun.gameObject.SetActive(true);
    }
    public void ReduceAmmo(float _currentSecondAmmo)
    {
        ammoFillImg.fillAmount = _currentSecondAmmo;
    }
    public void UseWeapon(Vector2 _mouseDir)
    {
        if(currentGun == null) { return; }
        AnimateAim();
        currentGun.UseWeapon(_mouseDir);
    }
    private void AnimateAim()
    {
        aim_Trans.DOScale(3, 0.25f).OnComplete(() =>
        {
            aim_Trans.DOScale(5, 0.25f);
        });
    }
    public void FlipGun(bool _flipped)
    {
        if (currentGun == null) { return; }
        currentGun.flipWeapon(_flipped);
    }
    public void ChangeLayer(int _layer)
    {
        if (currentGun == null) { return; }
        currentGun.changeLayer(_layer);
    }


}
