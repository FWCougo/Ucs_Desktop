using UnityEngine;

public class ENEMY : MonoBehaviour
{
    public PLAYER player;

    [SerializeField] float speed = 5f;

    private void Awake()
    {
        player = FindAnyObjectByType<PLAYER>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 dir = player.transform.position - transform.position;

            transform.Translate(dir*speed*Time.fixedDeltaTime);
        }
    }
}
