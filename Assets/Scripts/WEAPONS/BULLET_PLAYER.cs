using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BULLET_PLAYER : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;

    private Vector3 bulletDir;

    [SerializeField] private Transform bulletSprite;

    [SerializeField] private float DMG = 5;

    float normalPos;

    public float Damage => DMG;

    private void Start()
    {
        normalPos = bulletSprite.localPosition.y;
    }

    public void ReceiveDirection(Vector3 dir, float lifeSpan)
    {
        bulletDir = dir; // dir já deve chegar normalizado de quem chama
        //StartCoroutine(Disable(lifeSpan));

        Destroy(gameObject, lifeSpan);

        bulletSprite.DOLocalMoveY(normalPos + 1f, lifeSpan / 2).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                bulletSprite.DOLocalMoveY(normalPos, lifeSpan / 2).SetEase(Ease.InQuad);
            });

    }

    private IEnumerator Disable(float lifeSpan)
    {
        yield return new WaitForSeconds(lifeSpan);
        bulletDir = Vector3.zero; // Reseta para a bala não continuar em FixedUpdate
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (bulletDir == Vector3.zero) return;
        transform.Translate(bulletDir * bulletSpeed * Time.fixedDeltaTime, Space.World);
    }
}