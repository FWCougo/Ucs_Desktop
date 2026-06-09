using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PLAYER : MonoBehaviour
{
    [SerializeField] private SpriteRenderer p_sprite;

    [Header("Hitbox")]
    [SerializeField] private Vector3 hitboxCenter = Vector3.zero;
    [SerializeField] private Vector3 hitboxSize = Vector3.one;

    [Header("HP")]
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float hp;
    [SerializeField] private Image hpIMG;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private bool canTakeDMG = true;

    public float HP => hp;

    public bool isAlive = true;

    [Header("Audio")]
    [SerializeField] private AudioSource combatASource;
    [SerializeField] private AudioClip[] dmgAClips;

    

    private void Start()
    {
        hp = maxHp;
        isAlive = true;
    }

    // ─── Setter ─────────────────────────────────────────────────────
    public void ChangeMaxHP(float _maxHP)
    {
        maxHp = _maxHP;
        hp = maxHp;

        UpdateLifeIMG();
    }

    public void ReceiveHealth(float _hp)
    {
        hp += _hp;

        if(hp >= maxHp)
        {
            hp = maxHp;
        }

        UpdateLifeIMG();

        StartCoroutine(FlashHEAL());
    }

    // --- Update life IMG ----------------------------------
    private void UpdateLifeIMG()
    {
        hpIMG.fillAmount = hp / maxHp;
    }

    private IEnumerator FlashHEAL()
    {
        p_sprite.color = Color.limeGreen;
        yield return new WaitForSeconds(0.15f);
        p_sprite.color = Color.white;
    }

    // ─── Physics ─────────────────────────────────────────────────────

    private void FixedUpdate()
    {
        CheckDamageCollisions();
    }

    private void CheckDamageCollisions()
    {
        if (!canTakeDMG) return;

        Collider[] hits = Physics.OverlapBox(
        transform.position + hitboxCenter,
        hitboxSize * 0.5f,
        transform.rotation
        );

        if (hits.Length == 0) return;

        // Pega o primeiro inimigo válido encontrado
        foreach (Collider hit in hits)
        {
            IGiveDamage giveDMG = hit.GetComponentInParent<IGiveDamage>();
            if(giveDMG == null) continue;

            float _DMG = giveDMG.Damage;
            if (_DMG == 0) continue;

            StartCoroutine(TakeDMG(_DMG));
            
            break; // Um dano por frame é suficiente
        }
    }

    private IEnumerator TakeDMG(float dmg)
    {
        canTakeDMG = false;

        hp = Mathf.Max(hp - dmg, 0f);
        UpdateLifeIMG();

        CAMERA_SHAKE.Instance.ShakeMedium();
        combatASource.pitch = 1;
        combatASource.PlayOneShot(dmgAClips[Random.Range(0, dmgAClips.Length)]);
        StartCoroutine(FlashDMG());

        if (hp <= 0f)
        {
            Die();
            yield break;
        }

        yield return new WaitForSeconds(invincibilityDuration);
        canTakeDMG = true;
    }

    private IEnumerator FlashDMG()
    {
        p_sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        p_sprite.color = Color.white;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        isAlive = false;
        GAME_MANAGER.Instance.GAMEOVER();
    }


#if UNITY_EDITOR
    [SerializeField]
    private bool DrawHitBox = false;

    private void OnDrawGizmosSelected()
    {
        if (!DrawHitBox) return;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = canTakeDMG ? new Color(0f, 1f, 0f, 0.3f) : new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawCube(hitboxCenter, hitboxSize);
        Gizmos.color = canTakeDMG ? Color.green : Color.red;
        Gizmos.DrawWireCube(hitboxCenter, hitboxSize);
    }
#endif
}