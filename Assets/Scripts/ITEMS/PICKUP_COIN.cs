using UnityEngine;

public class PICKUP_COIN : PICKUP
{

    [SerializeField] private int coinValue = 1;
    public override void PICKED_UP()
    {
        GAME_MANAGER.Instance.ChangeCoins(coinValue);
        Destroy(gameObject);
    }

}
