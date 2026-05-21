using UnityEngine;

[CreateAssetMenu(fileName = "PLAYER")]
public class PLAYER_SO : ScriptableObject {

    public string characterName = "John Doe";

    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite sideSprite;
}
