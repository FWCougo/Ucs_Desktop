using UnityEngine;
using DG.Tweening;

public abstract class PICKUP : MonoBehaviour
{
    public PLAYER_MANAGER playerManager;
    public SpriteRenderer spriteTrans;
    public SpriteRenderer spriteShadowTrans;
    [SerializeField] public Collider col;
    [SerializeField] private float yMoveAmount = 2.2f;
    [SerializeField] private float scaleAmount = 0.76f;

    private void Start()
    {
        //float _yPos = spriteTrans.transform.position.y;
        float _xScale = spriteTrans.transform.localScale.x;

        spriteTrans.transform.DOLocalMoveY(yMoveAmount, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        spriteShadowTrans.transform.DOScale(_xScale*scaleAmount, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager = other.GetComponent<PLAYER_MANAGER>();
            PICKED_UP();
        }
    }
    public abstract void PICKED_UP();

}
