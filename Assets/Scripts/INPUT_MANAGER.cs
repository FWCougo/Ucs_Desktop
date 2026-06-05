using UnityEngine;
using UnityEngine.InputSystem;

public class INPUT_MANAGER : MonoBehaviour
{
    [SerializeField]
    private Vector2 dir;

    [SerializeField]
    private Vector2 mouseDir;
    public Vector2 MouseDir => mouseDir;

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

    public void onMousePosition(InputAction.CallbackContext _context)
    {
        //Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector2 mouseScreenPosition = _context.ReadValue<Vector2>();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        Vector3 mouseWorldPos = Vector3.zero;

        if (Physics.Raycast(ray, out hit))
        {
            mouseWorldPos = hit.point;
        }

        Vector3 direction = (mouseWorldPos - transform.position).normalized;

        mouseDir = new Vector2(direction.x, direction.z);
    }

    public void onAttack(InputAction.CallbackContext _context)
    {        

        if (_context.performed)
        {
            p_weapon.UseWeapon(mouseDir);
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
