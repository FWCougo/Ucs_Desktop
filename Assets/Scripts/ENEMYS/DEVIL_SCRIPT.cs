using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DEVIL_SCRIPT : MonoBehaviour
{
    Tween devilTremendo;

    [SerializeField] private SpriteRenderer mainSprite;
    [SerializeField] private SpriteRenderer shadowSprite;

    [SerializeField] private Sprite oldDevil;
    [SerializeField] private Sprite businessDevil;
    [SerializeField] private Sprite laserDevil;

    public static DEVIL_SCRIPT Instance;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainSprite.sprite = oldDevil;
        shadowSprite.sprite = oldDevil;
        devilTremendo = transform.DOShakePosition(1,0.5f, 5, 90).SetLoops(-1, LoopType.Yoyo);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameEnum());
    }

    public void EnteredUpgradeScreen()
    {
        StartCoroutine(BusinessDevil());
    }

    IEnumerator BusinessDevil()
    {
        mainSprite.sprite = laserDevil;
        devilTremendo = transform.DOShakePosition(0.5f, 1, 50, 90);

        yield return new WaitForSeconds(0.5f);
        devilTremendo.Kill();

        shadowSprite.sprite = businessDevil;
        mainSprite.sprite = businessDevil;
    }

    IEnumerator StartGameEnum()
    {
        mainSprite.sprite = laserDevil;
        devilTremendo = transform.DOShakePosition(0.5f, 1, 50, 90);

        yield return new WaitForSeconds(0.5f);

        devilTremendo.Kill();
        gameObject.SetActive(false);


        yield break;
    }
}
