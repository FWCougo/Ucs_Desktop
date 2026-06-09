using UnityEngine;

public class PICKUP_HEALTH : PICKUP
{
    [SerializeField] float healthRecoverAmount;

    public override void PICKED_UP()
    {
        playerManager.PLAYER.ReceiveHealth(healthRecoverAmount);
        Destroy(gameObject);
    }
}
