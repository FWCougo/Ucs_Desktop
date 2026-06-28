using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIMPLE_SPRITE_ANIMATION : MonoBehaviour
{
    [SerializeField] SpritesContainer[] spritesContainer;

    private void Start()
    {
        for (int i = 0; i < spritesContainer.Length; i++)
        {
            StartCoroutine(PlayAnimation(spritesContainer[i]));
        }
    }

    IEnumerator PlayAnimation(SpritesContainer spContainer)
    {
        float timeBetweenSprites = spContainer.timeBetweenSprites;
        Sprite[] _sprites = spContainer.sprites;
        SpriteRenderer _spRenderer = spContainer.spriteRenderer;

        while (isActiveAndEnabled)
        {
            for (int i = 0; i < _sprites.Length; i++)
            {
                _spRenderer.sprite = _sprites[i];

                yield return new WaitForSeconds(timeBetweenSprites);
            }

        }

        yield return null;

    }


}

[Serializable]
public class SpritesContainer
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public float timeBetweenSprites;
}
