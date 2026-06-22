using UnityEngine;
using DG.Tweening;

public abstract class PICKUP : MonoBehaviour
{
    public PLAYER_MANAGER playerManager;
    public GameObject spriteTrans;
    public GameObject spriteShadowTrans;
    [SerializeField] public Collider col;

    private void Start()
    {
        float _yPos = spriteTrans.transform.position.y;
        float _xScale = spriteTrans.transform.localScale.x;

        spriteTrans.transform.DOLocalMoveY(2.2f, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        spriteShadowTrans.transform.DOScale(_xScale*0.76f, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
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
