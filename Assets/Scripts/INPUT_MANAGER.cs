using UnityEngine;
using UnityEngine.InputSystem;

public class INPUT_MANAGER : MonoBehaviour
{
    [SerializeField]
    private Vector2 dir;

    [SerializeField] private PLAYER_MOVE p_move;
    [SerializeField] private WEAPON p_weapon;

    #region Get Set

    public Vector2 Dir { get { return dir; } }

    #endregion


    #region InputActions

    public void onMove(InputAction.CallbackContext _context)
    {

        InputActionPhase _phase = _context.phase;
        
        if(_phase == InputActionPhase.Performed)
        {
            dir = _context.ReadValue<Vector2>();
        }

        p_move.ReceiveMoveInput(dir, _phase);
    }

    public void onAttack(InputAction.CallbackContext _context)
    {
        if(_context.performed)
        {
            p_weapon.UseWeapon(dir);
        }
    }

    public void onPause(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            GAME_MANAGER.Instance.PauseGame();
        }
    }

    #endregion
}
