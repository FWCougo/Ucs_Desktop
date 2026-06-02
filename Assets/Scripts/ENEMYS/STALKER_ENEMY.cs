using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class STALKER_ENEMY : ENEMY
{
    [SerializeField]
    private float speedRange = 1f;
    [SerializeField]
    private float rotationSpeed = 0.5f;
    [SerializeField]
    private NavMeshAgent agent;
    Tween walkTween;
    private void Start()
    {
        Vector3 rot = enemySprite.transform.rotation.eulerAngles;
        rot.y += 20;
        walkTween = enemySprite.transform.DOLocalRotate(rot, rotationSpeed, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo);
        agent.updateRotation = false;
        agent.speed = enemy_SO.m_SPEED + Random.Range(-speedRange, speedRange);
    }

    public override void Die()
    {
        source.PlayOneShot(coin_clip);

        agent.speed = 0;
        agent.SetDestination(transform.position);
        isAlive = false;
        bloodSplatter_GO.transform.SetParent(null);
        bloodSplatter_GO.SetActive(true);
        walkTween.Kill();
        Vector3 rot = enemySprite.transform.rotation.eulerAngles;
        rot.x = 89;
        enemySprite.transform.DOLocalRotate(rot, 1f, RotateMode.Fast).OnComplete(() =>
        {
            enemySprite.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
        });

        GAME_MANAGER.Instance.IncreaseKillCount();
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            agent.SetDestination(currentPlayer.position);
        }
    }
}