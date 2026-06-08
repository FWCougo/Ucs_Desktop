using UnityEngine;

public class PLAYER_MANAGER : MonoBehaviour
{
    [SerializeField]
    private PLAYER_SO player_SO;

    [SerializeField]
    private PLAYER_MOVE p_Move;
    [SerializeField]
    private PLAYER p_Player;
    [SerializeField]
    private PLAYER_WEAPONS p_Weapon;

    [SerializeField]
    private PICKUP[] pickups;


    public PLAYER_WEAPONS PLAYER_WEAPONS { get { return p_Weapon; } }

    private void Awake()
    {
        p_Move.ChangeSpeed(player_SO.speed);
        p_Move.SetSprites(player_SO);
        p_Player.ChangeMaxHP(player_SO.HP);
        p_Weapon.SetMainWeapon(player_SO.mainWeapon);
    }



}
