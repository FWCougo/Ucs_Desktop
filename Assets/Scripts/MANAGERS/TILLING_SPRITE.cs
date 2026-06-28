using UnityEngine;

public class TILLING_SPRITE : MonoBehaviour
{
    [SerializeField] private float width = 1;
    [SerializeField] private float height = 1;

    [ContextMenu("TILLE THIS SHIT")]
    public void TileSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.drawMode = SpriteDrawMode.Tiled;
        sr.tileMode = SpriteTileMode.Adaptive;
        sr.size = new Vector2(width, height); // width / height in units
    }
}
