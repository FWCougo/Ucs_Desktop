using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EYESPAWNER_ENEMY : ENEMY
{
    [SerializeField] private Sprite lookUp;
    [SerializeField] private Sprite lookFront;
    [SerializeField] private Sprite lookSide;

    [SerializeField] private float waitTillNextPose = 1f;

    [SerializeField] private Transform spawnLittleEye;
    [SerializeField] private LITTLEEYE_ENEMY littleEye_prefab;

    private void Start()
    {
        enemySprite.sprite = lookUp;

        Vector3 pos = transform.position;
        pos.y = -3;
        transform.position = pos;

        transform.DOMoveY(0, 3f).OnComplete(() =>
        {
            StartCoroutine(DoStuff());

        });
    }

    public override void Die()
    {
        base.Die();

        Vector3 rot = enemySprite.transform.rotation.eulerAngles;
        rot.x = 89;
        enemySprite.transform.DOLocalRotate(rot, 1f, RotateMode.Fast).OnComplete(() =>
        {
            enemySprite.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
        });

        GAME_MANAGER.Instance.IncreaseKillCount();
    }

    IEnumerator DoStuff()
    {
        bool doingStuff = true;

        while (doingStuff)
        {
            //Olha pra Esquerda
            enemySprite.flipX = false;
            enemySprite.sprite = lookSide;
            WaitForSeconds await = new WaitForSeconds(Random.Range(waitTillNextPose, waitTillNextPose + 1));
            yield return await;

            //Olha pra Frente
            enemySprite.sprite = lookFront;
            await = new WaitForSeconds(Random.Range(waitTillNextPose, waitTillNextPose + 1));
            yield return await;

            //Olha pra Direita
            enemySprite.flipX = true;
            enemySprite.sprite = lookSide;
            await = new WaitForSeconds(Random.Range(waitTillNextPose, waitTillNextPose + 1));
            yield return await;

            int chanceToSpawn = Random.Range(1, 3);

            if (chanceToSpawn == 1)
            {
                SpawnLittleEye();
                doingStuff = false;
            }
            else
            {
                doingStuff = true;
            }

        }

    }

    void SpawnLittleEye()
    {
        enemySprite.sprite = lookUp;

        enemySprite.transform.DOShakeScale(3f, 1, 10, 90).OnComplete(() =>
        {
            LITTLEEYE_ENEMY _littleEye = Instantiate(littleEye_prefab, spawnLittleEye.position, Quaternion.identity);
            _littleEye.enemySprite.sortingOrder = 2;
            GAME_MANAGER.Instance.enemyCount++;
            if (isAlive)
            {
                StartCoroutine(DoStuff());
            }
            
        }
        );


    }

}
