using UnityEngine;

public class PASSIVA : MonoBehaviour
{
    public PLAYER_MANAGER playerManager;
    public SpriteRenderer passivaSprite;
    public int nivelPassiva = 0;

    private void Awake()
    {
        playerManager = GetComponentInParent<PLAYER_MANAGER>();
    }

    public void SetNivel(int _lv)
    {
        nivelPassiva=_lv;
    }

}
