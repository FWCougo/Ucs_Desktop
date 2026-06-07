using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class SLIME_ENEMY : ENEMY
{
    [SerializeField]
    private float rotationSpeed = 0.5f;
    [SerializeField]
    private NavMeshAgent agent;
    //[SerializeField]
    //private Transform enemySpriteParent;

    [Header("SHOOT")]
    [SerializeField]
    private float cadency = 1.0f;
    [SerializeField][Tooltip("0 até 100")]
    private float shotProbability = 50f; 
    [SerializeField] private int bulletAmount = 10;
    [SerializeField] private BULLET[] bullet_List;
    [SerializeField] private BULLET bulletPrefab;

    [Header("JUMP")]
    [SerializeField]
    private bool isJumping = false;

    [SerializeField]
    private float jumpHeight = 3;
    [SerializeField]
    private float jumpDuration = 3;

    private void Start()
    {        
        agent.updateRotation = false;
        agent.speed = enemy_SO.m_SPEED;

        InstantiateBullets();

        StartCoroutine(SlimeMovement());

        

    }

    private void InstantiateBullets()
    {
        bullet_List = new BULLET[bulletAmount];
        for (int i = 0; i < bulletAmount; i++)
        {
            BULLET _bulltet = Instantiate(bulletPrefab, transform);
            _bulltet.gameObject.SetActive(false);
            bullet_List[i] = _bulltet;
        }
    }

    public Vector3 RandomizeDirection(Vector3 direction, float spreadAngle)
    {
        // Converte a direção para ângulo em graus
        float baseAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        // Adiciona um offset aleatório dentro do cone de dispersão
        float randomOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

        float finalAngle = (baseAngle + randomOffset) * Mathf.Deg2Rad;

        // Converte de volta para vetor
        return new Vector3(Mathf.Cos(finalAngle),direction.y , Mathf.Sin(finalAngle));
    }

    private BULLET GetPooledBullets()
    {
        foreach (BULLET _bullet in bullet_List)
        {
            if (!_bullet.gameObject.activeInHierarchy)
                return _bullet;
        }
        return null;
    }

    IEnumerator Shoot()
    {
        //TENTA ATIRAR
        int _chosen = Random.Range(0,100);
        if(_chosen <= shotProbability)
        {
            //ATIRA
            BULLET _bullet = GetPooledBullets();
            if (_bullet == null) yield return null;
            _bullet.transform.SetParent(null);
            _bullet.transform.position = transform.position;

            Vector3 shotDir = (currentPlayer.transform.position - transform.position).normalized;
            shotDir = RandomizeDirection(shotDir, 10);

            _bullet.gameObject.SetActive(true);
            _bullet.ReceiveDirection(shotDir, 1.5f);
        }

        yield return null;
    }

    IEnumerator SlimeMovement()
    {
        // Captura UMA vez — estes valores não mudam
        float normalYPos = enemySprite.transform.localPosition.y;
        float normalSize = enemySprite.transform.localScale.y;
        float smallSize = normalSize * 0.3f;

        while (isAlive)
        {
            // ── Fase 1: Encolher e definir destino ──────────────────
            agent.isStopped = true;

            StartCoroutine(Shoot());

            yield return enemySprite.transform
                .DOScaleY(smallSize, 1f)
                .WaitForCompletion();

            agent.SetDestination(currentPlayer.position);
            agent.isStopped = false;

            // ── Fase 2: Expandir + Subir (simultâneo) ───────────────
            enemySprite.transform.DOScaleY(normalSize, 0.25f);

            yield return enemySprite.transform
                .DOLocalMoveY(normalYPos + jumpHeight, jumpDuration * 0.5f)
                .SetEase(Ease.OutQuad)
                .WaitForCompletion();

            // ── Fase 3: Descer ───────────────────────────────────────
            yield return enemySprite.transform
                .DOLocalMoveY(normalYPos, jumpDuration * 0.5f)
                .SetEase(Ease.InQuad)
                .WaitForCompletion();
        }
    }

    public override void Die()
    {

        source.PlayOneShot(coin_clip);
        

        agent.isStopped = true;
        isAlive = false;

        bloodSplatter_GO.transform.SetParent(null);
        bloodSplatter_GO.SetActive(true);

        Vector3 rot = enemySprite.transform.rotation.eulerAngles;
        rot.x = 89;
        enemySprite.transform.DOLocalRotate(rot, 1f, RotateMode.Fast).OnComplete(() =>
        {
            enemySprite.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
        });

        GAME_MANAGER.Instance.IncreaseKillCount();
    }
}