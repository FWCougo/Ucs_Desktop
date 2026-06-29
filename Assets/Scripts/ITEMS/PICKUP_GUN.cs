using UnityEngine;

public class PICKUP_GUN : PICKUP
{
    [SerializeField] WEAPON weapon;
    [SerializeField] WEAPON_SO weapon_SO;

    private void OnEnable()
    {
        spriteTrans.sprite = weapon_SO.itemSprite;
        spriteShadowTrans.sprite = weapon_SO.itemSprite;
        weapon = weapon_SO.weapon;
    }

    public override void PICKED_UP()
    {
        if (playerManager.PLAYER_WEAPONS.SetSecondWeapon(weapon))
        {
            Destroy(gameObject);
        }
    }
}
