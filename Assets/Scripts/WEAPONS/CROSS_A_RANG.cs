using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CROSS_A_RANG : WEAPON
{
    [Header("References")]
    [SerializeField] private Transform playerHand_Trans;
    [SerializeField] private BOOMERANG_SO boomerang_SO;
    [SerializeField] private SpriteRenderer g_Sprite;
    [SerializeField] private TrailRenderer[] trails;

    [Header("Settings")]
    [SerializeField] private Vector3 direction = Vector3.left;
    [SerializeField] private float trailFadeOutDuration = 0.4f;

    public bool canShoot = true;

    private bool isMoving = false;

    private bool isReturning;
    private Tween rotationTween;
    private float returnSpeed;
    private float[] trailOriginalTimes;

    [Header("Physics")]
    private float hitSphereRadius = 1f;

    [Header("SFX")]
    [SerializeField]
    private AudioSource source;

    private void Start()
    {
        playerHand_Trans = GetComponentInParent<PLAYER_WEAPONS>().GetHand();

        // Cache trail times uma única vez
        trailOriginalTimes = new float[trails.Length];
        for (int i = 0; i < trails.Length; i++)
            trailOriginalTimes[i] = trails[i].time;
    }

    private void OnEnable()
    {
        if(playerHand_Trans == null)
        {
            playerHand_Trans = GetComponentInParent<PLAYER>().transform;
        }

        OnBoomerangReturned();
    }

    private void Update()
    {
        if (!isReturning) return;

        transform.position = Vector3.MoveTowards(transform.position, playerHand_Trans.position, returnSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, playerHand_Trans.position) < 0.2f)
            OnBoomerangReturned();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hitSphereRadius);
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            Collider[] cols;

            cols = Physics.OverlapSphere(transform.position, hitSphereRadius);

            for (int i = 0;i < cols.Length; i++)
            { 
                IDamageable damageable = cols[i].gameObject.GetComponentInParent<IDamageable>();

                if (damageable != null)
                {
                    float totalDMG = boomerang_SO.damage + GAME_MANAGER.Instance.GetExtraDMG();
                    damageable.Damage(totalDMG);
                }
            }

        }
    }

    public override void flipWeapon(bool flipped)
    {
        
    }

    public override void changeLayer(int layer)
    {
        g_Sprite.sortingOrder = layer;
    }

    public override void UseWeapon(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            direction.x = dir.x;
            direction.z = dir.y;
        }

        if (canShoot)
            StartCoroutine(ShootCoroutine());
    }

    private void OnBoomerangReturned()
    {
        isMoving = false;
        isReturning = false;
        canShoot = true;

        rotationTween.Kill();
        transform.SetParent(playerHand_Trans);
        transform.localPosition = Vector3.up;
        g_Sprite.transform.rotation = Quaternion.Euler(50f, 0f, 0f);

        source.Stop();
    }

    private IEnumerator ShootCoroutine()
    {
        source.Play();

        isMoving = true;

        canShoot = false;
        isReturning = false;

        // Cache do speed para não recalcular no Update
        returnSpeed = boomerang_SO.b_Range / boomerang_SO.b_LifeSpan * 2f;

        transform.SetParent(null);
        SetTrailsActive(true);
        RotateBoomerang();

        Vector3 targetPos = transform.position + direction * boomerang_SO.b_Range;
        float halfLife = boomerang_SO.b_LifeSpan * 0.5f;

        transform.DOMove(targetPos, halfLife).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(halfLife);

        StartCoroutine(FadeOutTrails());
        isReturning = true;
    }

    private void RotateBoomerang()
    {
        rotationTween?.Kill();
        Vector3 rot = g_Sprite.transform.eulerAngles;
        rot.z += 180f;
        rotationTween = g_Sprite.transform
            .DOLocalRotate(rot, 0.25f, RotateMode.Fast)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    private void SetTrailsActive(bool value)
    {
        foreach (TrailRenderer tr in trails)
            tr.gameObject.SetActive(value);
    }

    private IEnumerator FadeOutTrails()
    {
        float elapsed = 0f;

        while (elapsed < trailFadeOutDuration)
        {
            elapsed += Time.deltaTime;
            float t = 1f - Mathf.Clamp01(elapsed / trailFadeOutDuration);

            for (int i = 0; i < trails.Length; i++)
                trails[i].time = trailOriginalTimes[i] * t;

            yield return null;
        }

        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].time = trailOriginalTimes[i];
            trails[i].gameObject.SetActive(false);
        }
    }
}