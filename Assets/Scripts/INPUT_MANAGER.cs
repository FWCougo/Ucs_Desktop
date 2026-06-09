using UnityEngine;
using UnityEngine.InputSystem;

public class INPUT_MANAGER : MonoBehaviour
{
    [SerializeField]
    private Vector2 dir;

    public Vector3 mouseWorldPos;

    [SerializeField]
    private Vector2 mouseDir;
    public Vector2 MouseDir => mouseDir;

    [SerializeField] private PLAYER_MOVE p_move;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] PLAYER_WEAPONS p_weapon;
    #region Get Set

    public Vector2 Dir { get { return dir; } }

    #endregion


    private void LateUpdate()
    {
        UpdateMousePosition();
    }

    void UpdateMousePosition()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        mouseWorldPos = Vector3.zero;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            
            mouseWorldPos = hit.point;
            
        }

        Vector3 direction = (mouseWorldPos - transform.position).normalized;

        mouseDir = new Vector2(direction.x, direction.z);
    }

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
