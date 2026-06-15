using UnityEngine;

public class PASSIVA : MonoBehaviour
{
    public PLAYER_MANAGER playerManager;
    public SpriteRenderer passivaSprite;

    private void Awake()
    {
        playerManager = GetComponentInParent<PLAYER_MANAGER>();
    }

}
