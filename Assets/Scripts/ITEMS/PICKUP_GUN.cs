using UnityEngine;

public class PICKUP_GUN : PICKUP
{
    [SerializeField] WEAPON weapon;

    public override void PICKED_UP()
    {
        print(transform.name+" coletado");
        if (playerManager.PLAYER_WEAPONS.SetSecondWeapon(weapon))
        {
            Destroy(gameObject);
        }
    }
}
