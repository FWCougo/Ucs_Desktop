using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class STALKER_ENEMY : ENEMY
{
    [SerializeField]
    private float rotationSpeed = 0.5f;

    [SerializeField]
    private NavMeshAgent agent;

    private void Start()
    {
        Vector3 rot = enemySprite.transform.rotation.eulerAngles;
        rot.y += 20;
        enemySprite.transform.DOLocalRotate(rot, rotationSpeed, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo);

        agent.updateRotation = false;
        agent.speed = enemy_SO.m_SPEED;
    }

    private void FixedUpdate()
    {
        agent.SetDestination(currentPlayer.position);

       // Vector3 direction = currentPlayer.position - transform.position;
       //
       // direction.y = 0f;
       //
       // transform.Translate(direction * enemy_SO.m_SPEED * Time.fixedDeltaTime);
    }
}
