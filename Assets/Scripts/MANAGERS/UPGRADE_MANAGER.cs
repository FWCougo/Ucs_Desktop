using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class UPGRADE_MANAGER : MonoBehaviour
{
    [SerializeField] private PLAYER_PASSIVES p_Passives;
    [SerializeField] private TMP_Text screen_TXT;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip cantBuy_Clip;
    [SerializeField] private AudioClip couldBuy_Clip;

    [SerializeField] private int moneySpent = 0;

    IEnumerator ShowTXT(string _TXT, Color _c)
    {
        screen_TXT.text = _TXT;
        screen_TXT.color = _c;
        screen_TXT.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        screen_TXT.gameObject.SetActive(false);

    }

    #region REGENERATION

    [Header("REGENERATION")]
    [SerializeField] private int regenLV = 0;
    [SerializeField] WILLOFGOD_SCRIPT willOfGod;
    [SerializeField] private UPGRADE_OPTION regenOptions;
    [SerializeField] private PASSIVA_SO regen_SO;
    public void UpgradeRegenLV()
    {
        if (regenLV >= 5)
        {
            StartCoroutine(ShowTXT("UPGRADE NO MAXIMO", Color.yellow));
            source.PlayOneShot(cantBuy_Clip);
            CheckRegenLV();
            return;
        }

        int _currentCoins = GAME_MANAGER.Instance.GetCoins();
        int _neededCoins = regen_SO.preco[regenLV];

        if (_currentCoins < _neededCoins) {

            StartCoroutine(ShowTXT("MOEDAS INSUFICIENTES", Color.red));
            source.PlayOneShot(cantBuy_Clip);
            return; 
        }

        ChangeMoneySpent(_neededCoins);

        StartCoroutine(ShowTXT("COMPRA BEM SUCEDIDA", Color.green));
        source.PlayOneShot(couldBuy_Clip);

        GAME_MANAGER.Instance.ChangeCoins(-_neededCoins);

        regenLV++;
        CheckRegenLV();
        PlayerPrefs.SetInt("REGEN_LV_KEY", regenLV);
    }
    public void CheckRegenLV()
    {
        regenOptions.AumentarLV(regenLV);
        p_Passives.ReceberPassivaREGEN(willOfGod, regenLV);
        //  if(regenLV > 0)
        //  {
        //      p_Passives.ReceberPassivaREGEN(willOfGod, regenLV);
        //  }
    }

    #endregion

    #region HP
    [Header("+HP")]
    [SerializeField] private int hpLV = 0;
    [SerializeField] private UPGRADE_OPTION hpOptions;
    [SerializeField] private PASSIVA_SO hp_SO;
    [SerializeField] private HP_PASSIVA hp_PASSIVA;

    public void UpgradeHpLV()
    {
        if (hpLV >= 5)
        {
            StartCoroutine(ShowTXT("UPGRADE NO MAXIMO", Color.yellow));
            source.PlayOneShot(cantBuy_Clip);
            CheckHPLV();
            return;
        }

        int _currentCoins = GAME_MANAGER.Instance.GetCoins();
        int _neededCoins = hp_SO.preco[hpLV];

        if (_currentCoins < _neededCoins)
        {
            StartCoroutine(ShowTXT("MOEDAS INSUFICIENTES", Color.red));
            source.PlayOneShot(cantBuy_Clip);
            return;
        }

        ChangeMoneySpent(_neededCoins);

        StartCoroutine(ShowTXT("COMPRA BEM SUCEDIDA", Color.green));
        source.PlayOneShot(couldBuy_Clip);

        GAME_MANAGER.Instance.ChangeCoins(-_neededCoins);

        hpLV++;
        CheckHPLV();
        PlayerPrefs.SetInt("HP_LV_KEY", hpLV);
    }
    public void CheckHPLV()
    {
        hpOptions.AumentarLV(hpLV);
        p_Passives.ReceberPassivaHP(hp_PASSIVA, hpLV);
        //   if (hpLV > 0)
        //   {
        //       p_Passives.ReceberPassivaHP(hp_PASSIVA, hpLV);
        //   }
    }
    #endregion

    #region SPEED
    [Header("+SPEED")]
    [SerializeField] private int speedLV = 0;
    [SerializeField] private UPGRADE_OPTION speedOptions;
    [SerializeField] private PASSIVA_SO speed_SO;
    [SerializeField] private SPEED_PASSIVA speed_PASSIVA;

    public void UpgradeSpeedLV()
    {
        if (speedLV >= 5)
        {
            StartCoroutine(ShowTXT("UPGRADE NO MAXIMO", Color.yellow));
            source.PlayOneShot(cantBuy_Clip);
            CheckSpeedLV();
            return;
        }


        int _currentCoins = GAME_MANAGER.Instance.GetCoins();
        int _neededCoins = speed_SO.preco[speedLV];

        if (_currentCoins < _neededCoins)
        {
            StartCoroutine(ShowTXT("MOEDAS INSUFICIENTES", Color.red));
            source.PlayOneShot(cantBuy_Clip);
            return;
        }

        ChangeMoneySpent(_neededCoins);

        StartCoroutine(ShowTXT("COMPRA BEM SUCEDIDA", Color.green));
        source.PlayOneShot(couldBuy_Clip);

        GAME_MANAGER.Instance.ChangeCoins(-_neededCoins);

        speedLV++;
        CheckSpeedLV();
        PlayerPrefs.SetInt("SPEED_LV_KEY", speedLV);
    }
    public void CheckSpeedLV()
    {
        speedOptions.AumentarLV(speedLV);
        p_Passives.ReceberPassivaSPEED(speed_PASSIVA, speedLV);
       // if (speedLV > 0)
       // {
       //     p_Passives.ReceberPassivaSPEED(speed_PASSIVA, speedLV);
       // }
    }
    #endregion

    #region DMG
    [Header("+DMG")]
    [SerializeField] private int dmgLV = 0;
    [SerializeField] private UPGRADE_OPTION dmgOptions;
    [SerializeField] private PASSIVA_SO dmg_SO;
    [SerializeField] private DMG_PASSIVA dmg_PASSIVA;

    public void UpgradeDmgLV()
    {
        if(dmgLV >= 5)
        {
            StartCoroutine(ShowTXT("UPGRADE NO MAXIMO", Color.yellow));
            source.PlayOneShot(cantBuy_Clip);
            CheckDmgLV();
            return;
        }

        int _currentCoins = GAME_MANAGER.Instance.GetCoins();
        int _neededCoins = speed_SO.preco[dmgLV];

        if (_currentCoins < _neededCoins)
        {
            StartCoroutine(ShowTXT("MOEDAS INSUFICIENTES", Color.red));
            source.PlayOneShot(cantBuy_Clip);
            return;
        }

        ChangeMoneySpent(_neededCoins);

        StartCoroutine(ShowTXT("COMPRA BEM SUCEDIDA", Color.green));
        source.PlayOneShot(couldBuy_Clip);

        GAME_MANAGER.Instance.ChangeCoins(-_neededCoins);

        dmgLV++;
        CheckDmgLV();
        PlayerPrefs.SetInt("DMG_LV_KEY", dmgLV);
    }
    public void CheckDmgLV()
    {
        dmgOptions.AumentarLV(dmgLV);
        p_Passives.ReceberPassivaDMG(dmg_PASSIVA, dmgLV);
       // if (dmgLV > 0)
       // {
       //     p_Passives.ReceberPassivaDMG(dmg_PASSIVA, dmgLV);
       // }
    }
    #endregion



    private void ChangeMoneySpent(int _i)
    {
        moneySpent += _i;

        PlayerPrefs.SetInt("MONEY_SPENT_KEY", moneySpent);
    }

    [ContextMenu("RESETAR_NIVEIS")]
    public void ResetUpgrades()
    {
        if(moneySpent > 0)
        {
            StartCoroutine(ShowTXT("REEMBOLSO REALIZADO", Color.green));
            source.PlayOneShot(couldBuy_Clip);
        }
        else
        {
            StartCoroutine(ShowTXT("NADA PARA REEMBOLSAR", Color.red));
            source.PlayOneShot(cantBuy_Clip);
        }

        PlayerPrefs.SetInt("REGEN_LV_KEY",0);
        PlayerPrefs.SetInt("HP_LV_KEY",0);
        PlayerPrefs.SetInt("SPEED_LV_KEY",0);
        PlayerPrefs.SetInt("DMG_LV_KEY",0);

        GAME_MANAGER.Instance.ChangeCoins(moneySpent);

        regenLV = 0;
        hpLV = 0;
        speedLV = 0;
        dmgLV = 0;
        moneySpent = 0;

        CheckRegenLV();
        CheckHPLV();
        CheckDmgLV();
        CheckSpeedLV();

        PlayerPrefs.SetInt("MONEY_SPENT_KEY", 0);
    }

    [ContextMenu("RESETAR_NIVEIS")]
    public void ResetGame()
    {
        PlayerPrefs.SetInt("REGEN_LV_KEY", 0);
        PlayerPrefs.SetInt("HP_LV_KEY", 0);
        PlayerPrefs.SetInt("SPEED_LV_KEY", 0);
        PlayerPrefs.SetInt("DMG_LV_KEY", 0);
        PlayerPrefs.SetInt("MONEY_SPENT_KEY", 0);

        regenLV = 0;
        hpLV = 0;
        speedLV = 0;
        dmgLV = 0;
        moneySpent = 0;

        CheckRegenLV();
        CheckHPLV();
        CheckDmgLV();
        CheckSpeedLV();

        GAME_MANAGER.Instance.ResetCoins();
    }

    private void Start()
    {
        screen_TXT.gameObject.SetActive(false);

        regenLV = PlayerPrefs.GetInt("REGEN_LV_KEY");
        hpLV = PlayerPrefs.GetInt("HP_LV_KEY");
        speedLV = PlayerPrefs.GetInt("SPEED_LV_KEY");
        dmgLV = PlayerPrefs.GetInt("DMG_LV_KEY");
        moneySpent = PlayerPrefs.GetInt("MONEY_SPENT_KEY");

        CheckRegenLV();
        CheckHPLV();
        CheckSpeedLV();
        CheckDmgLV();
    }

}
