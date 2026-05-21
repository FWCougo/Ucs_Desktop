using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLAYER_MOVE : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private PLAYER_SO player_SO;

    [Header("MOVEMENT SPEED")]
    [SerializeField]
    private float speed;

    [Header("SPRITES")]
    [SerializeField]
    private SpriteRenderer p_Sprite;
    [SerializeField]
    private SpriteRenderer g_Sprite;

    [Header("DIRECTION")]
    [SerializeField]
    private float angleDir;
    [SerializeField]
    private Vector3 dir;

    [SerializeField]
    private MOVEMENT_STATE moveState;

    public MOVEMENT_STATE MoveState 
    {         
        get { 
            return moveState; 
        } 
        
        set{
            moveState = value;
        }
    }

    private void Update()
    {
        if (isMoving())
        {
            characterController.Move(dir * speed * Time.deltaTime);
        }
    }

    public void ReceiveMoveInput(Vector2 _dir, InputActionPhase inputPhase)
    {
        switch (inputPhase)
        {
            case InputActionPhase.Performed:
                angleDir = (Mathf.Atan2(_dir.x, _dir.y) * Mathf.Rad2Deg);
                dir.x = _dir.x;
                dir.z = _dir.y;

                ChangeSprite();

                MoveState = MOVEMENT_STATE.moving;
                break;

            case InputActionPhase.Canceled:
                MoveState = MOVEMENT_STATE.idle;
                break;
        }
        
    }

    public void ChangeSprite()
    {
        if (dir.x < 0)
        {
            p_Sprite.flipX = true;
            g_Sprite.flipX = true;
        }
        else
        {
            p_Sprite.flipX = false;
            g_Sprite.flipX = false;
        }

        if(angleDir == 0)
        {
            p_Sprite.sprite = player_SO.backSprite;
        }
        else if(angleDir == 180)
        {
            p_Sprite.sprite = player_SO.frontSprite;
        }
        else
        {
            p_Sprite.sprite = player_SO.sideSprite;
        }
    }

    public bool isMoving()
    {
        switch (moveState){
            case MOVEMENT_STATE.moving:
                return true;
        }

        return false;
    }

}

public enum MOVEMENT_STATE { idle, moving }
