using DG.Tweening;
using System.Collections;
using UnityEngine;
public class ENEMY : MonoBehaviour, IDamageable, IGiveDamage
{
    public bool isAlive = true;
    public ENEMY_SO enemy_SO;
    public Transform currentPlayer;
    [Header("Sprite")]
    public SpriteRenderer enemySprite;
    [Header("HP")]
    [SerializeField] private float HP;
    [SerializeField] private bool canTakeDMG = true;
    [Header("VFX")]
    [SerializeField] private int bloodVFXAmount = 10;
    [SerializeField] private ParticleSystem[] bloodVFX_List;
    [SerializeField] public GameObject bloodSplatter_GO;
    [Header("SHAKE ANIMATION")]
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float strength = 1;
    [SerializeField] private int vibrato = 10;
    [SerializeField] private float randomness = 90;
    [Header("SFX")]
    [SerializeField] public AudioSource source;
    [SerializeField] public AudioClip coin_clip;
    [Header("DMG")]
    [SerializeField] private float dmg;
    public float DMG
    {
        get { return dmg; }
    }

    float IGiveDamage.Damage { get => DMG; }

    private void Awake()
    {
        HP = enemy_SO.m_HP;
        dmg = enemy_SO.m_DMG;
        bloodSplatter_GO.SetActive(false);
        InstantiateBloodVFX();
        FindClosestPlayer();
    }
    // ─── IDamageable ────────────────────────────────────────────────
    public void Damage(float dmg)
    {
        if (canTakeDMG)
            StartCoroutine(TakeDamage(dmg));
    }
    private IEnumerator TakeDamage(float dmg)
    {
        source.Play();
        canTakeDMG = false;
        HP -= dmg;
        PlayDmgAnimation();
        PlayBloodVFX();
        if (HP <= 0)
        {
            Die();
            yield break; // Encerra a coroutine imediatamente
        }
        yield return new WaitForSeconds(0.1f);
        canTakeDMG = true;
    }
    public virtual void Die()
    {
        dmg = 0;
        isAlive = false;
        bloodSplatter_GO.transform.SetParent(null);
        bloodSplatter_GO.SetActive(true);
    }
    // ─── VFX ────────────────────────────────────────────────────────
    private void InstantiateBloodVFX()
    {
        bloodVFX_List = new ParticleSystem[bloodVFXAmount];
        for (int i = 0; i < bloodVFXAmount; i++)
        {
            ParticleSystem vfx = Instantiate(enemy_SO.blood_VFX, transform);
            vfx.gameObject.SetActive(false);
            bloodVFX_List[i] = vfx;
        }
    }
    private void PlayDmgAnimation()
    {
        enemySprite.transform.DOShakePosition(duration, strength, vibrato, randomness).OnComplete(() =>
            enemySprite.transform.localPosition = Vector3.zero
        );
    }
    private void PlayBloodVFX()
    {
        ParticleSystem vfx = GetPooledBloodVFX();
        if (vfx == null) return;
        vfx.transform.SetParent(null);
        vfx.transform.position = transform.position;
        vfx.gameObject.SetActive(true);
    }
    private ParticleSystem GetPooledBloodVFX()
    {
        foreach (ParticleSystem vfx in bloodVFX_List)
        {
            if (!vfx.gameObject.activeInHierarchy)
                return vfx;
        }
        return null;
    }
    // ─── Player Targeting ───────────────────────────────────────────
    private void FindClosestPlayer()
    {
        PLAYER[] players = FindObjectsByType<PLAYER>(FindObjectsSortMode.None);
        if (players.Length == 0) return;
        float closestDist = float.MaxValue;
        foreach (PLAYER player in players)
        {
            float d = Vector3.Distance(transform.position, player.transform.position);
            if (d < closestDist)
            {
                closestDist = d;
                currentPlayer = player.transform;
            }
        }
    }


}