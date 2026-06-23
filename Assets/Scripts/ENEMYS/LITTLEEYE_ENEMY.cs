using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LITTLEEYE_ENEMY : ENEMY
{
    [SerializeField] private Transform spriteParent;
    [SerializeField] private Sprite[] phasesSprites;
    [SerializeField] private Sprite[] flyingSprites;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.speed = enemy_SO.m_SPEED;
        agent.enabled = false;
        agent.updateRotation = false;

        Vector3 pos = transform.position;
        float yValue = pos.y + 2;

        transform.DOMoveY(yValue, 0.5f).OnComplete(() =>
        {
            transform.DOMoveY(0, 1.5f).SetEase(Ease.InSine);
        });

        float zValue = pos.z - Random.Range(5,6);

        transform.DOMoveZ(zValue, 2);

        float xValue = pos.x + Random.Range(-5f,5f);
        transform.DOMoveX(xValue, 2).OnComplete(()=>
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
        agent.enabled = true;

        bool isChanging = true;
        float shakeDuration = 0.75f;

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
        spriteParent.transform.DOLocalMoveY(3.5f, 1f);
        spriteParent.transform.DOLocalMoveZ(1, 1f);

        float timeBetweenSprites = 0.3f;

        while (isAlive)
        {
            for (int i = 0; i < flyingSprites.Length; i++)
            {
                enemySprite.sprite = flyingSprites[i];
                shadowSprite.sprite = flyingSprites[i];

                yield return new WaitForSeconds(timeBetweenSprites);
            }
        }

    }

    public override void Die()
    {
        base.Die();

        if (agent.isOnNavMesh)
        {
            agent.speed = 0;
            agent.SetDestination(transform.position);
            agent.isStopped = true;
        }        
        Vector3 rot = enemySprite.transform.rotation.eulerAngles;
        rot.x = 89;
        enemySprite.transform.DOLocalRotate(rot, 1f, RotateMode.Fast).OnComplete(() =>
        {
            enemySprite.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
        });

        GAME_MANAGER.Instance.IncreaseKillCount();
    }

}
