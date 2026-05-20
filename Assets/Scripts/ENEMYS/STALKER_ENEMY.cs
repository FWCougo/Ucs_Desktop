using UnityEngine;

public class STALKER_ENEMY : ENEMY
{
    private void FixedUpdate()
    {
        Vector3 direction = currentPlayer.position - transform.position;

        transform.Translate(direction * enemy_SO.m_SPEED * Time.fixedDeltaTime);
    }
}
