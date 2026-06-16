using Unity.VisualScripting;
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
    private Canvas p_Canvas;

    public PLAYER_WEAPONS PLAYER_WEAPONS { get { return p_Weapon; } }
    public PLAYER PLAYER { get { return p_Player; } }

    private void Awake()
    {
        p_Move.ChangeSpeed(0);
        p_Move.SetSprites(player_SO);
        p_Player.ChangeMaxHP(player_SO.HP);        
    }

    public void StartGame()
    {
        p_Canvas.gameObject.SetActive(true);
        p_Weapon.SetMainWeapon(player_SO.mainWeapon);
        p_Move.ChangeSpeed(player_SO.speed);
    }



}
