using System.Collections;
using UnityEngine;

public class PICKUP_COIN : PICKUP
{
    [SerializeField] AudioSource source;
    [SerializeField] private int coinValue = 1;
    public override void PICKED_UP()
    {
        GAME_MANAGER.Instance.ChangeCoins(coinValue);
        source.Play();

        StartCoroutine(Disable());
        
    }

    IEnumerator Disable()
    {
        col.enabled = false;
        spriteTrans.gameObject.SetActive(false);
        spriteShadowTrans.gameObject.SetActive(false);

        while (source.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

}
