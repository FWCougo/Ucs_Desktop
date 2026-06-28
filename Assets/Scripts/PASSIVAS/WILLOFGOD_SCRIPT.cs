using DG.Tweening;
using System.Collections;
using UnityEngine;

public class WILLOFGOD_SCRIPT : PASSIVA
{
    [SerializeField] float healthAmout = 10;
    [SerializeField] float waitTillRegen = 25f;

    private void Start()
    {
        CheckLevel();
        StartCoroutine(Regenerar());
    }

    public void CheckLevel()
    {
        switch(nivelPassiva)
        {
            case 1:
                healthAmout = 10;
                break;

            case 2:
                healthAmout = 15;
                break;

            case 3:
                healthAmout = 20;
                break;
            
            case 4:
                healthAmout = 25;
                break;

            case 5:
                healthAmout = 30;
                break;
        }
    }

    IEnumerator Regenerar()
    {
        while (playerManager.PLAYER.isAlive)
        {
            yield return new WaitForSeconds(waitTillRegen);
            
            playerManager.PLAYER.ReceiveHealth(healthAmout);

            passivaSprite.DOFade(1f, 1);

            passivaSprite.transform.DOScale(6f, 1.5f).OnComplete(() =>
            {
                passivaSprite.transform.DOScale(5f, 1.5f);
            });

           passivaSprite.transform.DOLocalRotate(new Vector3(90, 360, 0),2, RotateMode.FastBeyond360).SetEase(Ease.InOutSine);

            yield return new WaitForSeconds(1);

            passivaSprite.DOFade(0f, 1);
        }        
    }
}
