using System.Collections.Generic;
using UnityEngine;

public class PLAYER_PASSIVES : MonoBehaviour
{

    [SerializeField] private Transform passiva_Container;

    [Header("REGENERATION")]
    [SerializeField] private int regenLV = 0;
    [SerializeField] WILLOFGOD_SCRIPT willOfGod;

    [Header("+HP")]
    [SerializeField] private int hpLV = 0;
    [SerializeField] private HP_PASSIVA hp_Passiva;

    [Header("+SPEED")]
    [SerializeField] private int speedLV = 0;
    [SerializeField] private SPEED_PASSIVA speed_Passiva;

    [Header("+DMG")]
    [SerializeField] private int dmgLV = 0;
    [SerializeField] private DMG_PASSIVA dmg_Passiva;


    #region RECEBER PASSIVA
    public void ReceberPassivaREGEN(WILLOFGOD_SCRIPT _p, int _pLV)
    {
        willOfGod = _p;
        regenLV = _pLV;
    }
    public void ReceberPassivaHP(HP_PASSIVA _p, int _pLV)
    {
        hp_Passiva = _p;
        hpLV = _pLV;
    }
    public void ReceberPassivaSPEED(SPEED_PASSIVA _p, int _pLV)
    {
        speed_Passiva = _p;
        speedLV = _pLV;
    }
    public void ReceberPassivaDMG(DMG_PASSIVA _p, int _pLV)
    {
        dmg_Passiva = _p;
        dmgLV = _pLV;
    }
    #endregion


    #region SPAWNAR PASSIVAS

    public void InstanciarPassivas()
    {
        if(regenLV > 0)
        {
            WILLOFGOD_SCRIPT _will = Instantiate(willOfGod,passiva_Container);
            _will.transform.localPosition = Vector3.zero;
            _will.SetNivel(regenLV);
        }
        if (hpLV > 0)
        {
            HP_PASSIVA _hp = Instantiate(hp_Passiva, passiva_Container);
            _hp.transform.localPosition = Vector3.zero;
            _hp.SetNivel(hpLV);
        }
        if (speedLV > 0)
        {
            SPEED_PASSIVA _speed = Instantiate(speed_Passiva, passiva_Container);
            _speed.transform.localPosition = Vector3.zero;
            _speed.SetNivel(speedLV);
        }
        if (dmgLV > 0)
        {
            DMG_PASSIVA _speed = Instantiate(dmg_Passiva, passiva_Container);
            _speed.transform.localPosition = Vector3.zero;
            _speed.SetNivel(speedLV);
        }
    }

    #endregion
}
