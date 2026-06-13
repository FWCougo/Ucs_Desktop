using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LITTLEEYE_ENEMY : ENEMY
{
    [SerializeField] private Sprite[] phasesSprites;
    [SerializeField] private Sprite[] flyingSprites;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.updateRotation = false;

        Vector3 pos = transform.position;
        float yValue = pos.y + 2;

        transform.DOMoveY(yValue, 1).OnComplete(() =>
        {
            transform.DOMoveY(0,2).SetEase(Ease.InSine);
        });

        float zValue = pos.z - Random.Range(5,6);

        transform.DOMoveZ(zValue, 3);

        float xValue = pos.x + Random.Range(-3f,3f);
        transform.DOMoveX(xValue, 3).OnComplete(()=>
            StartCoroutine(SairDoCasulo())
        );
    }

    private void FixedUpdate()
    {
        if (isAlive && isMoving)
        {
            agent.SetDestination(currentPlayer.position);
        }
    }

    IEnumerator SairDoCasulo()
    {
        bool isChanging = true;
        float shakeDuration = 1.5f;

        yield return new WaitForSeconds(shakeDuration);

        while (isChanging)
        {
            for (int i = 1; i < phasesSprites.Length; i++)
            {
                enemySprite.sprite = phasesSprites[i];
                enemySprite.transform.DOShakeScale(shakeDuration, 2, 10, 90);
                shadowSprite.sprite = phasesSprites[i];
                shadowSprite.transform.DOShakeScale(shakeDuration, 2, 10, 90);
                yield return new WaitForSeconds(shakeDuration);
            }

            isChanging = false;
        }
        
        StartCoroutine(FlyingAnimation());
    }

    IEnumerator FlyingAnimation()
    {
        isMoving = true;
        enemySprite.transform.DOLocalMoveY(3.5f, 1f);
        enemySprite.transform.DOLocalMoveZ(1, 1f);

        float timeBetweenSprites = 0.3f;

        while (isAlive)
        {
            for (int i = 1; i < flyingSprites.Length; i++)
            {
                enemySprite.sprite = flyingSprites[i];
                shadowSprite.sprite = flyingSprites[i];

                yield return new WaitForSeconds(timeBetweenSprites);
            }
        }

    }

}
