using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ENEMY : MonoBehaviour, IDamageable
{
    public bool isAlive = true;

    public SpriteRenderer enemySprite;

    public ENEMY_SO enemy_SO;
    public PLAYER[] playersTransform;
    public Transform currentPlayer;
    
    private float dist = 1000;

    [Header("HP and DMG")]
    [SerializeField]
    private float HP;
    [SerializeField]
    private bool canTakeDMG = true;


    [Header("VFX")]
    [SerializeField]
    private int bloodVFXAmount = 10;
    [SerializeField]
    private ParticleSystem[] bloodVFX_List;
    [SerializeField]
    private GameObject bloodSplatter_GO;

    public void Damage(float _dmg)
    {
        if (!canTakeDMG) return;
        StartCoroutine(TakeDamage(_dmg));

    }

    IEnumerator TakeDamage(float _dmg)
    {
        canTakeDMG = false;

        HP -= _dmg;

        ParticleSystem vfx = GetBloodVFX();

        if (vfx != null)
        {
            vfx.transform.SetParent(null);
            vfx.transform.position = transform.position;
            vfx.gameObject.SetActive(true);
        }

        if (HP < 0)
        {
            Die();
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        canTakeDMG = true;


        yield return null;
    }

    private void Die()
    {
        isAlive = false;
        bloodSplatter_GO.transform.SetParent(null);
        bloodSplatter_GO.SetActive(true);
        gameObject.SetActive(false);
    }

    private void GetAllPlayers()
    {
        playersTransform = FindObjectsByType<PLAYER>(FindObjectsSortMode.None);

        float aux_dist = 0;

        aux_dist = dist;

        for(int i = 0; i<playersTransform.Length; i++)
        {          
            dist = Vector3.Distance(transform.position, playersTransform[i].transform.position);

            if(dist < aux_dist)
            {
                currentPlayer = playersTransform[i].transform;
            }

            aux_dist = dist;
        }             
    }

    void InstantiateBloodVFX()
    {
        bloodVFX_List = new ParticleSystem[bloodVFXAmount];

        for (int i = 0; i < bloodVFXAmount; i++)
        {
            ParticleSystem _bloodVFX = Instantiate(enemy_SO.blood_VFX, transform);
            _bloodVFX.gameObject.SetActive(false);

            bloodVFX_List[i] = _bloodVFX;
        }
    }

    ParticleSystem GetBloodVFX()
    {
        for (int i = 0; i < bloodVFXAmount; i++)
        {
            if (!bloodVFX_List[i].gameObject.activeInHierarchy)
                return bloodVFX_List[i];
        }

        return null;
    }

    private void Awake()
    {
        GetAllPlayers();

        HP = enemy_SO.m_HP;
        bloodSplatter_GO.SetActive(false);

        InstantiateBloodVFX();
    }
}