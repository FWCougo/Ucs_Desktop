using UnityEngine;
using DG.Tweening;

public abstract class PICKUP : MonoBehaviour
{
    public PLAYER_MANAGER playerManager;
    [SerializeField]
    private GameObject spriteTrans;
    [SerializeField]
    private GameObject spriteShadowTrans;

    private void Start()
    {
        float _yPos = spriteTrans.transform.position.y;

        spriteTrans.transform.DOLocalMoveY(2.2f, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        spriteShadowTrans.transform.DOScale(3.8f, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager = other.GetComponent<PLAYER_MANAGER>();
            PICKED_UP();
            print("Pego por " + other.name);
        }
    }
    public abstract void PICKED_UP();

}
