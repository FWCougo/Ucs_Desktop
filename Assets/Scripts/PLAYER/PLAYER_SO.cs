using UnityEngine;

[CreateAssetMenu(fileName = "PLAYER")]
public class PLAYER_SO : ScriptableObject {

    public string characterName = "John Doe";

    public float speed = 10;

    public float HP = 10;

    public WEAPON mainWeapon;

    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite sideSprite;
}
